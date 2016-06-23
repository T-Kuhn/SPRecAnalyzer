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
        private CFactory myFactory = new CFactory();

        // Opened camera object
        private CCamera myCamera;

        // GenICam nodes
        private CNode myWidthNode;
        private CNode myHeightNode;
        private CNode myGainNode;
        private CNode myExposureNode;
        private CNode nodeAcquisitionMode;
        private CNode myTriggerSoftwareNode;
        private CNode partialScanNode;

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

        // - - - - - - - - - - - - - - - - -
        // - - - MATLAB FUNCTIONS START  - -
        // - - - - - - - - - - - - - - - - -
        private void buttonStartMatlab_Click(object sender, EventArgs e)
        {
            // Create the MATLAB instance 
            MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd d:\KT\Matlab");

            //textBoxMatlab.Text = "";

            matlab.Execute(@"gcaEx");

            textBoxMatlab.Text = "it works!";

            // Define the output
            //object result = null;

            // Call the MATLAB function myfunc
            //matlab.Feval("gcaEx", 0, out result);

            // Display result
            //object[] res = result as object[];
        }
        // - - - - - - - - - - - - - - - - -
        // - - - MATLAB FUNCTIONS STOP - - -
        // - - - - - - - - - - - - - - - - -

        // - - - - - - - - - - - - - - - - -
        // - - - CAMERA FUNCTIONS START  - -
        // - - - - - - - - - - - - - - - - -
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
                if (swTrigRadio.Checked)
                {
                    buttonTrigger.Enabled = true;
                }
                myCamera.StartImageAcquisition(true, 5);
            }
        }

        private void buttonCameraStop_Click(object sender, EventArgs e)
        {
            if (myCamera != null)
            {
                buttonTrigger.Enabled = false;
                myCamera.StopImageAcquisition();
            }
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

                // Get the Exposure GenICam Node
                myExposureNode = myCamera.GetNode("ExposureTimeRaw");
                if (myExposureNode != null)
                {
                    currentValue = int.Parse(myExposureNode.Value.ToString());

                    // Update range for the TrackBar Controls
                    ExposureTrackBar.Maximum = int.Parse(myExposureNode.Max.ToString());
                    ExposureTrackBar.Minimum = int.Parse(myExposureNode.Min.ToString());
                    ExposureTrackBar.Value = currentValue;
                    ExposureLabel.Text = myExposureNode.Value.ToString();

                    ExposureLabel.Enabled = true;
                    ExposureTrackBar.Enabled = true;
                }
                else
                {
                    ExposureLabel.Enabled = false;
                    ExposureTrackBar.Enabled = false;
                }
                // Get the AcquisitionMode GenICam Node
                nodeAcquisitionMode = myCamera.GetNode("AcquisitionMode");
                if (null != nodeAcquisitionMode)
                {
                    // This thing can either be "Continuous"
                    // or: "SingleFrame"
                    nodeAcquisitionMode.Value = "Continuous";
                    myCamera.AcquisitionCount = UInt32.MaxValue;
                    comboBoxAcquisitionMode.SelectedIndex = 0;
                }
                // Get the partialScanNode GenICam Node
                partialScanNode = myCamera.GetNode("PartialScan");
                if (null != partialScanNode)
                {
                    // This thing can either be "Continuous"
                    // or: "SingleFrame"
                    partialScanNode.Value = "Full Frame";
                    comboBoxPartialScan.SelectedIndex = 0;
                }

                // .. and remember to set the trigger accordingly

                // But we have 2 ways of setting up triggers: JAI and GenICam SNC
                // The GenICam SFNC trigger setup is available if a node called
                // TriggerSelector is available
                if (myCamera.GetNode("TriggerSelector") != null)
                {
                    // Here we assume that this is the GenICam SFNC way of setting up the trigger
                    // To switch to Continuous the following is required:
                    // TriggerSelector=FrameStart
                    // TriggerMode[TriggerSelector]=Off
                    myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                    myCamera.GetNode("TriggerMode").Value = "Off";

                    // Does this camera have a "Software Trigger" feature available?
                    myTriggerSoftwareNode = myCamera.GetNode("TriggerSoftware");
                    if (myTriggerSoftwareNode == null)
                    {
                        swTrigRadio.Enabled = false;
                        buttonTrigger.Enabled = false;
                        MessageBox.Show("No GenICam SFNC Software Trigger found!");
                        return;
                    }
                    else
                    {
                        swTrigRadio.Enabled = true;
                        freeRunRadio.Enabled = true;
                    }
                }
                else
                {
                    // Here we assume that this is the JAI of setting up the trigger
                    // To switch to Continuous the following is required:
                    // ExposureMode=Continuous
                    myCamera.GetNode("ExposureMode").Value = "Continuous";

                    // Does this camera have a "Software Trigger" feature available?
                    myTriggerSoftwareNode = myCamera.GetNode("SoftwareTrigger0");
                    if (myTriggerSoftwareNode == null)
                    {
                        swTrigRadio.Enabled = false;
                        buttonTrigger.Enabled = false;
                        MessageBox.Show("No Software Trigger found!");
                        return;
                    }
                    else
                    {
                        swTrigRadio.Enabled = true;
                    }
                }

                // check free running as default
                freeRunRadio.Checked = true;
                freeRunRadio.Enabled = true;

            }
            else
            {
                buttonCameraStart.Enabled = true;
                buttonCameraStop.Enabled = true;
                WidthNumericUpDown.Enabled = false;
                HeightNumericUpDown.Enabled = true;
                GainLabel.Enabled = false;
                GainTrackBar.Enabled = false;
                ExposureLabel.Enabled = false;
                ExposureTrackBar.Enabled = false;

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
        }

        private void ExposureTrackBar_Scroll(object sender, EventArgs e)
        {
            if (myExposureNode != null)
                myExposureNode.Value = int.Parse(ExposureTrackBar.Value.ToString());

            ExposureLabel.Text = myExposureNode.Value.ToString();
        }
        private void comboBoxAcquisitionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tmpSelInd = comboBoxAcquisitionMode.SelectedIndex;
            if (tmpSelInd == 0)
            {
                nodeAcquisitionMode.Value = "Continuous";

            }else if (tmpSelInd == 1)
            {
                nodeAcquisitionMode.Value = "SingleFrame"; 
            }
        }

        private void freeRunRadio_Click(object sender, EventArgs e)
        {
            // But we have 2 ways of setting up triggers: JAI and GenICam SNC
            // The GenICam SFNC trigger setup is available if a node called
            // TriggerSelector is available
            if (myCamera.GetNode("TriggerSelector") != null)
            {
                // Here we assume that this is the GenICam SFNC way of setting up the trigger
                // To switch to Continuous the following is required:
                // TriggerSelector=FrameStart
                // TriggerMode[TriggerSelector]=Off
                myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                myCamera.GetNode("TriggerMode").Value = "Off";
            }
            else
            {
                // Here we assume that this is the JAI of setting up the trigger
                // To switch to Continuous the following is required:
                // ExposureMode=Continuous
                myCamera.GetNode("ExposureMode").Value = "Continuous";
            }
        }

        private void swTrigRadio_Click(object sender, EventArgs e)
        {
            // Prepare for software trigger:

            // But we have 2 ways of setting up triggers: JAI and GenICam SNC
            // The GenICam SFNC trigger setup is available if a node called
            // TriggerSelector is available
            if (myCamera.GetNode("TriggerSelector") != null)
            {
                // Here we assume that this is the GenICam SFNC way of setting up the trigger
                // To switch to Continuous the following is required:
                // TriggerSelector=FrameStart
                // TriggerMode[TriggerSelector]=On
                // TriggerSource[TriggerSelector]=Software
                myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                myCamera.GetNode("TriggerMode").Value = "On";
                myCamera.GetNode("TriggerSource").Value = "Software";
            }
            else
            {
                // Select triggered mode (not continuous mode)

                // Here we assume that this is the JAI of setting up the trigger
                // To switch to Continuous the following is required:
                // ExposureMode=EdgePreSelect
                // LineSelector=CameraTrigger0
                // LineSource=SoftwareTrigger0
                // LineInverter=ActiveHigh
                myCamera.GetNode("ExposureMode").Value = "EdgePreSelect";

                // Set Line Selector to "Camera Trigger 0"
                myCamera.GetNode("LineSelector").Value = "CameraTrigger0";

                // Set Line Source to "Software Trigger 0"
                myCamera.GetNode("LineSource").Value = "SoftwareTrigger0";

                // .. and finally set the Line Polarity (LineInverter) to "Active High"
                myCamera.GetNode("LineInverter").Value = "ActiveHigh";
            }
        }

        private void buttonTrigger_Click(object sender, EventArgs e)
        {
            // But we have 2 ways of sending a software trigger: JAI and GenICam SNC
            // The GenICam SFNC software trigger is available if a node called
            // TriggerSoftware is available
            if (myCamera.GetNode("TriggerSoftware") != null)
            {
                // Here we assume that this is the GenICam SFNC way of setting up the trigger
                // To switch to Continuous the following is required:
                // TriggerSelector=FrameStart
                // Execute TriggerSoftware[TriggerSelector] command
                myCamera.GetNode("TriggerSelector").Value = "FrameStart";
                myCamera.GetNode("TriggerSoftware").ExecuteCommand();
            }
            else
            {
                // We need to "pulse" the Software Trigger feature in order to trigger the camera!
                myCamera.GetNode("SoftwareTrigger0").Value = 0;
                myCamera.GetNode("SoftwareTrigger0").Value = 1;
                myCamera.GetNode("SoftwareTrigger0").Value = 0;
            }
        }
        private void comboBoxPartialScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            partialScanNode.Value = comboBoxPartialScan.SelectedItem;
            Debug.WriteLine(comboBoxPartialScan.SelectedItem);
            //Full Frame
            //Partial 2/3 lines
            //Partial 1/2 lines
            //Partial 1/4 lines
            //Partial 1/8 lines
            //Variable Partial Scan
        }
        // - - - - - - - - - - - - - - - - -
        // - - - CAMERA FUNCTIONS STOP - - -
        // - - - - - - - - - - - - - - - - -

        private void buttonAIAStart_Click(object sender, EventArgs e)
        {
            AIARunning = true;
            AIAinit = true;
        }

        private void buttonAIAStop_Click(object sender, EventArgs e)
        {
            AIARunning = false;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
