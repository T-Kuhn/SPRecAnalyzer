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
            this.textBoxStringRead = new System.Windows.Forms.TextBox();
            this.labelStringRead = new System.Windows.Forms.Label();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.textBoxStringToWrite = new System.Windows.Forms.TextBox();
            this.labelStringToWrite = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.numericUpDownAdress = new System.Windows.Forms.NumericUpDown();
            this.labelAdress = new System.Windows.Forms.Label();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.gpibInterfaceTab.SuspendLayout();
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
            this.gpibInterfaceTab.Controls.Add(this.textBoxStringRead);
            this.gpibInterfaceTab.Controls.Add(this.labelStringRead);
            this.gpibInterfaceTab.Controls.Add(this.buttonRead);
            this.gpibInterfaceTab.Controls.Add(this.buttonWrite);
            this.gpibInterfaceTab.Controls.Add(this.textBoxStringToWrite);
            this.gpibInterfaceTab.Controls.Add(this.labelStringToWrite);
            this.gpibInterfaceTab.Controls.Add(this.buttonClose);
            this.gpibInterfaceTab.Controls.Add(this.buttonOpen);
            this.gpibInterfaceTab.Controls.Add(this.numericUpDownAdress);
            this.gpibInterfaceTab.Controls.Add(this.labelAdress);
            this.gpibInterfaceTab.Location = new System.Drawing.Point(4, 22);
            this.gpibInterfaceTab.Name = "gpibInterfaceTab";
            this.gpibInterfaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.gpibInterfaceTab.Size = new System.Drawing.Size(647, 338);
            this.gpibInterfaceTab.TabIndex = 0;
            this.gpibInterfaceTab.Text = "GPIB Interface";
            // 
            // textBoxStringRead
            // 
            this.textBoxStringRead.Location = new System.Drawing.Point(16, 223);
            this.textBoxStringRead.Multiline = true;
            this.textBoxStringRead.Name = "textBoxStringRead";
            this.textBoxStringRead.ReadOnly = true;
            this.textBoxStringRead.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStringRead.Size = new System.Drawing.Size(179, 92);
            this.textBoxStringRead.TabIndex = 9;
            // 
            // labelStringRead
            // 
            this.labelStringRead.AutoSize = true;
            this.labelStringRead.Location = new System.Drawing.Point(28, 197);
            this.labelStringRead.Name = "labelStringRead";
            this.labelStringRead.Size = new System.Drawing.Size(63, 12);
            this.labelStringRead.TabIndex = 8;
            this.labelStringRead.Text = "String read:";
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(97, 153);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(75, 23);
            this.buttonRead.TabIndex = 7;
            this.buttonRead.Text = "Read";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Location = new System.Drawing.Point(16, 153);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(75, 23);
            this.buttonWrite.TabIndex = 6;
            this.buttonWrite.Text = "Write";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // textBoxStringToWrite
            // 
            this.textBoxStringToWrite.Location = new System.Drawing.Point(16, 119);
            this.textBoxStringToWrite.Name = "textBoxStringToWrite";
            this.textBoxStringToWrite.Size = new System.Drawing.Size(156, 19);
            this.textBoxStringToWrite.TabIndex = 5;
            this.textBoxStringToWrite.Text = "*idn?\\n";
            // 
            // labelStringToWrite
            // 
            this.labelStringToWrite.AutoSize = true;
            this.labelStringToWrite.Location = new System.Drawing.Point(28, 104);
            this.labelStringToWrite.Name = "labelStringToWrite";
            this.labelStringToWrite.Size = new System.Drawing.Size(80, 12);
            this.labelStringToWrite.TabIndex = 4;
            this.labelStringToWrite.Text = "String to write:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(97, 56);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(16, 56);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpenGPIB_Click);
            // 
            // numericUpDownAdress
            // 
            this.numericUpDownAdress.Location = new System.Drawing.Point(97, 20);
            this.numericUpDownAdress.Name = "numericUpDownAdress";
            this.numericUpDownAdress.Size = new System.Drawing.Size(75, 19);
            this.numericUpDownAdress.TabIndex = 1;
            this.numericUpDownAdress.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // labelAdress
            // 
            this.labelAdress.AutoSize = true;
            this.labelAdress.Location = new System.Drawing.Point(28, 22);
            this.labelAdress.Name = "labelAdress";
            this.labelAdress.Size = new System.Drawing.Size(43, 12);
            this.labelAdress.TabIndex = 0;
            this.labelAdress.Text = "Adress:";
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
            // SPRA_Form
            // 
            this.ClientSize = new System.Drawing.Size(679, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "SPRA_Form";
            this.Text = "SP Record Analyzer";
            this.tabControl1.ResumeLayout(false);
            this.gpibInterfaceTab.ResumeLayout(false);
            this.gpibInterfaceTab.PerformLayout();
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
    }
}

