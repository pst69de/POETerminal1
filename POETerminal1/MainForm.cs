using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace POETerminal1
{
    public partial class MainForm : Form
    {
        public delegate void readSerial();
        public readSerial readLines;
        private bool _canInvoke = true;
        public string serialBuffer;
        public string ownLF = "\n";

        public MainForm() {
            InitializeComponent();
            readLines = new readSerial(readFromSerial);
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string serPort in portNames) {
                comboBoxCOM.Items.Add(serPort);
            }
            buttonDisconnect.Enabled = false;
        }

        private void buttonConnnect_Click(object sender, EventArgs e) {
            serialPortCOM.PortName = comboBoxCOM.SelectedItem.ToString();
            serialPortCOM.Open();
            buttonDisconnect.Enabled = true;
            buttonConnnect.Enabled = false;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e) {
            serialPortCOM.Close();
            buttonConnnect.Enabled = true;
            buttonDisconnect.Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            serialPortCOM.Close();
            serialPortCOM.Open();
        }

        public void interpretSerial() {
            if (serialBuffer[0] == 'U') {
                serialBuffer = serialBuffer.Substring(1);
                XmlDocument myMessage = new XmlDocument();
                XmlNodeList myNodes;
                bool readable = true;
                try {
                    myMessage.LoadXml(serialBuffer);
                }
                catch (Exception e) {
                    readable = false;
                }
                if (readable) {
                    switch (myMessage.DocumentElement.Name) {
                        case "net":
                            // bring node listing to treeViewNet
                            treeViewNet.BeginUpdate();
                            treeViewNet.Nodes.Clear();
                            TreeNode myTreeNet = treeViewNet.Nodes.Add("<net/>");
                            myNodes = myMessage.SelectNodes("node");
                            foreach (XmlNode aNode in myNodes) {
                                XmlElement myNetNode = (XmlElement)aNode;
                                TreeNode myTreeNetNode = myTreeNet.Nodes.Add("id='" + myNetNode.GetAttribute("id") + "'", "<node/>");
                                myTreeNetNode.Nodes.Add("id='" + myNetNode.GetAttribute("id") + "'");
                                myTreeNetNode.Nodes.Add("name='" + myNetNode.GetAttribute("name") + "'");
                            }
                            treeViewNet.EndUpdate();
                            break;
                        case "node":
                            // bring sensor/actor listing to "node"-Node of treeViewNet
                            treeViewNet.BeginUpdate();
                            string myNodeId = "id='" + myMessage.DocumentElement.GetAttribute("id") + "'";
                            TreeNode[] myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0) {
                                myNodes = myMessage.SelectNodes("node/*");
                                foreach (XmlNode aNode in myNodes) {
                                    myTreeNodes[0].Nodes.Add(aNode.InnerXml);
                                }
                            }
                            treeViewNet.EndUpdate();
                            break;
                        default:
                            // non-usable message ... should be passed to first node again
                            break;
                    }
                }
                // clear anyway
                serialBuffer = "";
            } else {
                // some sort of secondary messaging, 
                // should already be placed in textBoxOutput
                // -> clear buffer
                serialBuffer = "";
            }
        }

        public void readFromSerial() {
            //char[] myRead = new char[1];
            char myRead;
            _canInvoke = false;
            while (serialPortCOM.BytesToRead > 0) {
                myRead = (char)serialPortCOM.ReadChar();
                if (myRead == '\0') {
                    textBoxOutput.AppendText("\r\n");
                    interpretSerial();
                } else if (myRead == '\n') {
                    textBoxOutput.AppendText("\r\n");
                    interpretSerial();
                } else {
                    textBoxOutput.AppendText(myRead.ToString());
                }
                serialBuffer += myRead;
            }
            _canInvoke = true;
        }


        private void serialPortCOM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            if (_canInvoke) {
                Invoke(readLines);
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e) {
            serialPortCOM.Write(textBoxInput.Text + ownLF);
            textBoxInput.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (serialPortCOM.IsOpen) {
                serialPortCOM.Close();
            }
        }

        private void textBoxInput_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                serialPortCOM.Write(textBoxInput.Text + ownLF);
                textBoxInput.Text = "";
            }
        }

        private void buttonBL_Click(object sender, EventArgs e) {
            serialPortCOM.Write("B" + ownLF);
        }

        private void buttonTime_Click(object sender, EventArgs e) {
            serialPortCOM.Write("U<time>" + DateTime.Now.ToLongTimeString() + "</time>" + ownLF);
        }

        private void buttonNet_Click(object sender, EventArgs e) {
            serialPortCOM.Write("U<net/>" + ownLF);
        }

        private void buttonNode_Click(object sender, EventArgs e) {
            serialPortCOM.Write("U<node id=\"1\" />" + ownLF);
        }
    }
}
