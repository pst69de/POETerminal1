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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelConnect = new System.Windows.Forms.Panel();
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
            this.serialPortCOM = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxLF = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelConnect.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(838, 478);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelConnect
            // 
            this.panelConnect.Controls.Add(this.comboBoxLF);
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
            this.panelConnect.Size = new System.Drawing.Size(400, 478);
            this.panelConnect.TabIndex = 0;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 117);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(385, 349);
            this.textBoxOutput.TabIndex = 10;
            this.textBoxOutput.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Out";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnter.Location = new System.Drawing.Point(317, 78);
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
            this.textBoxInput.Location = new System.Drawing.Point(12, 78);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(247, 20);
            this.textBoxInput.TabIndex = 7;
            this.textBoxInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInput_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "In";
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFile.Location = new System.Drawing.Point(317, 39);
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
            this.textBoxFile.Size = new System.Drawing.Size(299, 20);
            this.textBoxFile.TabIndex = 4;
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(357, 12);
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
            this.buttonDisconnect.Location = new System.Drawing.Point(311, 12);
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
            this.buttonConnnect.Location = new System.Drawing.Point(265, 12);
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
            this.comboBoxCOM.Size = new System.Drawing.Size(247, 21);
            this.comboBoxCOM.TabIndex = 0;
            // 
            // panelGraf
            // 
            this.panelGraf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraf.Location = new System.Drawing.Point(0, 0);
            this.panelGraf.Name = "panelGraf";
            this.panelGraf.Size = new System.Drawing.Size(434, 478);
            this.panelGraf.TabIndex = 0;
            // 
            // serialPortCOM
            // 
            this.serialPortCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortCOM_DataReceived);
            // 
            // comboBoxLF
            // 
            this.comboBoxLF.FormattingEnabled = true;
            this.comboBoxLF.Items.AddRange(new object[] {
            "\\0",
            "\\n"});
            this.comboBoxLF.Location = new System.Drawing.Point(265, 77);
            this.comboBoxLF.Name = "comboBoxLF";
            this.comboBoxLF.Size = new System.Drawing.Size(40, 21);
            this.comboBoxLF.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 478);
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
        private System.Windows.Forms.ComboBox comboBoxLF;
    }
}

