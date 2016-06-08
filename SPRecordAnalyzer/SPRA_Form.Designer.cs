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
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.textBoxMotor2 = new System.Windows.Forms.TextBox();
            this.textBoxMotor1 = new System.Windows.Forms.TextBox();
            this.groupBoxAdvanced = new System.Windows.Forms.GroupBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.labelSequencer = new System.Windows.Forms.Label();
            this.buttonSeqStart = new System.Windows.Forms.Button();
            this.textBoxSequencer = new System.Windows.Forms.TextBox();
            this.groupBoxBasics = new System.Windows.Forms.GroupBox();
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
            this.matlabTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStartMatlab = new System.Windows.Forms.Button();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.GainGroupBox = new System.Windows.Forms.GroupBox();
            this.GainLabel = new System.Windows.Forms.Label();
            this.GainTrackBar = new System.Windows.Forms.TrackBar();
            this.ImageSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonCameraStop = new System.Windows.Forms.Button();
            this.buttonCameraStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CameraIDTextBox = new System.Windows.Forms.TextBox();
            this.unusedTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.gpibInterfaceTab.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.groupBoxAdvanced.SuspendLayout();
            this.groupBoxBasics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdress)).BeginInit();
            this.matlabTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cameraTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).BeginInit();
            this.ImageSizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gpibInterfaceTab);
            this.tabControl1.Controls.Add(this.cameraTab);
            this.tabControl1.Controls.Add(this.matlabTab);
            this.tabControl1.Controls.Add(this.unusedTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 364);
            this.tabControl1.TabIndex = 0;
            // 
            // gpibInterfaceTab
            // 
            this.gpibInterfaceTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gpibInterfaceTab.Controls.Add(this.groupBoxStatus);
            this.gpibInterfaceTab.Controls.Add(this.groupBoxAdvanced);
            this.gpibInterfaceTab.Controls.Add(this.groupBoxBasics);
            this.gpibInterfaceTab.Location = new System.Drawing.Point(4, 22);
            this.gpibInterfaceTab.Name = "gpibInterfaceTab";
            this.gpibInterfaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.gpibInterfaceTab.Size = new System.Drawing.Size(690, 338);
            this.gpibInterfaceTab.TabIndex = 0;
            this.gpibInterfaceTab.Text = "GPIB Interface";
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.label5);
            this.groupBoxStatus.Controls.Add(this.label4);
            this.groupBoxStatus.Controls.Add(this.label3);
            this.groupBoxStatus.Controls.Add(this.label2);
            this.groupBoxStatus.Controls.Add(this.textBoxStatus);
            this.groupBoxStatus.Controls.Add(this.textBoxMotor2);
            this.groupBoxStatus.Controls.Add(this.textBoxMotor1);
            this.groupBoxStatus.Location = new System.Drawing.Point(460, 3);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(221, 330);
            this.groupBoxStatus.TabIndex = 13;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "Motor2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Motor1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Pulse count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Bus Status:";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxStatus.Location = new System.Drawing.Point(106, 33);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatus.Size = new System.Drawing.Size(88, 19);
            this.textBoxStatus.TabIndex = 12;
            this.textBoxStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMotor2
            // 
            this.textBoxMotor2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxMotor2.Location = new System.Drawing.Point(106, 120);
            this.textBoxMotor2.Name = "textBoxMotor2";
            this.textBoxMotor2.ReadOnly = true;
            this.textBoxMotor2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMotor2.Size = new System.Drawing.Size(88, 19);
            this.textBoxMotor2.TabIndex = 11;
            // 
            // textBoxMotor1
            // 
            this.textBoxMotor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxMotor1.Location = new System.Drawing.Point(106, 95);
            this.textBoxMotor1.Name = "textBoxMotor1";
            this.textBoxMotor1.ReadOnly = true;
            this.textBoxMotor1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMotor1.Size = new System.Drawing.Size(88, 19);
            this.textBoxMotor1.TabIndex = 10;
            // 
            // groupBoxAdvanced
            // 
            this.groupBoxAdvanced.Controls.Add(this.textBoxInfo);
            this.groupBoxAdvanced.Controls.Add(this.labelSequencer);
            this.groupBoxAdvanced.Controls.Add(this.buttonSeqStart);
            this.groupBoxAdvanced.Controls.Add(this.textBoxSequencer);
            this.groupBoxAdvanced.Location = new System.Drawing.Point(233, 3);
            this.groupBoxAdvanced.Name = "groupBoxAdvanced";
            this.groupBoxAdvanced.Size = new System.Drawing.Size(221, 330);
            this.groupBoxAdvanced.TabIndex = 11;
            this.groupBoxAdvanced.TabStop = false;
            this.groupBoxAdvanced.Text = "Advanced";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxInfo.Location = new System.Drawing.Point(11, 205);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(204, 105);
            this.textBoxInfo.TabIndex = 10;
            this.textBoxInfo.Text = "m. origin:    H:W-\r\nel. origin:   R:W\r\nvelocity:     D:1S100F1000R100\r\nrel. move:" +
    "    M:W+P1000+P1000\r\nabs. move:    A:W+P1000+P1000\r\njog move:     J:W++\r\nfree mo" +
    "t:     C:W0 / C:W1\r\nstatus:       Q:";
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
            this.textBoxSequencer.Text = "D:1S1000F10000R200\r\nD:2S1000F10000R200\r\nH:W-\r\nM:W+P2000+P1000\r\nM:W+P20000+P10000\r" +
    "\nM:W-P2000-P1000\r\nM:W+P2000+P10000\r\nM:W-P2000-P10000\r\nM:W+P2000+P1000\r\nM:W-P2000" +
    "0-P10000";
            // 
            // groupBoxBasics
            // 
            this.groupBoxBasics.Controls.Add(this.numericUpDownAdress);
            this.groupBoxBasics.Controls.Add(this.textBoxStringRead);
            this.groupBoxBasics.Controls.Add(this.labelAdress);
            this.groupBoxBasics.Controls.Add(this.labelStringRead);
            this.groupBoxBasics.Controls.Add(this.buttonOpen);
            this.groupBoxBasics.Controls.Add(this.buttonRead);
            this.groupBoxBasics.Controls.Add(this.buttonClose);
            this.groupBoxBasics.Controls.Add(this.buttonWrite);
            this.groupBoxBasics.Controls.Add(this.labelStringToWrite);
            this.groupBoxBasics.Controls.Add(this.textBoxStringToWrite);
            this.groupBoxBasics.Location = new System.Drawing.Point(6, 3);
            this.groupBoxBasics.Name = "groupBoxBasics";
            this.groupBoxBasics.Size = new System.Drawing.Size(221, 331);
            this.groupBoxBasics.TabIndex = 10;
            this.groupBoxBasics.TabStop = false;
            this.groupBoxBasics.Text = "Basics";
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
            // matlabTab
            // 
            this.matlabTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.matlabTab.Controls.Add(this.groupBox1);
            this.matlabTab.Location = new System.Drawing.Point(4, 22);
            this.matlabTab.Name = "matlabTab";
            this.matlabTab.Padding = new System.Windows.Forms.Padding(3);
            this.matlabTab.Size = new System.Drawing.Size(690, 338);
            this.matlabTab.TabIndex = 1;
            this.matlabTab.Text = "Matlab";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStartMatlab);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 327);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basics";
            // 
            // buttonStartMatlab
            // 
            this.buttonStartMatlab.Location = new System.Drawing.Point(56, 42);
            this.buttonStartMatlab.Name = "buttonStartMatlab";
            this.buttonStartMatlab.Size = new System.Drawing.Size(75, 23);
            this.buttonStartMatlab.TabIndex = 10;
            this.buttonStartMatlab.Text = "Start";
            this.buttonStartMatlab.UseVisualStyleBackColor = true;
            this.buttonStartMatlab.Click += new System.EventHandler(this.buttonStartMatlab_Click);
            // 
            // cameraTab
            // 
            this.cameraTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cameraTab.Controls.Add(this.groupBox3);
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(690, 338);
            this.cameraTab.TabIndex = 2;
            this.cameraTab.Text = "Camera";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GainGroupBox);
            this.groupBox3.Controls.Add(this.ImageSizeGroupBox);
            this.groupBox3.Controls.Add(this.buttonCameraStop);
            this.groupBox3.Controls.Add(this.buttonCameraStart);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(6, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(536, 327);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basics";
            // 
            // GainGroupBox
            // 
            this.GainGroupBox.Controls.Add(this.GainLabel);
            this.GainGroupBox.Controls.Add(this.GainTrackBar);
            this.GainGroupBox.Location = new System.Drawing.Point(218, 90);
            this.GainGroupBox.Name = "GainGroupBox";
            this.GainGroupBox.Size = new System.Drawing.Size(200, 85);
            this.GainGroupBox.TabIndex = 17;
            this.GainGroupBox.TabStop = false;
            this.GainGroupBox.Text = "Gain Control";
            // 
            // GainLabel
            // 
            this.GainLabel.AutoSize = true;
            this.GainLabel.Enabled = false;
            this.GainLabel.Location = new System.Drawing.Point(12, 19);
            this.GainLabel.Name = "GainLabel";
            this.GainLabel.Size = new System.Drawing.Size(11, 12);
            this.GainLabel.TabIndex = 2;
            this.GainLabel.Text = "0";
            // 
            // GainTrackBar
            // 
            this.GainTrackBar.Enabled = false;
            this.GainTrackBar.Location = new System.Drawing.Point(6, 42);
            this.GainTrackBar.Name = "GainTrackBar";
            this.GainTrackBar.Size = new System.Drawing.Size(187, 45);
            this.GainTrackBar.TabIndex = 1;
            this.GainTrackBar.Scroll += new System.EventHandler(this.GainTrackBar_Scroll);
            // 
            // ImageSizeGroupBox
            // 
            this.ImageSizeGroupBox.Controls.Add(this.label6);
            this.ImageSizeGroupBox.Controls.Add(this.HeightNumericUpDown);
            this.ImageSizeGroupBox.Controls.Add(this.WidthNumericUpDown);
            this.ImageSizeGroupBox.Location = new System.Drawing.Point(12, 90);
            this.ImageSizeGroupBox.Name = "ImageSizeGroupBox";
            this.ImageSizeGroupBox.Size = new System.Drawing.Size(200, 84);
            this.ImageSizeGroupBox.TabIndex = 16;
            this.ImageSizeGroupBox.TabStop = false;
            this.ImageSizeGroupBox.Text = "Image Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Height";
            // 
            // HeightNumericUpDown
            // 
            this.HeightNumericUpDown.Enabled = false;
            this.HeightNumericUpDown.Location = new System.Drawing.Point(74, 44);
            this.HeightNumericUpDown.Name = "HeightNumericUpDown";
            this.HeightNumericUpDown.Size = new System.Drawing.Size(120, 19);
            this.HeightNumericUpDown.TabIndex = 1;
            this.HeightNumericUpDown.ValueChanged += new System.EventHandler(this.HeightNumericUpDown_ValueChanged);
            // 
            // WidthNumericUpDown
            // 
            this.WidthNumericUpDown.Enabled = false;
            this.WidthNumericUpDown.Location = new System.Drawing.Point(74, 18);
            this.WidthNumericUpDown.Name = "WidthNumericUpDown";
            this.WidthNumericUpDown.Size = new System.Drawing.Size(120, 19);
            this.WidthNumericUpDown.TabIndex = 0;
            this.WidthNumericUpDown.ValueChanged += new System.EventHandler(this.WidthNumericUpDown_ValueChanged);
            // 
            // buttonCameraStop
            // 
            this.buttonCameraStop.Enabled = false;
            this.buttonCameraStop.Location = new System.Drawing.Point(454, 125);
            this.buttonCameraStop.Name = "buttonCameraStop";
            this.buttonCameraStop.Size = new System.Drawing.Size(75, 21);
            this.buttonCameraStop.TabIndex = 15;
            this.buttonCameraStop.Text = "Stop";
            this.buttonCameraStop.UseVisualStyleBackColor = true;
            this.buttonCameraStop.Click += new System.EventHandler(this.buttonCameraStop_Click);
            // 
            // buttonCameraStart
            // 
            this.buttonCameraStart.Enabled = false;
            this.buttonCameraStart.Location = new System.Drawing.Point(454, 98);
            this.buttonCameraStart.Name = "buttonCameraStart";
            this.buttonCameraStart.Size = new System.Drawing.Size(75, 21);
            this.buttonCameraStart.TabIndex = 14;
            this.buttonCameraStart.Text = "Start";
            this.buttonCameraStart.UseVisualStyleBackColor = true;
            this.buttonCameraStart.Click += new System.EventHandler(this.buttonCameraStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SearchButton);
            this.groupBox2.Controls.Add(this.CameraIDTextBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 66);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ID of the first camera found";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(488, 16);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(29, 21);
            this.SearchButton.TabIndex = 6;
            this.SearchButton.Text = "...";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // CameraIDTextBox
            // 
            this.CameraIDTextBox.Enabled = false;
            this.CameraIDTextBox.Location = new System.Drawing.Point(6, 18);
            this.CameraIDTextBox.Multiline = true;
            this.CameraIDTextBox.Name = "CameraIDTextBox";
            this.CameraIDTextBox.Size = new System.Drawing.Size(476, 42);
            this.CameraIDTextBox.TabIndex = 0;
            // 
            // unusedTab
            // 
            this.unusedTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.unusedTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.unusedTab.Location = new System.Drawing.Point(4, 22);
            this.unusedTab.Name = "unusedTab";
            this.unusedTab.Size = new System.Drawing.Size(690, 338);
            this.unusedTab.TabIndex = 3;
            this.unusedTab.Text = "Unused Tab";
            // 
            // SPRA_Form
            // 
            this.ClientSize = new System.Drawing.Size(740, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "SPRA_Form";
            this.Text = "SP Record Analyzer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FTV_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.gpibInterfaceTab.ResumeLayout(false);
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.groupBoxAdvanced.ResumeLayout(false);
            this.groupBoxAdvanced.PerformLayout();
            this.groupBoxBasics.ResumeLayout(false);
            this.groupBoxBasics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdress)).EndInit();
            this.matlabTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cameraTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.GainGroupBox.ResumeLayout(false);
            this.GainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).EndInit();
            this.ImageSizeGroupBox.ResumeLayout(false);
            this.ImageSizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TabPage matlabTab;
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
        private System.Windows.Forms.GroupBox groupBoxAdvanced;
        private System.Windows.Forms.GroupBox groupBoxBasics;
        private System.Windows.Forms.Button buttonSeqStart;
        private System.Windows.Forms.TextBox textBoxSequencer;
        private System.Windows.Forms.Label labelSequencer;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.TabPage unusedTab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.TextBox textBoxMotor2;
        private System.Windows.Forms.TextBox textBoxMotor1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStartMatlab;
        private System.Windows.Forms.GroupBox GainGroupBox;
        private System.Windows.Forms.Label GainLabel;
        private System.Windows.Forms.TrackBar GainTrackBar;
        private System.Windows.Forms.GroupBox ImageSizeGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown HeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown WidthNumericUpDown;
        private System.Windows.Forms.Button buttonCameraStop;
        private System.Windows.Forms.Button buttonCameraStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox CameraIDTextBox;
    }
}

