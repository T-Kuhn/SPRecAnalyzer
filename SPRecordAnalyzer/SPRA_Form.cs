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
            // This Thread will go one forever. It will only be destroyd when the form is closed.
            while (true)
            {
                if (seqRunning)
                {
                    writeToStage("!:");
                    Thread.Sleep(10);
                    String response = readFromStage();
                    if (response.Contains("R"))
                    {
                        // bus is ready!
                        Debug.WriteLine("Bus is ready and we are doing the thing");
                        if (seqCounter >= splittedString.Length)
                        {
                            // job finished
                            seqRunning = false;
                        }
                        else
                        {
                            // job not finished yet
                            Debug.WriteLine("We write something on the Bus: " + splittedString[seqCounter]);
                            writeToStage(splittedString[seqCounter]);
                            Thread.Sleep(10);
                            writeToStage("G:");
                            Thread.Sleep(10);
                            seqCounter++;
                        }
                    }
                    else
                    {
                        // bus is not ready
                    }
                }
                Thread.Sleep(10);
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
            Debug.WriteLine("This will be sent: " + ReplaceCommonEscapeSequences(txt));
            try
            {
                device.Write(ReplaceCommonEscapeSequences(txt));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Thrown!");
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
