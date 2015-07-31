using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;

namespace POETerminal1
{
    public partial class MainForm : Form
    {
        private string[] ownArgs;
        private string ownConfig;
        public delegate void readSerial();
        public readSerial readLines;
        private bool _canInvoke = true;
        public string serialBuffer;
        public string ownLF = "\n\0";
        public DateTime tickTime;
        private CultureInfo cult = new CultureInfo("en-US");
        public XmlDocument chartFiling;
        public XmlElement chartEntry;
        private bool _autoConnecting;

        public MainForm() {
            InitializeComponent();
            readLines = new readSerial(readFromSerial);
        }

        public MainForm(string[] args)
            : this()
        {
            ownArgs = args;
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string serPort in portNames) {
                comboBoxCOM.Items.Add(serPort);
            }
            buttonDisconnect.Enabled = false;
            _autoConnecting = false;
            if (ownArgs.Length > 0)
            {
                if (Directory.Exists(Path.GetDirectoryName(ownArgs[0])))
                {
                    // seems a valid file name 
                    ownConfig = ownArgs[0];
                    if (File.Exists(ownConfig))
                    {
                        // read configuration
                        XmlNode aConfNode;
                        XmlElement aConfElement;
                        XmlText aConfText;
                        XmlDocument myConfig = new XmlDocument();
                        bool loadSuccess = true;
                        try
                        {
                            myConfig.Load(ownConfig);
                        }
                        catch
                        {
                            loadSuccess = false;
                        }
                        if (loadSuccess)
                        {
                            aConfNode = myConfig.DocumentElement.SelectSingleNode("COM");
                            if (aConfNode != null)
                            {
                                if (aConfNode is XmlElement)
                                {
                                    aConfElement = (XmlElement)aConfNode;
                                    if (aConfElement.FirstChild is XmlText)
                                    {
                                        aConfText = (XmlText)aConfElement.FirstChild;
                                        if (comboBoxCOM.FindString(aConfText.Value) > 0)
                                        {
                                            comboBoxCOM.Text = aConfText.Value;
                                            // autoConnect procedure 
                                            Connect();
                                            _autoConnecting = true;
                                            SendTimeSync();
                                        }
                                    }
                                }
                            }
                            aConfNode = myConfig.DocumentElement.SelectSingleNode("LOG");
                            if (aConfNode != null)
                            {
                                if (aConfNode is XmlElement)
                                {
                                    aConfElement = (XmlElement)aConfNode;
                                    if (aConfElement.FirstChild is XmlText)
                                    {
                                        aConfText = (XmlText)aConfElement.FirstChild;
                                        textBoxFile.Text = aConfText.Value;
                                    }
                                }
                            }
                            aConfNode = myConfig.DocumentElement.SelectSingleNode("net");
                            if (aConfNode != null)
                            {
                                if (aConfNode is XmlElement)
                                {
                                    aConfElement = (XmlElement)aConfNode;
                                    treeViewNet.BeginUpdate();
                                    treeViewNet.Nodes.Clear();
                                    TreeNode myTreeNet = treeViewNet.Nodes.Add("<net/>", "<net/>");
                                    XmlNodeList myNodes = aConfElement.SelectNodes("node");
                                    foreach (XmlNode aNode in myNodes)
                                    {
                                        XmlElement myNetNode = (XmlElement)aNode;
                                        string myNodeId = "<node id=\"" + myNetNode.GetAttribute("id") + "\" name=\"" + myNetNode.GetAttribute("name") + "\" />";
                                        TreeNode myTreeNetNode = myTreeNet.Nodes.Add(myNodeId, myNodeId);
                                        XmlNodeList mySensors = myNetNode.SelectNodes("analog|digital|switch|pwm");
                                        foreach (XmlNode aSensor in mySensors)
                                        {
                                            XmlElement mySensor = (XmlElement)aSensor;
                                            myNodeId = "<" + mySensor.Name
                                                     + " node=\"" + myNetNode.GetAttribute("id")
                                                     + "\" id=\"" + mySensor.GetAttribute("id") + "\" />";
                                            //myTreeNetNode.Nodes.Add(myNodeId, mySensor.OuterXml);
                                            myTreeNetNode.Nodes.Add(myNodeId, myNodeId);
                                            // add as Series, when Series become removable
                                            if (mySensor.GetAttribute("graphing").Contains("on"))
                                            {
                                                Series mySeries = new Series(myNodeId);
                                                mySeries.ChartType = SeriesChartType.FastLine;
                                                mySeries.XValueType = ChartValueType.Time;
                                                mySeries.Legend = "Legend1";
                                                mySeries.ChartArea = "ChartAreaGraph";
                                                chartGraph.Series.Add(mySeries);
                                                chartGraph.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";

                                            }
                                        }
                                    }
                                    treeViewNet.EndUpdate();
                                }
                            }
                        }
                    }
                }
            }
        }


        private void Connect() 
        {
            serialPortCOM.PortName = comboBoxCOM.SelectedItem.ToString();
            serialPortCOM.NewLine = "\f";
            serialPortCOM.DiscardNull = false;
            serialPortCOM.Open();
            buttonDisconnect.Enabled = true;
            buttonConnnect.Enabled = false;
        }

        private void buttonConnnect_Click(object sender, EventArgs e) {
            Connect();
        }

        private void Disconnect()
        {
            serialPortCOM.Close();
            buttonConnnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            timerGraph.Enabled = false;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e) {
            Disconnect();
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            serialPortCOM.Close();
            serialPortCOM.Open();
        }

        public void interpretSerial() {
            if ((serialBuffer.Length > 0) && (serialBuffer[0] == 'U'))
            {
                serialBuffer = serialBuffer.Substring(1);
                XmlDocument myMessage = new XmlDocument();
                XmlNodeList myNodes;
                string myNodeId;
                string myNode;
                string myId;
                string myStrValue;
                double myDblValue;
                string mySeriesName;
                TreeNode[] myTreeNodes;
                string mySensorValue;
                TreeNode[] myTreeSensors;

                XmlElement measureEntry;
                bool readable = true;
                try {
                    myMessage.LoadXml(serialBuffer);
                }
                catch (Exception e) {
                    readable = false;
                }
                if (readable) {
                    switch (myMessage.DocumentElement.Name) {
                        case "time":
                            if (_autoConnecting)
                            {
                                timerGraph.Interval = 200;
                                timerGraph.Enabled = true;
                                _autoConnecting = false;
                            }
                            break;
                        case "net":
                            // bring node listing to treeViewNet
                            treeViewNet.BeginUpdate();
                            treeViewNet.Nodes.Clear();
                            TreeNode myTreeNet = treeViewNet.Nodes.Add("<net/>", "<net/>");
                            myNodes = myMessage.DocumentElement.SelectNodes("node");
                            foreach (XmlNode aNode in myNodes) {
                                XmlElement myNetNode = (XmlElement)aNode;
                                myNodeId = "<node id=\"" + myNetNode.GetAttribute("id") + "\" name=\"" + myNetNode.GetAttribute("name") + "\" />";
                                TreeNode myTreeNetNode = myTreeNet.Nodes.Add(myNodeId, myNodeId);
                            }
                            treeViewNet.EndUpdate();
                            break;
                        case "node":
                            // bring sensor/actor listing to "node"-Node of treeViewNet
                            treeViewNet.BeginUpdate();
                            myNodeId = "<node id=\"" + myMessage.DocumentElement.GetAttribute("id") + "\" name=\"" + myMessage.DocumentElement.GetAttribute("name") + "\" />";
                            myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0) {
                                myNodes = myMessage.DocumentElement.SelectNodes("analog|digital|switch|pwm");
                                foreach (XmlNode aNode in myNodes) {
                                    XmlElement myElement = (XmlElement)aNode;
                                    myNodeId = "<" + myElement.Name
                                             + " node=\"" + myMessage.DocumentElement.GetAttribute("id")
                                             + "\" id=\"" + myElement.GetAttribute("id") + "\" />";
                                    //myTreeNodes[0].Nodes.Add(myNodeId, aNode.OuterXml);
                                    myTreeNodes[0].Nodes.Add(myNodeId, myNodeId);
                                }
                            }
                            treeViewNet.EndUpdate();
                            break;
                        case "analog":
                            myNode = myMessage.DocumentElement.GetAttribute("node");
                            myId = myMessage.DocumentElement.GetAttribute("id");
                            myStrValue = myMessage.DocumentElement.GetAttribute("value");
                            Double.TryParse(myStrValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, cult, out myDblValue);
                            mySeriesName = "<analog node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            if (chartGraph.Series.IndexOf(mySeriesName) >= 0)
                            {
                                chartGraph.Series[mySeriesName].Points.AddXY(tickTime, myDblValue);
                                if (chartEntry != null)
                                {
                                    measureEntry = chartFiling.CreateElement("analog");
                                    measureEntry.SetAttribute("node", myNode);
                                    measureEntry.SetAttribute("id", myId);
                                    measureEntry.SetAttribute("value", myStrValue);
                                    chartEntry.AppendChild(measureEntry);
                                }
                            }
                            myNodeId = "<analog" + " node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0)
                            {
                                myNodes = myMessage.DocumentElement.SelectNodes("numerator|denominator|offset|unit");
                                foreach (XmlNode aNode in myNodes)
                                {
                                    XmlElement myElement = (XmlElement)aNode;
                                    mySensorValue = "<" + myElement.Name
                                                  + " value=\"" + myElement.GetAttribute("value")
                                                  + "\" />";
                                    myNodeId = "<analog" + " node=\"" + myNode + "\" id=\"" + myId + "\" >"
                                             + "<" + myElement.Name + "></" + myElement.Name + ">"
                                             + "</analog>";
                                    myTreeSensors = myTreeNodes[0].Nodes.Find(myNodeId, true);
                                    if (myTreeSensors.Count() == 0)
                                    {
                                        myTreeNodes[0].Nodes.Add(myNodeId, mySensorValue);
                                    }
                                    else
                                    {
                                        myTreeSensors[0].Text = mySensorValue;
                                    }
                                }
                            }
                            break;
                        case "digital":
                            myNode = myMessage.DocumentElement.GetAttribute("node");
                            myId = myMessage.DocumentElement.GetAttribute("id");
                            myStrValue = myMessage.DocumentElement.GetAttribute("value");
                            Double.TryParse(myStrValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, cult, out myDblValue);
                            mySeriesName = "<digital node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            if (chartGraph.Series.IndexOf(mySeriesName) >= 0)
                            {
                                chartGraph.Series[mySeriesName].Points.AddXY(tickTime, myDblValue);
                                if (chartEntry != null)
                                {
                                    measureEntry = chartFiling.CreateElement("digital");
                                    measureEntry.SetAttribute("node", myNode);
                                    measureEntry.SetAttribute("id", myId);
                                    measureEntry.SetAttribute("value", myStrValue);
                                    chartEntry.AppendChild(measureEntry);
                                }
                            }
                            myNodeId = "<digital" + " node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0)
                            {
                                myNodes = myMessage.DocumentElement.SelectNodes("lovalue|hivalue");
                                foreach (XmlNode aNode in myNodes)
                                {
                                    XmlElement myElement = (XmlElement)aNode;
                                    mySensorValue = "<" + myElement.Name
                                                  + " value=\"" + myElement.GetAttribute("value")
                                                  + "\" />";
                                    myNodeId = "<digital" + " node=\"" + myNode + "\" id=\"" + myId + "\" >"
                                             + "<" + myElement.Name + "></" + myElement.Name + ">"
                                             + "</digital>";
                                    myTreeSensors = myTreeNodes[0].Nodes.Find(myNodeId, true);
                                    if (myTreeSensors.Count() == 0)
                                    {
                                        myTreeNodes[0].Nodes.Add(myNodeId, mySensorValue);
                                    }
                                }
                            }
                            break;
                        case "switch":
                            myNode = myMessage.DocumentElement.GetAttribute("node");
                            myId = myMessage.DocumentElement.GetAttribute("id");
                            myStrValue = myMessage.DocumentElement.GetAttribute("value");
                            Double.TryParse(myStrValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, cult, out myDblValue);
                            mySeriesName = "<switch node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            if (chartGraph.Series.IndexOf(mySeriesName) >= 0)
                            {
                                chartGraph.Series[mySeriesName].Points.AddXY(tickTime, myDblValue);
                                if (chartEntry != null)
                                {
                                    measureEntry = chartFiling.CreateElement("switch");
                                    measureEntry.SetAttribute("node", myNode);
                                    measureEntry.SetAttribute("id", myId);
                                    measureEntry.SetAttribute("value", myStrValue);
                                    chartEntry.AppendChild(measureEntry);
                                }
                            }
                            myNodeId = "<switch" + " node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0)
                            {
                                myNodes = myMessage.DocumentElement.SelectNodes("lovalue|hivalue");
                                foreach (XmlNode aNode in myNodes)
                                {
                                    XmlElement myElement = (XmlElement)aNode;
                                    mySensorValue = "<" + myElement.Name
                                                  + " value=\"" + myElement.GetAttribute("value")
                                                  + "\" />";
                                    myNodeId = "<switch" + " node=\"" + myNode + "\" id=\"" + myId + "\" >"
                                             + "<" + myElement.Name + "></" + myElement.Name + ">"
                                             + "</switch>";
                                    myTreeSensors = myTreeNodes[0].Nodes.Find(myNodeId, true);
                                    if (myTreeSensors.Count() == 0)
                                    {
                                        myTreeNodes[0].Nodes.Add(myNodeId, mySensorValue);
                                    }
                                }
                            }
                            break;
                        case "pwm":
                            myNode = myMessage.DocumentElement.GetAttribute("node");
                            myId = myMessage.DocumentElement.GetAttribute("id");
                            myStrValue = myMessage.DocumentElement.GetAttribute("value");
                            Double.TryParse(myStrValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, cult, out myDblValue);
                            mySeriesName = "<pwm node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            if (chartGraph.Series.IndexOf(mySeriesName) >= 0)
                            {
                                chartGraph.Series[mySeriesName].Points.AddXY(tickTime, myDblValue);
                                if (chartEntry != null)
                                {
                                    measureEntry = chartFiling.CreateElement("pwm");
                                    measureEntry.SetAttribute("node", myNode);
                                    measureEntry.SetAttribute("id", myId);
                                    measureEntry.SetAttribute("value", myStrValue);
                                    chartEntry.AppendChild(measureEntry);
                                }
                            }
                            myNodeId = "<pwm" + " node=\"" + myNode + "\" id=\"" + myId + "\" />";
                            myTreeNodes = treeViewNet.Nodes.Find(myNodeId, true);
                            if (myTreeNodes.Count() > 0)
                            {
                                myNodes = myMessage.DocumentElement.SelectNodes("frequency");
                                foreach (XmlNode aNode in myNodes)
                                {
                                    XmlElement myElement = (XmlElement)aNode;
                                    mySensorValue = "<" + myElement.Name
                                                  + " value=\"" + myElement.GetAttribute("value")
                                                  + "\" />";
                                    myNodeId = "<pwm" + " node=\"" + myNode + "\" id=\"" + myId + "\" >"
                                             + "<" + myElement.Name + "></" + myElement.Name + ">"
                                             + "</pwm>";
                                    myTreeSensors = myTreeNodes[0].Nodes.Find(myNodeId, true);
                                    if (myTreeSensors.Count() == 0)
                                    {
                                        myTreeNodes[0].Nodes.Add(myNodeId, mySensorValue);
                                    }
                                }
                            }
                            break;
                        default:
                            // non-usable message ... should be passed to first node again
                            break;
                    }
                }
                // clear anyway
                serialBuffer = "";
            }
            else
            {
                // some sort of secondary messaging, 
                // should already be placed in textBoxOutput
                // -> clear buffer
                serialBuffer = "";
            }
        }

        public void readFromSerial() {
            char[] myRead = new char[1024];
            //char myRead;
            _canInvoke = false;
            while (serialPortCOM.BytesToRead > 0) {
                // serialPort swallows newline, use ReadLine() instead  
                //myRead = (char)serialPortCOM.ReadChar();
                //myRead = (char)serialPortCOM.ReadByte();
                int haveRead = serialPortCOM.Read(myRead, 0, 1024);
                for (int cnt = 0; cnt < haveRead; cnt++) {
                    if (myRead[cnt] == '\0')
                    {
                        textBoxOutput.AppendText("\r\n");
                        interpretSerial();
                    }
                    else if (myRead[cnt] == '\n')
                    {
                        textBoxOutput.AppendText("\r\n");
                        interpretSerial();
                    }
                    else
                    {
                        textBoxOutput.AppendText(myRead[cnt].ToString());
                        serialBuffer += myRead[cnt];
                    }
                }
                //*/
                //serialBuffer = serialPortCOM.ReadLine();
                //textBoxOutput.AppendText(serialBuffer + "\r\n");
                //interpretSerial();
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
            if (ownConfig.Length > 0)
            {
                // store configuration 
                XmlDocument myConfig = new XmlDocument();
                myConfig.AppendChild(myConfig.CreateElement("POETerminal"));
                XmlElement anElement = myConfig.CreateElement("COM");
                XmlText aText = myConfig.CreateTextNode(comboBoxCOM.Text);
                anElement.AppendChild(aText);
                myConfig.DocumentElement.AppendChild(anElement);
                anElement = myConfig.CreateElement("LOG");
                aText = myConfig.CreateTextNode(textBoxFile.Text);
                anElement.AppendChild(aText);
                myConfig.DocumentElement.AppendChild(anElement);
                // store net configuration from treeview
                TreeNode[] myTreeNodes = treeViewNet.Nodes.Find("<net/>", true);
                if (myTreeNodes.Length > 0)
                {
                    TreeNode myNetNode = myTreeNodes[0];
                    anElement = myConfig.CreateElement("net");
                    myConfig.DocumentElement.AppendChild(anElement);
                    foreach (TreeNode myNodeNode in myNetNode.Nodes)
                    {
                        string nodeDescription = myNodeNode.Name;
                        XmlDocumentFragment myNodeElement = myConfig.CreateDocumentFragment();
                        myNodeElement.InnerXml = nodeDescription;
                        XmlElement theNode = (XmlElement)anElement.AppendChild(myNodeElement);
                        foreach (TreeNode mySensorNode in myNodeNode.Nodes)
                        {
                            XmlDocumentFragment mySensorElement = myConfig.CreateDocumentFragment();
                            mySensorElement.InnerXml = mySensorNode.Name;
                            XmlElement theSensor = (XmlElement)theNode.AppendChild(mySensorElement);
                            if (chartGraph.Series.IndexOf(mySensorNode.Name) >= 0)
                            {
                                theSensor.SetAttribute("graphing", "on");
                            }
                        }
                    }
                }
                // store graphing configuration of chartGraph.Series
                //if (chartGraph.Series.Count > 0)
                //{
                //    foreach (Series aSeries in chartGraph.Series)
                //    {
                //        string myNodeSensor = aSeries.Name;
                //        XmlDocumentFragment mySensor = myConfig.CreateDocumentFragment();
                //        mySensor.InnerXml = myNodeSensor;
                //        XmlElement mySensorElement = (XmlElement)mySensor.FirstChild;
                //        string mySelect = "//node[@id=\"" + mySensorElement.GetAttribute("node") + "\"]";
                //        XmlElement myNodeElement = (XmlElement)myConfig.SelectSingleNode(mySelect);
                //        if (myNodeElement != null)
                //        {
                //            myNodeElement.AppendChild(mySensor);
                //        }
                //    }
                //}

                myConfig.Save(ownConfig);
            }
            if (textBoxFile.Text.Length > 0)
            {
                string myFileBase = Path.GetDirectoryName(textBoxFile.Text);
                if (Directory.Exists(myFileBase))
                {
                    if (MessageBox.Show("Save Data ?", Application.ProductName, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string myFileXmlName = Path.ChangeExtension(textBoxFile.Text, tickTime.AddMinutes(-1).ToString("yyyyMMdd") + ".xml");
                        chartFiling.Save(myFileXmlName);
                        string myFilePngName = Path.ChangeExtension(textBoxFile.Text, tickTime.AddMinutes(-1).ToString("yyyyMMdd") + ".png");
                        Bitmap storeBitmap = new Bitmap(chartGraph.Width, chartGraph.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        chartGraph.DrawToBitmap(storeBitmap, new Rectangle(0, 0, chartGraph.Width, chartGraph.Height));
                        storeBitmap.Save(myFilePngName, ImageFormat.Png);
                    }
                }
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

        private void SendTimeSync()
        {
            serialPortCOM.Write("U<time>" + DateTime.Now.ToLongTimeString() + "</time>" + ownLF);
        }

        private void buttonTime_Click(object sender, EventArgs e) 
        {
            SendTimeSync();
        }

        private void buttonNet_Click(object sender, EventArgs e) {
            serialPortCOM.Write("U<net/>" + ownLF);
        }

        /* not needed anymore (direct access TreeView.DblClk)
        private void buttonNode_Click(object sender, EventArgs e) {
            string myMessage = "U<node id=\"1\" />" + ownLF;
            serialPortCOM.Write(myMessage);
        }
        */

        private void treeViewNet_DoubleClick(object sender, EventArgs e)
        {
            if (treeViewNet.SelectedNode != null)
            {
                textBoxInput.Text = "U" + treeViewNet.SelectedNode.Name;
                string myMessage = "U" + treeViewNet.SelectedNode.Name + ownLF;
                if (myMessage.Contains("<numerator")
                    | myMessage.Contains("<denominator")
                    | myMessage.Contains("<offset")
                    | myMessage.Contains("<unit")
                    | myMessage.Contains("<lovalue")
                    | myMessage.Contains("<hivalue")
                    | myMessage.Contains("<frequency")
                    )
                {
                    // fill in value by hand
                }
                else
                {
                    serialPortCOM.Write(myMessage);
                }
            }
        }

        private void chartGraph_DragDrop(object sender, DragEventArgs e)
        {
            string mySeriesName = (String)e.Data.GetData("Text");
            if (chartGraph.Series.IndexOf(mySeriesName) >= 0)
            {
                // already there -> delete
                chartGraph.Series.RemoveAt(chartGraph.Series.IndexOf(mySeriesName));
            }
            else
            {
                Series mySeries = new Series(mySeriesName);
                mySeries.ChartType = SeriesChartType.FastLine;
                mySeries.XValueType = ChartValueType.Time;
                //mySeries.LabelFormat = 
                mySeries.Legend = "Legend1";
                mySeries.ChartArea = "ChartAreaGraph";
                chartGraph.Series.Add(mySeries);
                chartGraph.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
                if (!timerGraph.Enabled)
                {
                    // 200 ms means search Minute pass (seconds == 0)
                    timerGraph.Interval = 200;
                    timerGraph.Enabled = true;

                }
            }
        }

        private void treeViewNet_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if ((e.Item) is TreeNode)
            {
                TreeNode myNode = (TreeNode)e.Item;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    DoDragDrop(myNode.Name, DragDropEffects.Copy);
                }
            }
        }

        private void chartGraph_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Text"))
            {
                string mySeriesName = (String)e.Data.GetData("Text");
                if (mySeriesName.StartsWith("<analog"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else if (mySeriesName.StartsWith("<digital"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else if (mySeriesName.StartsWith("<switch"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else if (mySeriesName.StartsWith("<pwm"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void timerGraph_Tick(object sender, EventArgs e)
        {
            //seriesTick++;
            textBoxOutput.Clear();
            if (timerGraph.Interval < 1000)
            {
                if (DateTime.Now.Second == 0)
                {
                    chartFiling = new XmlDocument();
                    chartFiling.AppendChild( chartFiling.CreateElement("chart"));
                    timerGraph.Interval = 60000;
                    timerGraph.Enabled = true;
                }
            }
            else
            {
                if (timerGraph.Interval != 60000)
                {
                    // set back 60 seconds
                    timerGraph.Interval = 60000;
                    timerGraph.Enabled = true;
                }
                tickTime = DateTime.Now;
                if (tickTime.Second != 0)
                {
                    // maybe synchronization needed
                    if (tickTime.Second > 30)
                    {
                        if (tickTime.Second < 55)
                        {
                            // next tick later
                            timerGraph.Interval = 60000 + (60 - tickTime.Second) * 1000;
                        }
                        tickTime = tickTime.AddSeconds(60 - tickTime.Second);
                    }
                    else
                    {
                        if (tickTime.Second > 5)
                        {
                            // next tick earlier
                            timerGraph.Interval = 60000 - tickTime.Second * 1000;
                        }
                        tickTime = tickTime.AddSeconds(-tickTime.Second);
                    }
                }
                if (tickTime.Minute == 0)
                {
                    // every hour send time sync
                    //serialPortCOM.Write("U<time>" + DateTime.Now.ToLongTimeString() + "</time>" + ownLF);
                    // store chartFiling
                    if (textBoxFile.Text.Length > 0)
                    {
                        string myFileBase = Path.GetDirectoryName(textBoxFile.Text);
                        if (Directory.Exists(myFileBase))
                        {
                            string myFileXmlName = Path.ChangeExtension(textBoxFile.Text, tickTime.AddMinutes(-1).ToString("yyyyMMdd") + ".xml");
                            chartFiling.Save(myFileXmlName);
                            if (tickTime.Hour == 0)
                            {
                                string myFilePngName = Path.ChangeExtension(textBoxFile.Text, tickTime.AddMinutes(-1).ToString("yyyyMMdd") + ".png");
                                Bitmap storeBitmap = new Bitmap(chartGraph.Width, chartGraph.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                chartGraph.DrawToBitmap(storeBitmap, new Rectangle(0, 0, chartGraph.Width, chartGraph.Height));
                                storeBitmap.Save(myFilePngName, ImageFormat.Png);
                                foreach (Series aSeries in chartGraph.Series)
                                {
                                    aSeries.Points.Clear();
                                }
                            }
                        }
                    }
                    if (tickTime.Hour == 0)
                    {
                        // reset XmlDocument
                        chartFiling = new XmlDocument();
                        chartFiling.AppendChild(chartFiling.CreateElement("chart"));
                    }
                }
                chartEntry = chartFiling.CreateElement("measure");
                chartEntry.SetAttribute("date", tickTime.ToString("yyyy-MM-dd"));
                chartEntry.SetAttribute("time", tickTime.ToString("HH:mm:ss"));
                chartFiling.DocumentElement.AppendChild(chartEntry);
                foreach (Series aSeries in chartGraph.Series)
                {
                    // as there maybe a time sync ahead wait before sending
                    for (int cnt = 0; cnt < 5; cnt++)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(50);
                    }
                    string myMessage = "U" + aSeries.Name;
                    textBoxInput.Text = myMessage;
                    serialPortCOM.Write(myMessage + ownLF);
                }
            }
        }

        private void buttonTestNet_Click(object sender, EventArgs e)
        {
            string myNodeId;
            string mySensorActorId;
            treeViewNet.BeginUpdate();
            treeViewNet.Nodes.Clear();
            TreeNode myTreeNet = treeViewNet.Nodes.Add("<net/>", "<net/>");
            for (int cntNode = 1; cntNode < 4; cntNode++)
            {
                myNodeId = "<node id=\"" + cntNode.ToString() + "\" />";
                TreeNode myTreeNetNode = myTreeNet.Nodes.Add(myNodeId, "<node/>");
                myTreeNetNode.Nodes.Add("id='" + cntNode.ToString() + "'");
                myTreeNetNode.Nodes.Add("name='" + cntNode.ToString("X4") + "'");
                for (int cntAnalog = 1; cntAnalog < 5; cntAnalog++)
                {
                    mySensorActorId = "<analog node=\"" + cntNode.ToString() + "\" id=\"" + cntAnalog.ToString() + "\" />";
                    myTreeNetNode.Nodes.Add(mySensorActorId, "<analog id=\"" + cntAnalog.ToString() + "\" value=\"" + cntNode.ToString() + "\" />");
                }
                for (int cntDigital = 1; cntDigital < 3; cntDigital++)
                {
                    mySensorActorId = "<digital node=\"" + cntNode.ToString() + "\" id=\"" + cntDigital.ToString() + "\" />";
                    myTreeNetNode.Nodes.Add(mySensorActorId, "<digital id=\"" + cntDigital.ToString() + "\" value=\"0\" />");
                }
                for (int cntSwitch = 1; cntSwitch < 3; cntSwitch++)
                {
                    mySensorActorId = "<switch node=\"" + cntNode.ToString() + "\" id=\"" + cntSwitch.ToString() + "\" />";
                    myTreeNetNode.Nodes.Add(mySensorActorId, "<switch id=\"" + cntSwitch.ToString() + "\" value=\"0\" />");
                }
                for (int cntPwm = 1; cntPwm < 3; cntPwm++)
                {
                    mySensorActorId = "<pwm node=\"" + cntNode.ToString() + "\" id=\"" + cntPwm.ToString() + "\" />";
                    myTreeNetNode.Nodes.Add(mySensorActorId, "<pwm id=\"" + cntPwm.ToString() + "\" value=\"50\" />");
                }
            }
            treeViewNet.EndUpdate();
        }
    }
}
