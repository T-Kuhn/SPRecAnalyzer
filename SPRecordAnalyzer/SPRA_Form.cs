using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;

namespace SPRecordAnalyzer
{
    public partial class SPRA_Form : Form
    {
        private Thread stageComThread;
        private bool seqRunning;
        private string[] splittedString;
        private Device device;
        private int seqCounter;
        delegate void setStatusBoxCallback(string text);
        delegate void setPosTextMotor1Callback(string text);
        delegate void setPosTextMotor2Callback(string text);

        public SPRA_Form()
        {
            InitializeComponent();
            stageComThread = new Thread(sCT);
            seqRunning = false;
            // start thread
            stageComThread.Start();
        }

        private void FTV_FormClosed(object sender, FormClosedEventArgs e)
        {
            stageComThread.Abort();
        }

        private void sCT()
        {
            String response;
            String[] responceSplit;
            // This Thread will go one forever. It will only be destroyd when the form is closed.
            while (true)
            {
                if (device != null)
                {
                    writeToStage("Q:");
                    Thread.Sleep(5);
                    response = readFromStage();
                    responceSplit = response.Split(',');
                    setPosTextMotor1(responceSplit[0]);
                    setPosTextMotor2(responceSplit[1]);
                }
                if (seqRunning)
                {
                    writeToStage("!:");
                    Thread.Sleep(5);
                    response = readFromStage();
                    if (response.Contains("R"))
                    {
                        // bus is ready!
                        setStatusBox("Ready");
                        if (seqCounter >= splittedString.Length)
                        {
                            // job finished
                            seqRunning = false;
                        }
                        else
                        {
                            // job not finished yet
                            writeToStage(splittedString[seqCounter]);
                            Thread.Sleep(5);
                            writeToStage("G:");
                            Thread.Sleep(5);
                            seqCounter++;
                        }
                    }
                    else
                    {
                        // bus is not ready
                        setStatusBox("Busy");
                    }
                }
                Thread.Sleep(5);
            }
        }

        private
            void buttonOpenGPIB_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Intialize the device
                device = new Device((int)0, (byte)numericUpDownAdress.Value, (byte)0);
                SetupControlState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void SetupControlState(bool isSessionOpen)
        {
            numericUpDownAdress.Enabled = !isSessionOpen;
            buttonOpen.Enabled = !isSessionOpen;
            buttonClose.Enabled = isSessionOpen;
            textBoxStringToWrite.Enabled = isSessionOpen;
            buttonWrite.Enabled = isSessionOpen;
            buttonRead.Enabled = isSessionOpen;
            textBoxStringRead.Enabled = isSessionOpen;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                device.Dispose();
                SetupControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            writeToStage(textBoxStringToWrite.Text);
        }

        /// <summary>
        /// write data to the GPIB bus which is connected to the stage
        /// </summary>
        /// <param name="txt">the data that has to be written to the bus</param>
        private void writeToStage(String txt)
        {
            try
            {
                device.Write(ReplaceCommonEscapeSequences(txt));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// read data from the GPIB bus which is connected to the stage
        /// </summary>
        /// <returns>the data read from the input buffer</returns>
        private String readFromStage()
        {
            String retStr = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                retStr = InsertCommonEscapeSequences(device.ReadString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return retStr;
        }

        private void setStatusBox(String text)
        {
            if (textBoxStatus.InvokeRequired)
            {
                setStatusBoxCallback d = new setStatusBoxCallback(setStatusBox);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxStatus.Text = text;
                if (text == "Ready")
                    textBoxStatus.BackColor = Color.FromArgb(255, 128, 255, 128);
                else
                    textBoxStatus.BackColor = Color.FromArgb(255, 255, 192, 128);
            }
            textBoxStatus.Text = text;
            if (text == "Ready")
                textBoxStatus.BackColor = Color.FromArgb(255, 128, 255, 128);
            else
                textBoxStatus.BackColor = Color.FromArgb(255, 255, 192, 128);
        }

        private void setPosTextMotor1(String text)
        {
            if (textBoxMotor1.InvokeRequired)
            {
                setPosTextMotor1Callback d = new setPosTextMotor1Callback(setPosTextMotor1);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxMotor1.Text = text;
            }
            textBoxMotor1.Text = text;
        }

        private void setPosTextMotor2(String text)
        {
            if (textBoxMotor2.InvokeRequired)
            {
                setPosTextMotor2Callback d = new setPosTextMotor2Callback(setPosTextMotor2);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxMotor2.Text = text;
            }
            textBoxMotor2.Text = text;
        }

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            textBoxStringRead.Text = readFromStage();
        }
        
        private void buttonSeqStart_Click(object sender, EventArgs e)
        {
            string str = textBoxSequencer.Text;
            splittedString = str.Split('\n');
            for (int i = 0; i < splittedString.Length; i++)
            {
                splittedString[i] = splittedString[i].Replace("\r", "");
                splittedString[i] = splittedString[i].Replace("\n", "");
                Debug.WriteLine("String entry : " + splittedString[i]);
            }
            seqRunning = true;
            seqCounter = 0;
            Debug.WriteLine("Length of the splitted string: " + splittedString.Length);
        }

        private void buttonStartMatlab_Click(object sender, EventArgs e)
        {
            // Create the MATLAB instance 
            MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd d:\KT\Matlab");

            matlab.Execute(@"gcaEx");

            // Define the output
            //object result = null;

            // Call the MATLAB function myfunc
            //matlab.Feval("gcaEx", 0, out result);

            // Display result
            //object[] res = result as object[];
        }
    }
}
