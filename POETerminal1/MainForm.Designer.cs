namespace POETerminal1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelConnect = new System.Windows.Forms.Panel();
            this.treeViewNet = new System.Windows.Forms.TreeView();
            this.buttonTestNet = new System.Windows.Forms.Button();
            this.buttonNet = new System.Windows.Forms.Button();
            this.buttonTime = new System.Windows.Forms.Button();
            this.buttonBL = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnnect = new System.Windows.Forms.Button();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.panelGraf = new System.Windows.Forms.Panel();
            this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPortCOM = new System.IO.Ports.SerialPort(this.components);
            this.timerGraph = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelConnect.SuspendLayout();
            this.panelGraf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelConnect);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelGraf);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 682);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelConnect
            // 
            this.panelConnect.Controls.Add(this.treeViewNet);
            this.panelConnect.Controls.Add(this.buttonTestNet);
            this.panelConnect.Controls.Add(this.buttonNet);
            this.panelConnect.Controls.Add(this.buttonTime);
            this.panelConnect.Controls.Add(this.buttonBL);
            this.panelConnect.Controls.Add(this.textBoxOutput);
            this.panelConnect.Controls.Add(this.label2);
            this.panelConnect.Controls.Add(this.buttonEnter);
            this.panelConnect.Controls.Add(this.textBoxInput);
            this.panelConnect.Controls.Add(this.label1);
            this.panelConnect.Controls.Add(this.buttonSelectFile);
            this.panelConnect.Controls.Add(this.textBoxFile);
            this.panelConnect.Controls.Add(this.buttonReset);
            this.panelConnect.Controls.Add(this.buttonDisconnect);
            this.panelConnect.Controls.Add(this.buttonConnnect);
            this.panelConnect.Controls.Add(this.comboBoxCOM);
            this.panelConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConnect.Location = new System.Drawing.Point(0, 0);
            this.panelConnect.Name = "panelConnect";
            this.panelConnect.Size = new System.Drawing.Size(360, 682);
            this.panelConnect.TabIndex = 0;
            // 
            // treeViewNet
            // 
            this.treeViewNet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewNet.Location = new System.Drawing.Point(12, 92);
            this.treeViewNet.Name = "treeViewNet";
            this.treeViewNet.Size = new System.Drawing.Size(343, 155);
            this.treeViewNet.TabIndex = 0;
            this.treeViewNet.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewNet_ItemDrag);
            this.treeViewNet.DoubleClick += new System.EventHandler(this.treeViewNet_DoubleClick);
            // 
            // buttonTestNet
            // 
            this.buttonTestNet.Location = new System.Drawing.Point(270, 66);
            this.buttonTestNet.Name = "buttonTestNet";
            this.buttonTestNet.Size = new System.Drawing.Size(80, 21);
            this.buttonTestNet.TabIndex = 15;
            this.buttonTestNet.Text = "test net";
            this.buttonTestNet.UseVisualStyleBackColor = true;
            this.buttonTestNet.Click += new System.EventHandler(this.buttonTestNet_Click);
            // 
            // buttonNet
            // 
            this.buttonNet.Location = new System.Drawing.Point(184, 65);
            this.buttonNet.Name = "buttonNet";
            this.buttonNet.Size = new System.Drawing.Size(80, 21);
            this.buttonNet.TabIndex = 14;
            this.buttonNet.Text = "net";
            this.buttonNet.UseVisualStyleBackColor = true;
            this.buttonNet.Click += new System.EventHandler(this.buttonNet_Click);
            // 
            // buttonTime
            // 
            this.buttonTime.Location = new System.Drawing.Point(98, 65);
            this.buttonTime.Name = "buttonTime";
            this.buttonTime.Size = new System.Drawing.Size(80, 21);
            this.buttonTime.TabIndex = 13;
            this.buttonTime.Text = "time";
            this.buttonTime.UseVisualStyleBackColor = true;
            this.buttonTime.Click += new System.EventHandler(this.buttonTime_Click);
            // 
            // buttonBL
            // 
            this.buttonBL.Location = new System.Drawing.Point(12, 65);
            this.buttonBL.Name = "buttonBL";
            this.buttonBL.Size = new System.Drawing.Size(80, 21);
            this.buttonBL.TabIndex = 12;
            this.buttonBL.Text = "BL";
            this.buttonBL.UseVisualStyleBackColor = true;
            this.buttonBL.Click += new System.EventHandler(this.buttonBL_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 308);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(345, 362);
            this.textBoxOutput.TabIndex = 10;
            this.textBoxOutput.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Out";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnter.Location = new System.Drawing.Point(277, 268);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(80, 21);
            this.buttonEnter.TabIndex = 8;
            this.buttonEnter.Text = "Enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.AcceptsReturn = true;
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInput.Location = new System.Drawing.Point(12, 268);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(259, 20);
            this.textBoxInput.TabIndex = 7;
            this.textBoxInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInput_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "In";
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFile.Location = new System.Drawing.Point(277, 39);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(80, 21);
            this.buttonSelectFile.TabIndex = 5;
            this.buttonSelectFile.Text = "Select File";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(12, 39);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(259, 20);
            this.textBoxFile.TabIndex = 4;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(317, 12);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(40, 21);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "Rst";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisconnect.Location = new System.Drawing.Point(271, 12);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(40, 21);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Dis";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnnect
            // 
            this.buttonConnnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnnect.Location = new System.Drawing.Point(225, 12);
            this.buttonConnnect.Name = "buttonConnnect";
            this.buttonConnnect.Size = new System.Drawing.Size(40, 21);
            this.buttonConnnect.TabIndex = 1;
            this.buttonConnnect.Text = "Con";
            this.buttonConnnect.UseVisualStyleBackColor = true;
            this.buttonConnnect.Click += new System.EventHandler(this.buttonConnnect_Click);
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Location = new System.Drawing.Point(12, 12);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(207, 21);
            this.comboBoxCOM.TabIndex = 0;
            // 
            // panelGraf
            // 
            this.panelGraf.Controls.Add(this.chartGraph);
            this.panelGraf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraf.Location = new System.Drawing.Point(0, 0);
            this.panelGraf.Name = "panelGraf";
            this.panelGraf.Size = new System.Drawing.Size(900, 682);
            this.panelGraf.TabIndex = 0;
            // 
            // chartGraph
            // 
            this.chartGraph.AllowDrop = true;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.Name = "ChartAreaGraph";
            this.chartGraph.ChartAreas.Add(chartArea1);
            this.chartGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartGraph.Legends.Add(legend1);
            this.chartGraph.Location = new System.Drawing.Point(0, 0);
            this.chartGraph.Name = "chartGraph";
            this.chartGraph.Size = new System.Drawing.Size(900, 682);
            this.chartGraph.TabIndex = 0;
            this.chartGraph.Text = "chart1";
            this.chartGraph.DragDrop += new System.Windows.Forms.DragEventHandler(this.chartGraph_DragDrop);
            this.chartGraph.DragEnter += new System.Windows.Forms.DragEventHandler(this.chartGraph_DragEnter);
            // 
            // serialPortCOM
            // 
            this.serialPortCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortCOM_DataReceived);
            // 
            // timerGraph
            // 
            this.timerGraph.Interval = 60000;
            this.timerGraph.Tick += new System.EventHandler(this.timerGraph_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "POETerminal V1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelConnect.ResumeLayout(false);
            this.panelConnect.PerformLayout();
            this.panelGraf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelConnect;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnnect;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.Panel panelGraf;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.IO.Ports.SerialPort serialPortCOM;
        private System.Windows.Forms.TreeView treeViewNet;
        private System.Windows.Forms.Button buttonTestNet;
        private System.Windows.Forms.Button buttonNet;
        private System.Windows.Forms.Button buttonTime;
        private System.Windows.Forms.Button buttonBL;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
        private System.Windows.Forms.Timer timerGraph;
    }
}

