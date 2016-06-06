namespace SPRecordAnalyzer
{
    partial class SPRA_Form
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gpibInterfaceTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSeqStart = new System.Windows.Forms.Button();
            this.textBoxSequencer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownAdress = new System.Windows.Forms.NumericUpDown();
            this.textBoxStringRead = new System.Windows.Forms.TextBox();
            this.labelAdress = new System.Windows.Forms.Label();
            this.labelStringRead = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.labelStringToWrite = new System.Windows.Forms.Label();
            this.textBoxStringToWrite = new System.Windows.Forms.TextBox();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.labelSequencer = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.gpibInterfaceTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdress)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gpibInterfaceTab);
            this.tabControl1.Controls.Add(this.cameraTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(655, 364);
            this.tabControl1.TabIndex = 0;
            // 
            // gpibInterfaceTab
            // 
            this.gpibInterfaceTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gpibInterfaceTab.Controls.Add(this.groupBox2);
            this.gpibInterfaceTab.Controls.Add(this.groupBox1);
            this.gpibInterfaceTab.Location = new System.Drawing.Point(4, 22);
            this.gpibInterfaceTab.Name = "gpibInterfaceTab";
            this.gpibInterfaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.gpibInterfaceTab.Size = new System.Drawing.Size(647, 338);
            this.gpibInterfaceTab.TabIndex = 0;
            this.gpibInterfaceTab.Text = "GPIB Interface";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelSequencer);
            this.groupBox2.Controls.Add(this.buttonSeqStart);
            this.groupBox2.Controls.Add(this.textBoxSequencer);
            this.groupBox2.Location = new System.Drawing.Point(233, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 327);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced";
            // 
            // buttonSeqStart
            // 
            this.buttonSeqStart.Location = new System.Drawing.Point(17, 140);
            this.buttonSeqStart.Name = "buttonSeqStart";
            this.buttonSeqStart.Size = new System.Drawing.Size(75, 23);
            this.buttonSeqStart.TabIndex = 10;
            this.buttonSeqStart.Text = "Start";
            this.buttonSeqStart.UseVisualStyleBackColor = true;
            this.buttonSeqStart.Click += new System.EventHandler(this.buttonSeqStart_Click);
            // 
            // textBoxSequencer
            // 
            this.textBoxSequencer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxSequencer.Location = new System.Drawing.Point(17, 42);
            this.textBoxSequencer.Multiline = true;
            this.textBoxSequencer.Name = "textBoxSequencer";
            this.textBoxSequencer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSequencer.Size = new System.Drawing.Size(179, 92);
            this.textBoxSequencer.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownAdress);
            this.groupBox1.Controls.Add(this.textBoxStringRead);
            this.groupBox1.Controls.Add(this.labelAdress);
            this.groupBox1.Controls.Add(this.labelStringRead);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.buttonRead);
            this.groupBox1.Controls.Add(this.buttonClose);
            this.groupBox1.Controls.Add(this.buttonWrite);
            this.groupBox1.Controls.Add(this.labelStringToWrite);
            this.groupBox1.Controls.Add(this.textBoxStringToWrite);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 331);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basics";
            // 
            // numericUpDownAdress
            // 
            this.numericUpDownAdress.Location = new System.Drawing.Point(99, 18);
            this.numericUpDownAdress.Name = "numericUpDownAdress";
            this.numericUpDownAdress.Size = new System.Drawing.Size(75, 19);
            this.numericUpDownAdress.TabIndex = 1;
            this.numericUpDownAdress.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // textBoxStringRead
            // 
            this.textBoxStringRead.Location = new System.Drawing.Point(18, 221);
            this.textBoxStringRead.Multiline = true;
            this.textBoxStringRead.Name = "textBoxStringRead";
            this.textBoxStringRead.ReadOnly = true;
            this.textBoxStringRead.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStringRead.Size = new System.Drawing.Size(179, 92);
            this.textBoxStringRead.TabIndex = 9;
            // 
            // labelAdress
            // 
            this.labelAdress.AutoSize = true;
            this.labelAdress.Location = new System.Drawing.Point(30, 20);
            this.labelAdress.Name = "labelAdress";
            this.labelAdress.Size = new System.Drawing.Size(43, 12);
            this.labelAdress.TabIndex = 0;
            this.labelAdress.Text = "Adress:";
            // 
            // labelStringRead
            // 
            this.labelStringRead.AutoSize = true;
            this.labelStringRead.Location = new System.Drawing.Point(30, 195);
            this.labelStringRead.Name = "labelStringRead";
            this.labelStringRead.Size = new System.Drawing.Size(63, 12);
            this.labelStringRead.TabIndex = 8;
            this.labelStringRead.Text = "String read:";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(18, 54);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpenGPIB_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(99, 151);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(75, 23);
            this.buttonRead.TabIndex = 7;
            this.buttonRead.Text = "Read";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(99, 54);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Location = new System.Drawing.Point(18, 151);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(75, 23);
            this.buttonWrite.TabIndex = 6;
            this.buttonWrite.Text = "Write";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // labelStringToWrite
            // 
            this.labelStringToWrite.AutoSize = true;
            this.labelStringToWrite.Location = new System.Drawing.Point(30, 102);
            this.labelStringToWrite.Name = "labelStringToWrite";
            this.labelStringToWrite.Size = new System.Drawing.Size(80, 12);
            this.labelStringToWrite.TabIndex = 4;
            this.labelStringToWrite.Text = "String to write:";
            // 
            // textBoxStringToWrite
            // 
            this.textBoxStringToWrite.Location = new System.Drawing.Point(18, 117);
            this.textBoxStringToWrite.Name = "textBoxStringToWrite";
            this.textBoxStringToWrite.Size = new System.Drawing.Size(156, 19);
            this.textBoxStringToWrite.TabIndex = 5;
            this.textBoxStringToWrite.Text = "M:1+P5000";
            // 
            // cameraTab
            // 
            this.cameraTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Padding = new System.Windows.Forms.Padding(3);
            this.cameraTab.Size = new System.Drawing.Size(647, 338);
            this.cameraTab.TabIndex = 1;
            this.cameraTab.Text = "Camera";
            // 
            // labelSequencer
            // 
            this.labelSequencer.AutoSize = true;
            this.labelSequencer.Location = new System.Drawing.Point(15, 22);
            this.labelSequencer.Name = "labelSequencer";
            this.labelSequencer.Size = new System.Drawing.Size(64, 12);
            this.labelSequencer.TabIndex = 12;
            this.labelSequencer.Text = "Sequencer: ";
            // 
            // SPRA_Form
            // 
            this.ClientSize = new System.Drawing.Size(679, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "SPRA_Form";
            this.Text = "SP Record Analyzer";
            this.tabControl1.ResumeLayout(false);
            this.gpibInterfaceTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage GBIB_Tab;
        private System.Windows.Forms.TabPage Camera_Tab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage gpibInterfaceTab;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.TextBox textBoxStringRead;
        private System.Windows.Forms.Label labelStringRead;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.TextBox textBoxStringToWrite;
        private System.Windows.Forms.Label labelStringToWrite;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.NumericUpDown numericUpDownAdress;
        private System.Windows.Forms.Label labelAdress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSeqStart;
        private System.Windows.Forms.TextBox textBoxSequencer;
        private System.Windows.Forms.Label labelSequencer;
    }
}

