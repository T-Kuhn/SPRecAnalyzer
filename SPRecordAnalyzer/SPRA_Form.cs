using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Jai_FactoryDotNET;
using NationalInstruments.NI4882;
using Newtonsoft.Json;
using static System.IO.File;

namespace SPRecordAnalyzer
{
    public partial class SPRA_Form : Form
    {
        private Thread stageComThread;
        private Thread imageThread;
        private bool seqRunning;
        private bool calibRunning;
        private bool AIARunning;
        private int AIApulsesTriggerVal;
        private bool AIAinit;
        private string[] splittedString;
        private Device device;
        private int seqCounter;
        private int imgNmbr;
        private bool busReady;
        private double currentPosition_mm;
        private double currentPosition_deg;
        private int currentPosMotor1_pulses;
        private int currentPosMotor2_pulses;
        private string currentPosMotor2_pulses_text;
        private double pulsesPerPixelRatio;
        private int pulsesPerImage;
        private int nmbrOfImagesPerRound;  
        private double betaAngle;
        private int imgWidth;
        private int imgHeight;
        private double imgWidth_mm;
        private double imgHeight_mm;

        private const double ratio_degPerPulse = 0.0025;
        private const double ratio_mmPerPulse = 0.002;

        private delegate void setStatusBoxCallback(string text);
        private delegate void setPosTextMotor1Callback(string text);
        private delegate void setPosTextMotor2Callback(string text);
        private delegate void savebmpCallback();
        private delegate void setDispTextMotor1Callback(string text);
        private delegate void setDispTextMotor2Callback(string text);
        private delegate void setBox1stmeasurmentCallback(string text);
        private delegate void setBox2ndmeasurmentCallback(string text);
        private delegate void setBoxCalibAbsDiffCallback(string text);
        private delegate void setBoxpppratioCallback(string text);
        private delegate void setBoxWidthCallback(string text);
        private delegate void setBoxHeightCallback(string text);
        private delegate void setBoxWidth_mmCallback(string text);
        private delegate void setBoxHeight_mmCallback(string text);
        private delegate void setBoxPulsesPerImageCallback(string text);
        private delegate void setBoxBetaAngleCallback(string text);
        private delegate void setBoxAIAPulseCountM2Callback(string text);
        private delegate void setBoxAIAimageNmbrCallback(string text);


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
        private CNode VariablePartialScanStartLineNode;
        private CNode VariablePartialScanNumOfLinesNode;

        public SPRA_Form()
        {
            InitializeComponent();
            stageComThread = new Thread(sCT);
            imageThread = new Thread(imgThread);
            seqRunning = false;
            pulsesPerPixelRatio = 0.0;
            imgNmbr = 0;
            AIAinit = false;
            // start thread
            stageComThread.Start();
            imageThread.Start();
            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.EFactoryError.Success;
            

            // Open the factory with the default Registry database
            error = myFactory.Open("");

            // Search for cameras and update all controls
            SearchButton_Click(null, null);
            currentPosition_mm = 0.0;
            currentPosition_deg = 0.0;
        }

        private void FTV_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stage
            stageComThread.Abort();
            imageThread.Abort();
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
                    //Thread.Sleep(1);
                    response = readFromStage();
                    responceSplit = response.Split(',');
                    setPosTextMotor1(responceSplit[0]);
                    setDisplTextMotor1(responceSplit[0]);
                    setPosTextMotor2(responceSplit[1]);
                    setDisplTextMotor2(responceSplit[1]);
                    // DIRTY CODE START
                    int val;
                    
                    if (responceSplit[1].Contains("-"))
                    {
                        currentPosMotor2_pulses = -int.Parse(responceSplit[1].Remove(0, 1));
                    }
                    else
                    {
                        currentPosMotor2_pulses = int.Parse(responceSplit[1]);
                    }
                    // DIRTY CODE END
                    writeToStage("!:");
                    //Thread.Sleep(1);
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
            }
        }

        private void imgThread()
        {
            while (true)
            {
                if (calibRunning)
                {
                    Thread.Sleep(22000);
                    Point point1 = getCoordsOfOutermostLine();
                    setBox1stmeasurment(point1.X.ToString());


                    seqRunning = true;
                    seqCounter = 0;
                    string str = "D:2S500F15000R1200" + '\n' +
                                 "M:1-P400";
                    splittedString = str.Split('\n');

                    Thread.Sleep(1000);

                    Point point2 = getCoordsOfOutermostLine();

                    setBox2ndmeasurment(point2.X.ToString());
                    var absdiff = Math.Abs(point2.X - point1.X);

                    //write the Points to the Interface & calculate the PulsePerPixel value
                    setBoxCalibAbsDiff(absdiff.ToString());
                    pulsesPerPixelRatio = (400.0f/absdiff);
                    setBoxpppratio(pulsesPerPixelRatio.ToString().Remove(6));

                    imgHeight_mm = (imgHeight*pulsesPerPixelRatio*ratio_mmPerPulse);
                    imgWidth_mm = (imgWidth*pulsesPerPixelRatio*ratio_mmPerPulse);

                    SetupPPIandBA();

                    if (imgHeight_mm.ToString().Length > 9)
                    {
                        setBoxHeight_mm(imgHeight_mm.ToString().Remove(6));
                    }
                    else
                    {
                        setBoxHeight_mm(imgHeight_mm.ToString());
                    }
                    if (imgWidth_mm.ToString().Length > 9)
                    {
                        setBoxWidth_mm(imgWidth_mm.ToString().Remove(6));
                    }
                    else
                    {
                        setBoxWidth_mm(imgWidth_mm.ToString());
                    }
                    calibRunning = false;
                }
                if (AIARunning)
                {
                    setBoxAIAPulseCountM2(currentPosMotor2_pulses_text);
                    if (AIAinit)
                    {
                        string str = "D:2S200F200R200" + '\n' + "J:2+";
                        splittedString = str.Split('\n');
                        seqRunning = true;
                        seqCounter = 0;
                        imgNmbr = 0;
                        AIApulsesTriggerVal = currentPosMotor2_pulses + pulsesPerImage;
                        AIAinit = false;
                    }
                    else if(nmbrOfImagesPerRound <= imgNmbr)
                    {
                        AIARunning = false;
                        string str = "D:2S200F200R200" + '\n' + "L:2";
                        splittedString = str.Split('\n');
                        seqRunning = true;
                        seqCounter = 0;
                        imgNmbr = 0;
                        AIAinit = false;
                    }
                    else if (currentPosMotor2_pulses >= AIApulsesTriggerVal)
                    {
                        AIApulsesTriggerVal += pulsesPerImage;
                        imgNmbr++;
                        setBoxAIAimageNmbr(imgNmbr.ToString());
                        takePicture("AIA" + imgNmbr);
                    }
                }
            }
        }
        
        private Point getCoordsOfOutermostLine()
        {
            takePicture();

            Thread.Sleep(500);

            helperFunctionMatlab();
            
            return helperFunctionJson();
        }

        private void helperFunctionMatlab()
        {
            // Create the MATLAB instance 
            MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd d:\KT\SPRecordAnalyzer\matlab\getPulsePerPixelData");

            //textBoxMatlab.Text = "";

            matlab.Execute(@"PixelProcessing");
        }

        private Point helperFunctionJson()
        {
            RootObject pixelObjectArr;
            // deserialize JSON directly from a file
            using (StreamReader file = OpenText(@"D:\KT\SPRecordAnalyzer\matlab\getPulsePerPixelData\PixelObjectArr.json"))
            {
                var serializer = new JsonSerializer();
                pixelObjectArr = (RootObject)serializer.Deserialize(file, typeof(RootObject));
                //textBoxMatlab1.Text = pixelObjectArr2.PixelObjectArr.PixelObjects[1].CenterPos[1].ToString();
            }

            var query =
                (from obj in pixelObjectArr.PixelObjectArr.PixelObjects
                    orderby obj.CenterPos[1] descending
                    select obj).ToList();

            Debug.WriteLine(query[0].CenterPos[1]);
            // Here comes the LINQ code to get the outermost thing.
            return new Point(query[0].CenterPos[1], query[0].CenterPos[0]);
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

        private void setBoxAIAimageNmbr(String text)
        {
            if (textBoxAIAImgNumber.InvokeRequired)
            {
                setBoxAIAimageNmbrCallback d = new setBoxAIAimageNmbrCallback(setBoxAIAimageNmbr);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxAIAImgNumber.Text = text;
            }
        }

        private void setBoxAIAPulseCountM2(string text)
        {
            if (textBoxAIAPulseCountM2.InvokeRequired)
            {
                setBoxAIAPulseCountM2Callback d = new setBoxAIAPulseCountM2Callback(setBoxAIAPulseCountM2);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxAIAPulseCountM2.Text = text;
            }
        }

        private void setBoxBetaAngle(string text)
        {
            if (textBoxBetaAngle.InvokeRequired)
            {
                setBoxBetaAngleCallback d = new setBoxBetaAngleCallback(setBoxBetaAngle);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxBetaAngle.Text = text;
            }
        }

        private void setBoxPulsesPerImage(string text)
        {
            if (textBoxPulsesPerImage.InvokeRequired)
            {
                setBoxPulsesPerImageCallback d = new setBoxPulsesPerImageCallback(setBoxPulsesPerImage);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxPulsesPerImage.Text = text;
            }
        }

        private void setBoxHeight_mm(String text)
        {
            if (textBoxHeight_mm.InvokeRequired)
            {
                setBoxHeight_mmCallback d = new setBoxHeight_mmCallback(setBoxHeight_mm);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxHeight_mm.Text = text;
            }
        }

        private void setBoxWidth_mm(String text)
        {
            if (textBoxWidth_mm.InvokeRequired)
            {
                setBoxWidth_mmCallback d = new setBoxWidth_mmCallback(setBoxWidth_mm);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxWidth_mm.Text = text;
            }
        }

        private void setBoxHeight(String text)
        {
            if (textBoxHeight.InvokeRequired)
            {
                setBoxHeightCallback d = new setBoxHeightCallback(setBoxHeight);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxHeight.Text = text;
            }
        }

        private void setBoxWidth(String text)
        {
            if (textBoxWidth.InvokeRequired)
            {
                setBoxWidthCallback d = new setBoxWidthCallback(setBoxWidth);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxWidth.Text = text;
            }
        }

        private void setBox1stmeasurment(String text)
        {
            if (textBoxCalib1stmm.InvokeRequired)
            {
                setBox1stmeasurmentCallback d = new setBox1stmeasurmentCallback(setBox1stmeasurment);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxCalib1stmm.Text = text;
            }
        }

        private void setBox2ndmeasurment(String text)
        {
            if (textBoxCalib2ndmm.InvokeRequired)
            {
                setBox2ndmeasurmentCallback d = new setBox2ndmeasurmentCallback(setBox2ndmeasurment);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxCalib2ndmm.Text = text;
            }
        }

        private void setBoxCalibAbsDiff(String text)
        {
            if (textBoxCalibAbsDiff.InvokeRequired)
            {
                setBoxCalibAbsDiffCallback d = new setBoxCalibAbsDiffCallback(setBoxCalibAbsDiff);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxCalibAbsDiff.Text = text;
            }
        }

        private void setBoxpppratio(String text)
        {
            if (textBoxpppratio.InvokeRequired)
            {
                setBoxpppratioCallback d = new setBoxpppratioCallback(setBoxpppratio);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxpppratio.Text = text;
            }
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
        }

        private void setDisplTextMotor1(String text)
        {
            if (textBoxMotor1Displ.InvokeRequired)
            {
                setDispTextMotor1Callback d = new setDispTextMotor1Callback(setDisplTextMotor1);
                Invoke(d, new object[] {text});
            }
            else
            {
                int val;
                if (text.Contains("-"))
                {
                    text = text.Remove(0, 1);
                    val = int.Parse(text);
                    // 0.002 mm per pulse!
                    currentPosition_mm = -val * ratio_mmPerPulse;
                    currentPosMotor1_pulses = -val;
                }
                else
                {
                    val = int.Parse(text);
                    // 0.002 mm per pulse!
                    currentPosition_mm = val * ratio_mmPerPulse;
                    currentPosMotor1_pulses = val;
                }
                textBoxMotor1Displ.Text = currentPosition_mm.ToString();
            }
        }

        private void setDisplTextMotor2(String text)
        {
            if (textBoxMotor2Displ.InvokeRequired)
            {
                setDispTextMotor2Callback d = new setDispTextMotor2Callback(setDisplTextMotor2);
                Invoke(d, new object[] { text });
            }
            else
            {
                int val;
                if (text.Contains("-"))
                {
                    text = text.Remove(0, 1);
                    val = int.Parse(text);
                    // 0.0025 deg per pulse!
                    currentPosition_deg = -val * ratio_degPerPulse;
                    currentPosMotor2_pulses_text = text;
                }
                else
                {
                    val = int.Parse(text);
                    // 0.0025 deg per pulse!
                    currentPosition_deg = val * ratio_degPerPulse;
                    currentPosMotor2_pulses_text = text;
                }
                textBoxMotor2Displ.Text = currentPosition_deg.ToString();
            }
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
            matlab.Execute(@"cd d:\KT\SPRecordAnalyzer\matlab\getPulsePerPixelData");

            //textBoxMatlab.Text = "";

            matlab.Execute(@"PixelProcessing");

            textBoxMatlab1.Text = "it works!";

            // Define the output
            //object result = null;

            // Call the MATLAB function 
            //matlab.Feval("gcaEx", 0, out result);

            // Display result
            //object[] res = result as object[];


            // read file into a string and deserialize JSON to a type
            PixelObjectArr pixelObjectArr = JsonConvert.DeserializeObject<PixelObjectArr>(ReadAllText(@"D:\KT\SPRecordAnalyzer\matlab\getPulsePerPixelData\PixelObjectArr.json"));

            // deserialize JSON directly from a file
            using(StreamReader file = OpenText(@"D:\KT\SPRecordAnalyzer\matlab\getPulsePerPixelData\PixelObjectArr.json"))
            {
                var serializer = new JsonSerializer();
                var pixelObjectArr2 = (RootObject)serializer.Deserialize(file, typeof(RootObject));
                textBoxMatlab1.Text = pixelObjectArr2.PixelObjectArr.PixelObjects[1].CenterPos[1].ToString();
            }


            //textBoxMatlab.Text = pixelObjectArr.PixelObjects[1].EntryIndex.ToString();
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


                // Rotating the Image 90 degree. doesn't work.
                //myCamera.ImageRotateType = 0;
                //myCamera.RotateImage = true;

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
                if (nodeAcquisitionMode != null)
                {
                    // This thing can either be "Continuous"
                    // or: "SingleFrame"
                    nodeAcquisitionMode.Value = "Continuous";
                    myCamera.AcquisitionCount = UInt32.MaxValue;
                    comboBoxAcquisitionMode.SelectedIndex = 0;
                }
                // Get the partialScanNode GenICam Node
                partialScanNode = myCamera.GetNode("PartialScan");
                if (partialScanNode != null)
                {
                    // This thing can either be "Continuous"
                    // or: "SingleFrame"
                    partialScanNode.Value = "Full Frame";
                    comboBoxPartialScan.SelectedIndex = 0;
                }

                VariablePartialScanStartLineNode = myCamera.GetNode("VariablePartialScanStartLine");
                if (VariablePartialScanStartLineNode != null)
                {
                    //Set the Startvalue to 965
                    VariablePartialScanStartLineNode.Value = "965";
                }

                VariablePartialScanNumOfLinesNode = myCamera.GetNode("VariablePartialScanNumOfLines");
                if (VariablePartialScanNumOfLinesNode != null)
                {
                    //Set the Startvalue to 128
                    VariablePartialScanNumOfLinesNode.Value = "128";
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

                // check swTrigRadio as default
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
                numericUpDownVarPartScanStartLine.Enabled = false;
                numericUpDownVarPartScanNumberOfLines.Enabled = false;

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
            takePicture();
        }

        // - - - - - - - - - - - - - - - - -
        // - - - - TAKE PICTURE  - - - - - -
        // - - - - - - - - - - - - - - - - -
        private void takePicture()
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

            //Thread.Sleep(10);

            int waitcycleCounter = 0;
            ulong nmbr = myCamera.NumFramesDelivered;
            while (nmbr == myCamera.NumFramesDelivered)
            {
                waitcycleCounter++;
                Debug.WriteLine("Waiting: " + waitcycleCounter.ToString());
                // loop until frame available
            }
            Thread.Sleep(10);
            // save the image for matlab
            myCamera.SaveLastRawFrame("D:/KT/SPRecordAnalyzer/matlab/getPulsePerPixelData/img/tmp.bmp", Jai_FactoryWrapper.ESaveFileFormat.Bmp, 100);

            
            // update width and height
            // we also want to get the img width and height here.
            if (myWidthNode != null)
            {
                imgWidth = Int32.Parse(myWidthNode.Value.ToString())-2; // -2 because there is a black pixel frame of size 1 left and right
                setBoxWidth(imgWidth.ToString());
            }
            if (myHeightNode != null)
            {
                imgHeight = Int32.Parse(myHeightNode.Value.ToString())-2; // -2 because there is a black pixel frame of size 1 top and bottom
                setBoxHeight(imgHeight.ToString());
            }
        }

        // - - - - - - - - - - - - - - - - -
        // - - - - TAKE PICTURE  - - - - - - Version with stringparameter for image name
        // - - - - - - - - - - - - - - - - -
        private void takePicture(string str)
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

            //Thread.Sleep(10);

            int waitcycleCounter = 0;
            ulong nmbr = myCamera.NumFramesDelivered;
            while (nmbr == myCamera.NumFramesDelivered)
            {
                waitcycleCounter++;
                Debug.WriteLine("Waiting: " + waitcycleCounter.ToString());
                // loop until frame available
                if (waitcycleCounter > 200)
                {
                    break;
                }
                // or we are above 200
            }
            Thread.Sleep(10);
            // save the image for matlab
            myCamera.SaveLastRawFrame("D:/KT/SPRecordAnalyzer/matlab/getPulsePerPixelData/img/" + str + ".bmp" , 
                Jai_FactoryWrapper.ESaveFileFormat.Bmp, 100);
            // update width and height
            // we also want to get the img width and height here.
            if (myWidthNode != null)
            {
                imgWidth = Int32.Parse(myWidthNode.Value.ToString()) - 2; // -2 because there is a black pixel frame of size 1 left and right
                setBoxWidth(imgWidth.ToString());
            }
            if (myHeightNode != null)
            {
                imgHeight = Int32.Parse(myHeightNode.Value.ToString()) - 2; // -2 because there is a black pixel frame of size 1 top and bottom
                setBoxHeight(imgHeight.ToString());
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
            if (comboBoxPartialScan.SelectedItem == "Variable Partial Scan")
            {
                Debug.WriteLine("Variable Partial Scan was selected.");
                numericUpDownVarPartScanStartLine.Enabled = true;
                numericUpDownVarPartScanNumberOfLines.Enabled = true;
            }
            else
            {
                Debug.WriteLine("Variable Partial Scan was deselected.");
                numericUpDownVarPartScanStartLine.Enabled = false;
                numericUpDownVarPartScanNumberOfLines.Enabled = false;
            }
        }
        private void numericUpDownVarPartScanStartLine_ValueChanged(object sender, EventArgs e)
        {
            VariablePartialScanStartLineNode.Value = int.Parse(numericUpDownVarPartScanStartLine.Value.ToString());
        }

        private void numericUpDownVarPartScanNumberOfLines_ValueChanged(object sender, EventArgs e)
        {
            VariablePartialScanNumOfLinesNode.Value = int.Parse(numericUpDownVarPartScanNumberOfLines.Value.ToString());
        }
        // - - - - - - - - - - - - - - - - -
        // - - - CAMERA FUNCTIONS STOP - - -
        // - - - - - - - - - - - - - - - - -

        private void buttonAIAStart_Click(object sender, EventArgs e)
        {
            AIARunning = true;
            AIAinit = true;
            nmbrOfImagesPerRound = 144000 / pulsesPerImage;
            textBoxTotalImgNmbr.Text = nmbrOfImagesPerRound.ToString();
        }

        private void buttonAIAStop_Click(object sender, EventArgs e)
        {
            AIARunning = false;
            string str = "D:2S200F200R200" + '\n' + "L:2";
            splittedString = str.Split('\n');
            seqRunning = true;
            seqCounter = 0;
            imgNmbr = 0;
            AIAinit = false;
        }

        private void buttonStartCalib_Click(object sender, EventArgs e)
        {
            //INIT SEQUENCE (SEQUENCER1)
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

            calibRunning = true;
        }

        private void buttonUpdateBetaAngle_Click(object sender, EventArgs e)
        {
            SetupPPIandBA();
        }

        private void SetupPPIandBA()
        {
            double tmp = (Math.Atan2(imgHeight_mm, (currentPosition_mm + imgWidth_mm / 2)));
            betaAngle = (tmp * (180.0 / Math.PI));

            setBoxBetaAngle(betaAngle.ToString().Remove(6));

            pulsesPerImage = (int)Math.Round((betaAngle / ratio_degPerPulse), 0);
            setBoxPulsesPerImage(pulsesPerImage.ToString());
        }
    }
}
