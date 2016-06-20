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
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.textBoxMotor2 = new System.Windows.Forms.TextBox();
            this.textBoxMotor1 = new System.Windows.Forms.TextBox();
            this.groupBoxAdvanced = new System.Windows.Forms.GroupBox();
            this.labelSequencer2 = new System.Windows.Forms.Label();
            this.buttonSeq2Start = new System.Windows.Forms.Button();
            this.textBoxSequencer2 = new System.Windows.Forms.TextBox();
            this.labelSequencer1 = new System.Windows.Forms.Label();
            this.buttonSeq1Start = new System.Windows.Forms.Button();
            this.textBoxSequencer1 = new System.Windows.Forms.TextBox();
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
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ExposureLabel = new System.Windows.Forms.Label();
            this.ExposureTrackBar = new System.Windows.Forms.TrackBar();
            this.buttonSaveBMP = new System.Windows.Forms.Button();
            this.GainGroupBox = new System.Windows.Forms.GroupBox();
            this.GainLabel = new System.Windows.Forms.Label();
            this.GainTrackBar = new System.Windows.Forms.TrackBar();
            this.ImageSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonCameraStop = new System.Windows.Forms.Button();
            this.buttonCameraStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CameraIDTextBox = new System.Windows.Forms.TextBox();
            this.matlabTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxMatlab = new System.Windows.Forms.TextBox();
            this.buttonStartMatlab = new System.Windows.Forms.Button();
            this.autImgAcq = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelImageNumber = new System.Windows.Forms.Label();
            this.textBoxImgNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAIAwaitBefImage = new System.Windows.Forms.TextBox();
            this.checkBoxSaveImages = new System.Windows.Forms.CheckBox();
            this.labelAIAloopinstr = new System.Windows.Forms.Label();
            this.textBoxAIAloopInstr = new System.Windows.Forms.TextBox();
            this.labelAIAinit = new System.Windows.Forms.Label();
            this.textBoxAIAinitInstr = new System.Windows.Forms.TextBox();
            this.buttonAIAStop = new System.Windows.Forms.Button();
            this.buttonAIAStart = new System.Windows.Forms.Button();
            this.comboBoxAcquisitionMode = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.gpibInterfaceTab.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.groupBoxAdvanced.SuspendLayout();
            this.groupBoxBasics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdress)).BeginInit();
            this.cameraTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTrackBar)).BeginInit();
            this.GainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).BeginInit();
            this.ImageSizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.matlabTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.autImgAcq.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gpibInterfaceTab);
            this.tabControl1.Controls.Add(this.cameraTab);
            this.tabControl1.Controls.Add(this.matlabTab);
            this.tabControl1.Controls.Add(this.autImgAcq);
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
            this.groupBoxStatus.Controls.Add(this.textBoxInfo);
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
            // textBoxInfo
            // 
            this.textBoxInfo.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxInfo.Location = new System.Drawing.Point(10, 208);
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
            this.groupBoxAdvanced.Controls.Add(this.labelSequencer2);
            this.groupBoxAdvanced.Controls.Add(this.buttonSeq2Start);
            this.groupBoxAdvanced.Controls.Add(this.textBoxSequencer2);
            this.groupBoxAdvanced.Controls.Add(this.labelSequencer1);
            this.groupBoxAdvanced.Controls.Add(this.buttonSeq1Start);
            this.groupBoxAdvanced.Controls.Add(this.textBoxSequencer1);
            this.groupBoxAdvanced.Location = new System.Drawing.Point(233, 3);
            this.groupBoxAdvanced.Name = "groupBoxAdvanced";
            this.groupBoxAdvanced.Size = new System.Drawing.Size(221, 330);
            this.groupBoxAdvanced.TabIndex = 11;
            this.groupBoxAdvanced.TabStop = false;
            this.groupBoxAdvanced.Text = "Advanced";
            // 
            // labelSequencer2
            // 
            this.labelSequencer2.AutoSize = true;
            this.labelSequencer2.Location = new System.Drawing.Point(15, 175);
            this.labelSequencer2.Name = "labelSequencer2";
            this.labelSequencer2.Size = new System.Drawing.Size(70, 12);
            this.labelSequencer2.TabIndex = 15;
            this.labelSequencer2.Text = "Sequencer2: ";
            // 
            // buttonSeq2Start
            // 
            this.buttonSeq2Start.Location = new System.Drawing.Point(17, 293);
            this.buttonSeq2Start.Name = "buttonSeq2Start";
            this.buttonSeq2Start.Size = new System.Drawing.Size(75, 23);
            this.buttonSeq2Start.TabIndex = 13;
            this.buttonSeq2Start.Text = "Start";
            this.buttonSeq2Start.UseVisualStyleBackColor = true;
            this.buttonSeq2Start.Click += new System.EventHandler(this.buttonSeq2Start_Click);
            // 
            // textBoxSequencer2
            // 
            this.textBoxSequencer2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxSequencer2.Location = new System.Drawing.Point(17, 195);
            this.textBoxSequencer2.Multiline = true;
            this.textBoxSequencer2.Name = "textBoxSequencer2";
            this.textBoxSequencer2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSequencer2.Size = new System.Drawing.Size(179, 92);
            this.textBoxSequencer2.TabIndex = 14;
            this.textBoxSequencer2.Text = "D:2S500F15000R1200\r\nJ:2+";
            // 
            // labelSequencer1
            // 
            this.labelSequencer1.AutoSize = true;
            this.labelSequencer1.Location = new System.Drawing.Point(15, 22);
            this.labelSequencer1.Name = "labelSequencer1";
            this.labelSequencer1.Size = new System.Drawing.Size(70, 12);
            this.labelSequencer1.TabIndex = 12;
            this.labelSequencer1.Text = "Sequencer1: ";
            // 
            // buttonSeq1Start
            // 
            this.buttonSeq1Start.Location = new System.Drawing.Point(17, 140);
            this.buttonSeq1Start.Name = "buttonSeq1Start";
            this.buttonSeq1Start.Size = new System.Drawing.Size(75, 23);
            this.buttonSeq1Start.TabIndex = 10;
            this.buttonSeq1Start.Text = "Start";
            this.buttonSeq1Start.UseVisualStyleBackColor = true;
            this.buttonSeq1Start.Click += new System.EventHandler(this.buttonSeqStart_Click);
            // 
            // textBoxSequencer1
            // 
            this.textBoxSequencer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxSequencer1.Location = new System.Drawing.Point(17, 42);
            this.textBoxSequencer1.Multiline = true;
            this.textBoxSequencer1.Name = "textBoxSequencer1";
            this.textBoxSequencer1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSequencer1.Size = new System.Drawing.Size(179, 92);
            this.textBoxSequencer1.TabIndex = 11;
            this.textBoxSequencer1.Text = "D:1S800F15000R1200\r\nD:2S800F15000R1200\r\nH:W-\r\nA:W+P64300+P0";
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
            this.groupBox3.Controls.Add(this.comboBoxAcquisitionMode);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.buttonSaveBMP);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ExposureLabel);
            this.groupBox5.Controls.Add(this.ExposureTrackBar);
            this.groupBox5.Location = new System.Drawing.Point(218, 183);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 85);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Exposure Control";
            // 
            // ExposureLabel
            // 
            this.ExposureLabel.AutoSize = true;
            this.ExposureLabel.Enabled = false;
            this.ExposureLabel.Location = new System.Drawing.Point(12, 19);
            this.ExposureLabel.Name = "ExposureLabel";
            this.ExposureLabel.Size = new System.Drawing.Size(11, 12);
            this.ExposureLabel.TabIndex = 2;
            this.ExposureLabel.Text = "0";
            // 
            // ExposureTrackBar
            // 
            this.ExposureTrackBar.Enabled = false;
            this.ExposureTrackBar.Location = new System.Drawing.Point(6, 42);
            this.ExposureTrackBar.Name = "ExposureTrackBar";
            this.ExposureTrackBar.Size = new System.Drawing.Size(187, 45);
            this.ExposureTrackBar.TabIndex = 1;
            this.ExposureTrackBar.Scroll += new System.EventHandler(this.ExposureTrackBar_Scroll);
            // 
            // buttonSaveBMP
            // 
            this.buttonSaveBMP.Location = new System.Drawing.Point(454, 153);
            this.buttonSaveBMP.Name = "buttonSaveBMP";
            this.buttonSaveBMP.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveBMP.TabIndex = 18;
            this.buttonSaveBMP.Text = "Save Image";
            this.buttonSaveBMP.UseVisualStyleBackColor = true;
            this.buttonSaveBMP.Click += new System.EventHandler(this.buttonSaveBMP_Click);
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
            this.ImageSizeGroupBox.Controls.Add(this.label8);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Width";
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
            this.buttonCameraStop.Size = new System.Drawing.Size(75, 23);
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
            this.buttonCameraStart.Size = new System.Drawing.Size(75, 23);
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
            this.groupBox1.Controls.Add(this.textBoxMatlab);
            this.groupBox1.Controls.Add(this.buttonStartMatlab);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 327);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basics";
            // 
            // textBoxMatlab
            // 
            this.textBoxMatlab.Location = new System.Drawing.Point(43, 93);
            this.textBoxMatlab.Name = "textBoxMatlab";
            this.textBoxMatlab.Size = new System.Drawing.Size(100, 19);
            this.textBoxMatlab.TabIndex = 11;
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
            // autImgAcq
            // 
            this.autImgAcq.BackColor = System.Drawing.Color.WhiteSmoke;
            this.autImgAcq.Controls.Add(this.groupBox4);
            this.autImgAcq.ForeColor = System.Drawing.SystemColors.ControlText;
            this.autImgAcq.Location = new System.Drawing.Point(4, 22);
            this.autImgAcq.Name = "autImgAcq";
            this.autImgAcq.Size = new System.Drawing.Size(690, 338);
            this.autImgAcq.TabIndex = 3;
            this.autImgAcq.Text = "Automatic Image Acq.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelImageNumber);
            this.groupBox4.Controls.Add(this.textBoxImgNumber);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxAIAwaitBefImage);
            this.groupBox4.Controls.Add(this.checkBoxSaveImages);
            this.groupBox4.Controls.Add(this.labelAIAloopinstr);
            this.groupBox4.Controls.Add(this.textBoxAIAloopInstr);
            this.groupBox4.Controls.Add(this.labelAIAinit);
            this.groupBox4.Controls.Add(this.textBoxAIAinitInstr);
            this.groupBox4.Controls.Add(this.buttonAIAStop);
            this.groupBox4.Controls.Add(this.buttonAIAStart);
            this.groupBox4.Location = new System.Drawing.Point(6, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(212, 332);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Basics";
            // 
            // labelImageNumber
            // 
            this.labelImageNumber.AutoSize = true;
            this.labelImageNumber.Location = new System.Drawing.Point(17, 154);
            this.labelImageNumber.Name = "labelImageNumber";
            this.labelImageNumber.Size = new System.Drawing.Size(80, 12);
            this.labelImageNumber.TabIndex = 30;
            this.labelImageNumber.Text = "Image Number:";
            // 
            // textBoxImgNumber
            // 
            this.textBoxImgNumber.Location = new System.Drawing.Point(108, 151);
            this.textBoxImgNumber.Name = "textBoxImgNumber";
            this.textBoxImgNumber.ReadOnly = true;
            this.textBoxImgNumber.Size = new System.Drawing.Size(79, 19);
            this.textBoxImgNumber.TabIndex = 29;
            this.textBoxImgNumber.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "wait time in ms";
            // 
            // textBoxAIAwaitBefImage
            // 
            this.textBoxAIAwaitBefImage.Location = new System.Drawing.Point(108, 183);
            this.textBoxAIAwaitBefImage.Name = "textBoxAIAwaitBefImage";
            this.textBoxAIAwaitBefImage.Size = new System.Drawing.Size(79, 19);
            this.textBoxAIAwaitBefImage.TabIndex = 27;
            this.textBoxAIAwaitBefImage.Text = "1200";
            // 
            // checkBoxSaveImages
            // 
            this.checkBoxSaveImages.AutoSize = true;
            this.checkBoxSaveImages.Location = new System.Drawing.Point(22, 122);
            this.checkBoxSaveImages.Name = "checkBoxSaveImages";
            this.checkBoxSaveImages.Size = new System.Drawing.Size(88, 16);
            this.checkBoxSaveImages.TabIndex = 25;
            this.checkBoxSaveImages.Text = "save Images";
            this.checkBoxSaveImages.UseVisualStyleBackColor = true;
            // 
            // labelAIAloopinstr
            // 
            this.labelAIAloopinstr.AutoSize = true;
            this.labelAIAloopinstr.Location = new System.Drawing.Point(17, 79);
            this.labelAIAloopinstr.Name = "labelAIAloopinstr";
            this.labelAIAloopinstr.Size = new System.Drawing.Size(86, 12);
            this.labelAIAloopinstr.TabIndex = 23;
            this.labelAIAloopinstr.Text = "loop instruction:";
            // 
            // textBoxAIAloopInstr
            // 
            this.textBoxAIAloopInstr.Location = new System.Drawing.Point(19, 94);
            this.textBoxAIAloopInstr.Name = "textBoxAIAloopInstr";
            this.textBoxAIAloopInstr.Size = new System.Drawing.Size(156, 19);
            this.textBoxAIAloopInstr.TabIndex = 24;
            this.textBoxAIAloopInstr.Text = "M:2+P600";
            // 
            // labelAIAinit
            // 
            this.labelAIAinit.AutoSize = true;
            this.labelAIAinit.Location = new System.Drawing.Point(17, 28);
            this.labelAIAinit.Name = "labelAIAinit";
            this.labelAIAinit.Size = new System.Drawing.Size(93, 12);
            this.labelAIAinit.TabIndex = 21;
            this.labelAIAinit.Text = "initial instruction:";
            // 
            // textBoxAIAinitInstr
            // 
            this.textBoxAIAinitInstr.Location = new System.Drawing.Point(19, 43);
            this.textBoxAIAinitInstr.Name = "textBoxAIAinitInstr";
            this.textBoxAIAinitInstr.Size = new System.Drawing.Size(156, 19);
            this.textBoxAIAinitInstr.TabIndex = 22;
            this.textBoxAIAinitInstr.Text = "D:2S800F10000R100";
            // 
            // buttonAIAStop
            // 
            this.buttonAIAStop.Location = new System.Drawing.Point(112, 218);
            this.buttonAIAStop.Name = "buttonAIAStop";
            this.buttonAIAStop.Size = new System.Drawing.Size(75, 23);
            this.buttonAIAStop.TabIndex = 20;
            this.buttonAIAStop.Text = "Stop";
            this.buttonAIAStop.UseVisualStyleBackColor = true;
            this.buttonAIAStop.Click += new System.EventHandler(this.buttonAIAStop_Click);
            // 
            // buttonAIAStart
            // 
            this.buttonAIAStart.Location = new System.Drawing.Point(21, 218);
            this.buttonAIAStart.Name = "buttonAIAStart";
            this.buttonAIAStart.Size = new System.Drawing.Size(75, 23);
            this.buttonAIAStart.TabIndex = 19;
            this.buttonAIAStart.Text = "Start";
            this.buttonAIAStart.UseVisualStyleBackColor = true;
            this.buttonAIAStart.Click += new System.EventHandler(this.buttonAIAStart_Click);
            // 
            // comboBoxAcquisitionMode
            // 
            this.comboBoxAcquisitionMode.FormattingEnabled = true;
            this.comboBoxAcquisitionMode.Items.AddRange(new object[] {
            "Continuous",
            "SingleFrame"});
            this.comboBoxAcquisitionMode.Location = new System.Drawing.Point(12, 194);
            this.comboBoxAcquisitionMode.Name = "comboBoxAcquisitionMode";
            this.comboBoxAcquisitionMode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxAcquisitionMode.TabIndex = 19;
            this.comboBoxAcquisitionMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcquisitionMode_SelectedIndexChanged);
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
            this.cameraTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTrackBar)).EndInit();
            this.GainGroupBox.ResumeLayout(false);
            this.GainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GainTrackBar)).EndInit();
            this.ImageSizeGroupBox.ResumeLayout(false);
            this.ImageSizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.matlabTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.autImgAcq.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Button buttonSeq1Start;
        private System.Windows.Forms.TextBox textBoxSequencer1;
        private System.Windows.Forms.Label labelSequencer1;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.TabPage autImgAcq;
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
        private System.Windows.Forms.Label labelSequencer2;
        private System.Windows.Forms.Button buttonSeq2Start;
        private System.Windows.Forms.TextBox textBoxSequencer2;
        private System.Windows.Forms.Button buttonSaveBMP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonAIAStop;
        private System.Windows.Forms.Button buttonAIAStart;
        private System.Windows.Forms.Label labelAIAinit;
        private System.Windows.Forms.TextBox textBoxAIAinitInstr;
        private System.Windows.Forms.Label labelAIAloopinstr;
        private System.Windows.Forms.TextBox textBoxAIAloopInstr;
        private System.Windows.Forms.CheckBox checkBoxSaveImages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAIAwaitBefImage;
        private System.Windows.Forms.Label labelImageNumber;
        private System.Windows.Forms.TextBox textBoxImgNumber;
        private System.Windows.Forms.Button buttonStartMatlab;
        public System.Windows.Forms.TextBox textBoxMatlab;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label ExposureLabel;
        private System.Windows.Forms.TrackBar ExposureTrackBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxAcquisitionMode;
    }
}

