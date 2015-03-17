using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POETerminal1
{
    public partial class MainForm : Form
    {
        public delegate void readSerial();
        public readSerial readLines;
        private bool _canInvoke = true;
        public string serialBuffer;

        public MainForm()
        {
            InitializeComponent();
            readLines = new readSerial(readFromSerial);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string serPort in portNames) {
                comboBoxCOM.Items.Add(serPort);
            }
            comboBoxLF.SelectedIndex = 0;
            buttonDisconnect.Enabled = false;
        }

        private void buttonConnnect_Click(object sender, EventArgs e)
        {
            serialPortCOM.PortName = comboBoxCOM.SelectedItem.ToString();
            serialPortCOM.Open();
            buttonDisconnect.Enabled = true;
            buttonConnnect.Enabled = false;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            serialPortCOM.Close();
            buttonConnnect.Enabled = true;
            buttonDisconnect.Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            serialPortCOM.Close();
            serialPortCOM.Open();
        }

        public void readFromSerial()
        {
            _canInvoke = false;
            while (serialPortCOM.BytesToRead > 0) {
                serialBuffer += serialPortCOM.ReadExisting();
            }
            serialBuffer = serialBuffer.Replace("\r\n", "\n");
            serialBuffer = serialBuffer.Replace("\n", "\r\n");
            textBoxOutput.AppendText(serialBuffer);
            serialBuffer = "";
            _canInvoke = true;
        }


        private void serialPortCOM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (_canInvoke) {
                Invoke(readLines);
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string myLF;
            if (comboBoxLF.SelectedItem.ToString().Equals("\\n")) { myLF = "\n"; } 
            else { myLF = ""; }
            serialPortCOM.Write(textBoxInput.Text + myLF);
            textBoxInput.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPortCOM.IsOpen) {
                serialPortCOM.Close();
            }
        }

        private void textBoxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) {
                string myLF;
                if (comboBoxLF.SelectedItem.ToString().Equals("\\n")) { myLF = "\n"; }
                else { myLF = ""; }
                serialPortCOM.Write(textBoxInput.Text + myLF);
                textBoxInput.Text = "";
            }
        }
    }
}
