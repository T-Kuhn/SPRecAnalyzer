using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jai_FactoryDotNET;
using NationalInstruments.NI4882;

namespace SPRecordAnalyzer
{
    public partial class SPRA_Form : Form
    {
        private Thread stageComThread;
        private bool seqRunning;
        private bool AIARunning;
        private bool AIAinit;
        private string[] splittedString;
        private Device device;
        private int seqCounter;
        private int imgNmbr;
        private bool busReady;
        delegate void setStatusBoxCallback(string text);
        delegate void setPosTextMotor1Callback(string text);
        delegate void setPosTextMotor2Callback(string text);
        delegate void savebmpCallback();
        // Main factory object
        CFactory myFactory = new CFactory();

        // Opened camera object
        CCamera myCamera;

        // GenICam nodes
        CNode myWidthNode;
        CNode myHeightNode;
        CNode myGainNode;

        public SPRA_Form()
        {
            InitializeComponent();
            stageComThread = new Thread(sCT);
            seqRunning = false;
            imgNmbr = 0;
            // start thread
            stageComThread.Start();
            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.EFactoryError.Success;

            // Open the factory with the default Registry database
            error = myFactory.Open("");

            // Search for cameras and update all controls
            SearchButton_Click(null, null);
        }

        private void FTV_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stage
            stageComThread.Abort();
            // Camera
            if (myCamera != null)
            {
                buttonCameraStop_Click(null, null);
                myCamera.Close();
            }
        }

        private void sCT()
        {
            String response;
            String[] responceSplit;
            // This Thread will go one forever. It will only be destroyd when the form is closed.
            while (true)
            {
                // GPIB communication
                if (device != null)
                {
                    writeToStage("Q:");
                    Thread.Sleep(5);
                    response = readFromStage();
                    responceSplit = response.Split(',');
                    setPosTextMotor1(responceSplit[0]);
                    setPosTextMotor2(responceSplit[1]);
                    writeToStage("!:");
                    Thread.Sleep(5);
                    response = readFromStage();
                    busReady = response.Contains("R");
                }
                if (seqRunning)
                {
                    if (busReady)
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

                // Automatic Image Acquisition
                if (device != null && myCamera != null)
                {
                    if (busReady && AIARunning)
                    {
                        if (!AIAinit)
                        {
                            writeToStage(textBoxAIAinitInstr.Text);
                            Thread.Sleep(5);
                            AIAinit = false;
                        }
                        else
                        {
                            writeToStage(textBoxAIAloopInstr.Text);
                            Thread.Sleep(5);
                            writeToStage("G:");
                            int i;
                            Int32.TryParse(textBoxAIAwaitBefImage.Text, out i);
                            Thread.Sleep(i);
                            if(checkBoxSaveImages.Checked)
                                Savebmp();
                        }
                    }
                        
                }
                Thread.Sleep(5);
            }
        }

        private void buttonOpenGPIB_Click(object sender, EventArgs e)
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
            string str = textBoxSequencer1.Text;
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

        private void buttonSeq2Start_Click(object sender, EventArgs e)
        {
            string str = textBoxSequencer2.Text;
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
        // CAMERA FUNCTIONS START
        private void WidthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (myWidthNode != null)
            {
                myWidthNode.Value = int.Parse(WidthNumericUpDown.Value.ToString());
                SetFramegrabberValue("Width", (Int64)myWidthNode.Value);
            }
        }

        private void HeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (myHeightNode != null)
            {
                myHeightNode.Value = int.Parse(HeightNumericUpDown.Value.ToString());
                SetFramegrabberValue("Height", (Int64)myHeightNode.Value);
            }
        }

        private void GainTrackBar_Scroll(object sender, EventArgs e)
        {
            if (myGainNode != null)
                myGainNode.Value = int.Parse(GainTrackBar.Value.ToString());

            GainLabel.Text = myGainNode.Value.ToString();
        }

        private void buttonSaveBMP_Click(object sender, EventArgs e)
        {
            myCamera.SaveLastRawFrame("D:/KT/img/tmp.bmp", Jai_FactoryWrapper.ESaveFileFormat.Bmp, 100);
        }

        private void Savebmp()
        {
            String text = imgNmbr.ToString();
            myCamera.SaveLastRawFrame("D:/KT/img/Image" + imgNmbr + ".bmp", Jai_FactoryWrapper.ESaveFileFormat.Bmp, 100);
            if (textBoxImgNumber.InvokeRequired)
            {
                savebmpCallback d = new savebmpCallback(Savebmp);
                Invoke(d);
            }
            else
            {
                textBoxImgNumber.Text = text;
                imgNmbr++;
            }
            
        }

        private void buttonCameraStart_Click(object sender, EventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.StartImageAcquisition(true, 5);
            }
        }

        private void buttonCameraStop_Click(object sender, EventArgs e)
        {
            if (myCamera != null)
                myCamera.StopImageAcquisition();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (null != myCamera)
            {
                if (myCamera.IsOpen)
                {
                    myCamera.Close();
                }

                myCamera = null;
            }

            // Discover GigE and/or generic GenTL devices using myFactory.UpdateCameraList(in this case specifying Filter Driver for GigE cameras).
            myFactory.UpdateCameraList(Jai_FactoryDotNET.CFactory.EDriverType.FilterDriver);

            // Open the camera - first check for GigE devices
            for (int i = 0; i < myFactory.CameraList.Count; i++)
            {
                myCamera = myFactory.CameraList[i];
                if (Jai_FactoryWrapper.EFactoryError.Success == myCamera.Open())
                {
                    break;
                }
            }

            if (null != myCamera && myCamera.IsOpen)
            {
                CameraIDTextBox.Text = myCamera.CameraID;

                if (myCamera.NumOfDataStreams > 0)
                {
                    buttonCameraStart.Enabled = true;
                    buttonCameraStop.Enabled = true;
                }
                else
                {
                    buttonCameraStart.Enabled = false;
                    buttonCameraStop.Enabled = false;
                }

                int currentValue = 0;

                // Get the Width GenICam Node
                myWidthNode = myCamera.GetNode("Width");
                if (myWidthNode != null)
                {
                    currentValue = int.Parse(myWidthNode.Value.ToString());

                    // Update range for the Numeric Up/Down control
                    // Convert from integer to Decimal type
                    WidthNumericUpDown.Maximum = decimal.Parse(myWidthNode.Max.ToString());
                    WidthNumericUpDown.Minimum = decimal.Parse(myWidthNode.Min.ToString());
                    WidthNumericUpDown.Value = decimal.Parse(currentValue.ToString());

                    WidthNumericUpDown.Enabled = true;
                }
                else
                    WidthNumericUpDown.Enabled = false;

                SetFramegrabberValue("Width", (Int64)myWidthNode.Value);

                // Get the Height GenICam Node
                myHeightNode = myCamera.GetNode("Height");
                if (myHeightNode != null)
                {
                    currentValue = int.Parse(myHeightNode.Value.ToString());

                    // Update range for the Numeric Up/Down control
                    // Convert from integer to Decimal type
                    HeightNumericUpDown.Maximum = decimal.Parse(myHeightNode.Max.ToString());
                    HeightNumericUpDown.Minimum = decimal.Parse(myHeightNode.Min.ToString());
                    HeightNumericUpDown.Value = decimal.Parse(currentValue.ToString());

                    HeightNumericUpDown.Enabled = true;
                }
                else
                    HeightNumericUpDown.Enabled = false;

                SetFramegrabberValue("Height", (Int64)myHeightNode.Value);

                SetFramegrabberPixelFormat();

                // Get the GainRaw GenICam Node
                myGainNode = myCamera.GetNode("GainRaw");
                if (myGainNode != null)
                {
                    currentValue = int.Parse(myGainNode.Value.ToString());

                    // Update range for the TrackBar Controls
                    GainTrackBar.Maximum = int.Parse(myGainNode.Max.ToString());
                    GainTrackBar.Minimum = int.Parse(myGainNode.Min.ToString());
                    GainTrackBar.Value = currentValue;
                    GainLabel.Text = myGainNode.Value.ToString();

                    GainLabel.Enabled = true;
                    GainTrackBar.Enabled = true;
                }
                else
                {
                    GainLabel.Enabled = false;
                    GainTrackBar.Enabled = false;
                }
            }
            else
            {
                buttonCameraStart.Enabled = true;
                buttonCameraStop.Enabled = true;
                WidthNumericUpDown.Enabled = false;
                HeightNumericUpDown.Enabled = true;
                GainLabel.Enabled = false;
                GainTrackBar.Enabled = false;

                MessageBox.Show("No Cameras Found!");
            }
        }

        private void SetFramegrabberValue(String nodeName, Int64 int64Val)
        {
            if (null == myCamera)
            {
                return;
            }

            IntPtr hDevice = IntPtr.Zero;
            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.J_Camera_GetLocalDeviceHandle(myCamera.CameraHandle, ref hDevice);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            if (IntPtr.Zero == hDevice)
            {
                return;
            }

            IntPtr hNode;
            error = Jai_FactoryWrapper.J_Camera_GetNodeByName(hDevice, nodeName, out hNode);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            if (IntPtr.Zero == hNode)
            {
                return;
            }

            error = Jai_FactoryWrapper.J_Node_SetValueInt64(hNode, false, int64Val);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            //Special handling for Active Silicon CXP boards, which also has nodes prefixed
            //with "Incoming":
            if ("Width" == nodeName || "Height" == nodeName)
            {
                string strIncoming = "Incoming" + nodeName;
                IntPtr hNodeIncoming;
                error = Jai_FactoryWrapper.J_Camera_GetNodeByName(hDevice, strIncoming, out hNodeIncoming);
                if (Jai_FactoryWrapper.EFactoryError.Success != error)
                {
                    return;
                }

                if (IntPtr.Zero == hNodeIncoming)
                {
                    return;
                }

                error = Jai_FactoryWrapper.J_Node_SetValueInt64(hNodeIncoming, false, int64Val);
            }
        }

        private void SetFramegrabberPixelFormat()
        {
            String nodeName = "PixelFormat";

            if (null == myCamera)
            {
                return;
            }

            IntPtr hDevice = IntPtr.Zero;
            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.J_Camera_GetLocalDeviceHandle(myCamera.CameraHandle, ref hDevice);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            if (IntPtr.Zero == hDevice)
            {
                return;
            }

            long pf = 0;
            error = Jai_FactoryWrapper.J_Camera_GetValueInt64(myCamera.CameraHandle, nodeName, ref pf);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }
            UInt64 pixelFormat = (UInt64)pf;

            UInt64 jaiPixelFormat = 0;
            error = Jai_FactoryWrapper.J_Image_Get_PixelFormat(myCamera.CameraHandle, pixelFormat, ref jaiPixelFormat);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            StringBuilder sbJaiPixelFormatName = new StringBuilder(512);
            uint iSize = (uint)sbJaiPixelFormatName.Capacity;
            error = Jai_FactoryWrapper.J_Image_Get_PixelFormatName(myCamera.CameraHandle, jaiPixelFormat, sbJaiPixelFormatName, iSize);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            IntPtr hNode;
            error = Jai_FactoryWrapper.J_Camera_GetNodeByName(hDevice, nodeName, out hNode);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            if (IntPtr.Zero == hNode)
            {
                return;
            }

            error = Jai_FactoryWrapper.J_Node_SetValueString(hNode, false, sbJaiPixelFormatName.ToString());
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            //Special handling for Active Silicon CXP boards, which also has nodes prefixed
            //with "Incoming":
            string strIncoming = "Incoming" + nodeName;
            IntPtr hNodeIncoming;
            error = Jai_FactoryWrapper.J_Camera_GetNodeByName(hDevice, strIncoming, out hNodeIncoming);
            if (Jai_FactoryWrapper.EFactoryError.Success != error)
            {
                return;
            }

            if (IntPtr.Zero == hNodeIncoming)
            {
                return;
            }

            error = Jai_FactoryWrapper.J_Node_SetValueString(hNodeIncoming, false, sbJaiPixelFormatName.ToString());
        }// CAMERA FUNCTIONS STOP

        private void buttonAIAStart_Click(object sender, EventArgs e)
        {
            AIARunning = true;
            AIAinit = true;
        }

        private void buttonAIAStop_Click(object sender, EventArgs e)
        {
            AIARunning = false;
        }
    }
}
