using LED_Autocorrelator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using PI;
using Ivi.Visa;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Runtime.ConstrainedExecution;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Xml.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using MathNet.Numerics.Statistics;

namespace LED_Autocorrelator
{
    public partial class Form1 : Form
    {
        // Define application variables
        // MOTOR
        ProgramLogic.ApplicationSettings applicationSettings = new ProgramLogic.ApplicationSettings();
        private StringBuilder connectedUsbController = new StringBuilder(1024);
        internal int ControllerId = -1;
        private const int PI_RESULT_FAILURE = 0;
        private const int PI_TRUE = 1;
        private static readonly string Axis = "1";
        private const int PI_FALSE = 0;
        private double defaultMotorSpeed = 25; // mm/s
        // SCOPE
        private string scopeName = "USB0::0x0AAD::0x01D6::107415::INSTR";//"USB0::0x0AAD::0x01D6::107415::INSTR";
        IMessageBasedSession io;
        private double timeBase = 1e-7;
        private double voltsPerDiv = 100e-3;


        // Running scans
        private bool isRunningContinuously = false;
        private Thread _basicScanThread;
        string fileName;
        string fitParametersFilePath = @"""C:\Users\jdm24\source\repos\LED Autocorrelator\Scripts\Fits\fitParameters.csv""";
        string pulseDurationFilename;
        int numberOfProgressBarIncrementsInScan = 10;

        public Form1()
        {
            InitializeComponent();
        }

        // ******************* //
        // *General functions* //
        // ******************* //

        private void StartupFcn()
        {
            LogMessage("Starting application ...");
            SetApplicationSettings(applicationSettings);
            LoadAvailableUSBs();
            if (motorUSBComboBox.Items.Count > 0)
            {
                motorUSBComboBox.SelectedIndex = 0;
            }
            else { LogMessage("No motors appear to be connected..."); }
            try
            {
                LogMessage("Connecting to scope ...");
                GetScopeName();
                LogMessage($"Scope name: {scopeName}");
                //LogMessage($"Scope found: {scopeName}");
                scopeComboBox.Items.Add(scopeName);
                if (scopeComboBox.Items.Count > 0)
                {
                    scopeComboBox.SelectedIndex = 0;
                }
            }
            catch { NotificationMessage("No scope found"); }
            //try
            //{
            //    ConnectToScope(scopeName);
            //}
            //catch { NotificationMessage("Scope not connected"); }
            saveLocationTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        }

        public void ErrorMessage(string error)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(ErrorMessage), new object[] { error });
                return;
            }
            MessageBox.Select(MessageBox.TextLength, 0);
            MessageBox.SelectionColor = System.Drawing.Color.Red;
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MMM-dd HH:mm:ss");
            MessageBox.AppendText($"{formattedDateTime} - ERROR: {error}");
            MessageBox.AppendText(Environment.NewLine);
            MessageBox.ScrollToCaret();
        }

        private void SetApplicationSettings(ProgramLogic.ApplicationSettings application)
        {
            try
            {
                timeBase = Settings.Default.timeBase;
                voltsPerDiv = Settings.Default.voltsPerDivisionChannel1;
                setTimeBaseNumericUpDown.Value = (decimal)timeBase;
                voltsPerDivNumericUpDown.Value = (decimal)voltsPerDiv;
                motorStartPositionNumericUpDown.Value = Settings.Default.motorStartPosition;
                motorEndPositionNumericUpDown.Value = Settings.Default.motorEndPosition;
                noScanPointsNumericUpDown.Value = Settings.Default.noScanPoints;
                fitGaussianCheckBox.Checked = Settings.Default.fitGaussian;
                LogMessage("Last user settings applied successfully.");
            }
            catch
            {
                ErrorMessage("Unable to set application settings");
            }
        }

        public void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(LogMessage), new object[] { message });
                return;
            }
            MessageBox.Select(MessageBox.TextLength, 0);
            MessageBox.SelectionColor = System.Drawing.Color.Black;
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MMM-dd HH:mm:ss");
            MessageBox.AppendText($"{formattedDateTime} - {message}");
            MessageBox.AppendText(Environment.NewLine);
            MessageBox.ScrollToCaret();
        }

        public void NotificationMessage(string message)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(NotificationMessage), new object[] { message });
                return;
            }
            notificationTextBox.Text = $"{message}";
            notificationTextBox.ScrollToCaret();
        }

        // **************************** //
        // *User interaction functions* //
        // **************************** //
        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to exit?", "Confirm exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DisableMotor();
                this.Close();
                Application.Exit();
            }
        }

        private void ClearMessageBoxButton_Click(object sender, EventArgs e)
        {
            MessageBox.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StartupFcn();
                LogMessage("Application started");
            }
            catch
            {
                ErrorMessage("Application startup failed");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(); // Create an instance of SettingsForm
            settingsForm.Show();
        }

        private void ConnectToMotor(StringBuilder connectedUsbController)
        {
            LogMessage("Connecting to motor ...");
            NotificationMessage("Connecting to motor ...");
            if (IsMotorConnected() == true)
            {
                GCS2.CloseConnection(ControllerId);
                ControllerId = -1;
                LogMessage($"Motor {connectedUsbController} disconnected");
                connectedUsbController.Clear();
            }
            //LogMessage("Connecting to motor");
            try
            {
                ControllerId = GCS2.ConnectUSB(
                        connectedUsbController
                            .ToString()
                            .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                            [0]);
                if (ControllerId == -1)
                {
                    ErrorMessage("Unable to connect to motor");
                    NotificationMessage("Unable to connect to motor");
                }
                else
                {
                    LogMessage($"Motor {connectedUsbController} connected");
                    ConfigureMotor();
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
            }
        }

        private bool ConfigureMotor(int configureState = 1)
        {
            //LogMessage("Inside ConfigureMotor");
            try
            {
                int[] referencedMode = new int[] { configureState };
                int[] servoMode = new int[] { configureState };
                int[] motorMode = new int[] { configureState };
                if (GCS2.qFRF(ControllerId, "1", referencedMode) == PI_RESULT_FAILURE)
                {
                    NotificationMessage("Unable to configure motor: Unreferenced motor.");
                    return false;
                    //throw new GcsCommandError("Unable to query reference status");
                }
                if (referencedMode[0] == 0)
                {
                    NotificationMessage("Motor not referenced. Referencing motor...");
                    if (GCS2.FRF(ControllerId, "1") == PI_RESULT_FAILURE)
                    {
                        throw new GcsCommandError("Unable to reference motor");
                    }
                    else
                    {
                        NotificationMessage("Motor referenced successfully");
                    }
                }

                if (GCS2.EAX(ControllerId, "1", motorMode) == PI_RESULT_FAILURE)
                {
                    throw new GcsCommandError("Unable to set motor status");
                }
                if (GCS2.SVO(ControllerId, "1", servoMode) == PI_RESULT_FAILURE)
                {
                    throw new GcsCommandError("Unable to set servo status");
                }
                if (GCS2.HLT(ControllerId, "1") == PI_RESULT_FAILURE)
                {
                    throw new GcsCommandError("Unable to set servo status");
                }
                else { NotificationMessage("Motor motion set to smooth"); }



                if ((ControllerId == -1) || (servoMode[0] != configureState) || (motorMode[0] != configureState))
                {
                    ErrorMessage($"Motor not configured\n\tCID: {ControllerId.ToString()}\n\tServo state: {servoMode[0]}\n\tMotor state: {motorMode[0]}");
                    NotificationMessage("Motor not configured");
                    return false;
                }
                else
                {
                    NotificationMessage("Motor configured successfully");
                    LogMessage($"Motor configured successfully\n\tCID: {ControllerId.ToString()}\n\tServo state: {servoMode[0]}\n\tMotor state: {motorMode[0]}");
                    return true;
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
                return false;
            }
        }

        private string GetScopeName()
        {
            ushort vendorId = 0x0AAD;
            ushort productId = 0x01D6;
            string serial = "107415";//  // "107415"   // Optional; pass null or an empty string if not required

            string visaResource = VisaHelper.FindVisaResource(vendorId, productId, serial);

            if (visaResource != null)
            {
                NotificationMessage("Found VISA resource: " + visaResource);
                //LogMessage("Found VISA resource: " + visaResource);
                return null;
                
                // You can now open the session using your VISA API.
            }
            else
            {
                NotificationMessage("No matching VISA resource found.");
                //LogMessage("No matching VISA resource found.");
                return visaResource;
            }
        }
        //private void ConnectToScope(string ResourceName) // Original working connect To scope function
        //{
        //    //LogMessage("Connecting to scope ...");
        //    NotificationMessage("Connecting to scope ...");
        //    bool scopeConnected = false;
        //    // Separate try-catch for the instrument initialization prevents accessing uninitialized object
        //    try
        //    {
        //        // Initialization:
        //        //GetScopeName();
        //        io = GlobalResourceManager.Open(ResourceName) as IMessageBasedSession;
        //        io.Clear();
        //        // Timeout for VISA Read Operations
        //        Thread.Sleep(250);
        //        io.TimeoutMilliseconds = 10000;
        //    }
        //    catch (NativeVisaException e)
        //    {
        //        ErrorMessage("Unable to connect to scope");
        //        NotificationMessage("Unable to connet to scope");
        //        //LogMessage($"Error initializing the io session:\n{e.Message}");
        //        return;
        //    }

        //    try
        //    {
        //        //LogMessage("Ivi GlobalResourceManager selected the following VISA.NET:");
        //        //LogMessage($"Manufacturer: {io.ResourceManufacturerName}");
        //        //LogMessage($"Implementation Version: {io.ResourceImplementationVersion}");

        //        io.RawIO.Write("*RST;*CLS"); // Reset the instrument, clear the Error queue
        //        io.RawIO.Write("*IDN?");
        //        var idnResponse = io.RawIO.ReadString();

        //        LogMessage($"Scope: {idnResponse} connected");
        //        scopeConnected = true;
        //    }
        //    catch (VisaException e)
        //    {
        //        ErrorMessage("Scope not connected");
        //        //LogMessage($"Error initializing the io session:\n{e.Message}");
        //    }
        //    if (scopeConnected)
        //    {
        //        ConfigureScope();
        //    }
        //}

        private void ConnectToScope(string resourceName) // Joes version with Ethernet fallback
        {
            NotificationMessage("Attempting to connect to scope via USB...");
            bool scopeConnected = false;

            // Try connecting via USB
            try
            {
                io = GlobalResourceManager.Open(resourceName) as IMessageBasedSession;
                io.Clear();
                io.TimeoutMilliseconds = 10000; // Timeout for VISA Read Operations
                Thread.Sleep(250);

                io.RawIO.Write("*RST;*CLS"); // Reset the instrument, clear the Error queue
                io.RawIO.Write("*IDN?");
                var idnResponse = io.RawIO.ReadString();

                LogMessage($"Scope connected successfully via USB. IDN Response: {idnResponse}");
                NotificationMessage($"Scope connected via USB: {idnResponse}");
                scopeConnected = true;
            }
            catch (Exception usbEx)
            {
                ErrorMessage($"USB connection failed: {usbEx.Message}");
                NotificationMessage("USB connection failed. Attempting Ethernet connection...");
            }

            // If USB connection fails, try Ethernet
            if (!scopeConnected)
            {
                string ipAddress = "169.254.182.112"; // Replace with the actual IP address of the scope
                string port = "5025"; // Replace with the actual port (e.g., "INSTR" or "5025")
                string ethernetResource = $"TCPIP::{ipAddress}::{port}";

                try
                {
                    io = GlobalResourceManager.Open(ethernetResource) as IMessageBasedSession;
                    io.Clear();
                    io.TimeoutMilliseconds = 10000; // Timeout for VISA Read Operations
                    Thread.Sleep(250);

                    io.RawIO.Write("*RST;*CLS"); // Reset the instrument, clear the Error queue
                    io.RawIO.Write("*IDN?");
                    var idnResponse = io.RawIO.ReadString();

                    LogMessage($"Scope connected successfully via Ethernet. IDN Response: {idnResponse}");
                    NotificationMessage($"Scope connected via Ethernet: {idnResponse}");
                    scopeConnected = true;
                }
                catch (Exception ethernetEx)
                {
                    ErrorMessage($"Ethernet connection failed: {ethernetEx.Message}");
                    NotificationMessage("Failed to connect to scope via both USB and Ethernet.");
                }
            }

            // If neither connection method works
            if (!scopeConnected)
            {
                NotificationMessage("Unable to connect to scope.");
                ErrorMessage("Scope connection failed via both USB and Ethernet.");
            }
            else
            {
                ConfigureScope();
            }
        }

        private void ConfigureScope()
        {
            //ErrorMessage("Configuring scope ...");

            NotificationMessage("Configuring scope ...");
            try
            {
                SetUserTimeBase();
                io.RawIO.Write("TIM:SCAL?");
                var setTimeBase = io.RawIO.ReadString();

                SetUserVoltsPerDiv();
                io.RawIO.Write("CHAN1:SCAL?");
                var setVoltsPerDiv = io.RawIO.ReadString();

                LogMessage($"Scope configured successfully.\nSettings:  Timebase = {setTimeBase.Trim()}\n\tVoltage range = {setVoltsPerDiv.Trim()} V");
                NotificationMessage("Scope configured successfully");
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
            }
        }

        private void LoadAvailableUSBs()
        {
            NotificationMessage("Loading USBs");
            // Clear any existing items in the ComboBox
            motorUSBComboBox.Items.Clear();
            try
            {
                var connectedUsbController = new StringBuilder(1024);
                var noDevices = GCS2.EnumerateUSB(connectedUsbController, 1024, "");
                motorUSBComboBox.Items.Add(connectedUsbController);
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void motorUSBComboBox_DropDown(object sender, EventArgs e)
        {
            LoadAvailableUSBs();
        }

        private void motorUSBComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectedUsbController = null;
            connectedUsbController = new StringBuilder(motorUSBComboBox.SelectedItem.ToString());
            ConnectToMotor(connectedUsbController);
        }

        private void sendSerialCommand_Click(object sender, EventArgs e)
        {
            if (ControllerId == -1)
            {
                ErrorMessage("No motor connected");
                return;
            }
            try
            {

                //if (double.TryParse(textBox1.Text, out double commandValue))
                //{
                //    LogMessage($"{commandValue} of type {commandValue.GetType().ToString()}");
                //    int v = GCS2.MOV(ControllerId, "1", new double[] { commandValue });
                //    LogMessage($"Motor moved to {commandValue}");
                //}
                //else
                //{
                //    ErrorMessage("Invalid input. Please enter a valid number.");
                //}
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.ToString());
            }
        }

        private void identifyButton_Click(object sender, EventArgs e)
        {
            NotificationMessage("Identifying motor ...");
            IsMotorConnected();
            //int iControllderReady = 0; // Initialize the variable
            //int result = GCS2.IsControllerReady(ControllerId, ref iControllderReady);
            //LogMessage($"IsControllerReady: {result} iControllerReady {iControllderReady}");
            //int[] servoMode = new int[1];

            //LogMessage($"Servo state: {GCS2.qSVO(ControllerId, "1", servoMode)}");
            //LogMessage($"Servo state (servomode): {servoMode[0]}");
            //double[] pos = new double[1];

            //servoMode[0] = 1;
            //LogMessage($"Set servo state to 0: {GCS2.SVO(ControllerId, "1", servoMode)}");
            //GCS2.qPOS(ControllerId, "1", pos);
            //LogMessage($"Position: {pos}");
        }
        private void MoveToAbsoluteTarget(double[] targetPos)
        {
            if (GCS2.MOV(ControllerId, Axis, targetPos) == PI_RESULT_FAILURE)
            {
                throw new GcsCommandError("Unable to move to target position.");
            }
            //if (GCS2.qPOS(ControllerId, Axis, targetPos) == targetPos)
            //{
            //    throw new GcsCommandError("Unable to query position.");
            //}
        }

        private bool IsMotorMoving()
        {
            NotificationMessage("Motor moving ...");
            int[] isMoving = new int[1];
            int result = GCS2.IsMoving(ControllerId, Axis, isMoving);
            if (result == PI_RESULT_FAILURE)
            {
                throw new GcsCommandError("Unable to query movement status.");
            }
            else
            {
                if (isMoving[0] == PI_TRUE)
                {
                    return true;
                }
                else
                {
                    NotificationMessage("Motor moved successfully.");
                    return false;
                }
            }
        }

        private bool IsMotorConnected()
        {
            NotificationMessage("Checking motor connection ...");
            //LogMessage($"Checking motor connection CID {ControlFlerId}");
            int[] referencedMode = new int[1];
            int[] servoMode = new int[1];
            int[] motorMode = new int[1];
            if (ControllerId == -1)
            {
                //ErrorMessage($"Motor not connected, CID =  -1");
                return false;
            }
            if (GCS2.qFRF(ControllerId, "1", referencedMode) == PI_RESULT_FAILURE)
            {
                LogMessage($"Motor not connected: Unreferenced motor");
                NotificationMessage("Motor not connected: Unreferenced motor");
                // throw new GcsCommandError("Unable to query reference status");
            }
            //if (referencedMode[0] == 0)
            //{
            //    ErrorMessage("Motor not connected: Unreferenced motor");
            //}
            if (GCS2.qSVO(ControllerId, "1", servoMode) == PI_RESULT_FAILURE)
            {
                throw new GcsCommandError("Unable to query servo status");
            }
            if (GCS2.qEAX(ControllerId, "1", motorMode) == PI_RESULT_FAILURE)
            {
                throw new GcsCommandError("Unable to query motor status");
            }

            if ((ControllerId == -1) || (servoMode[0] == 0) || (motorMode[0] == 0))
            {
                NotificationMessage("Motor not connected");
                LogMessage($"Motor not connected\n\tCID: {ControllerId.ToString()}\n\tServo state: {servoMode[0]}\n\tMotor state: {motorMode[0]}");
                return false;
            }
            else
            {
                NotificationMessage("Motor connected");
                return true;
            }
        }

        private double GetMotorPosition()
        {
            double[] motorPosition = new double[1];
            if (GCS2.qPOS(ControllerId, "1", motorPosition) == PI_RESULT_FAILURE)
            {
                throw new GcsCommandError("Unable to query motor position");
            }
            return motorPosition[0];
        }

        private string GenerateUniqueFileName(string folderPath, string baseFileName)
        {
            // Add .csv extension if the base filename does not have an extension
            if (string.IsNullOrEmpty(Path.GetExtension(baseFileName)))
            {
                baseFileName += ".csv";
            }

            string fullFilePath = Path.Combine(folderPath, baseFileName);
            string fileExtension = Path.GetExtension(baseFileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
            if (iterateFilenamesCheckBox.Checked == false)
            {
                return fullFilePath;
            }
            int counter = 1;

            while (File.Exists(fullFilePath))
            {
                string newFileName = $"{fileNameWithoutExtension}_{counter}{fileExtension}";
                fullFilePath = Path.Combine(folderPath, newFileName);
                counter++;
            }

            return fullFilePath;
        }

        private void SetFileName()
        {
            string folderPath = saveLocationTextBox.Text;
            string baseFileName = basicScanFileNameTextBox.Text;
            fileName = GenerateUniqueFileName(folderPath, baseFileName);
        }

        private void HeaderFile(string fileName, string header)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(header);
                    writer.Flush();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                ErrorMessage($"Access to the path '{fileName}' is denied.");
            }
            catch (Exception ex)
            {
                ErrorMessage($"An error occurred: {ex.Message}");
            }
        }

        private void AddDataLineToFile(string fileName, params object[] values)
        {
            NotificationMessage("Writing to file ...");
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Convert each value to its string representation and join them with commas
                    string line = string.Join(", ", values.Select(v => v?.ToString() ?? string.Empty));
                    writer.WriteLine(line);
                    writer.Flush();
                }
                NotificationMessage("Data written to file");
            }
            catch
            {
                ErrorMessage("Error writing to file");
            }
        }


        //private void AddDataLineToFile(string fileName, double position, double voltage)
        //{
        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(fileName, true))
        //        {
        //            writer.WriteLine($"{position}, {voltage}");
        //            writer.Flush();
        //        }

        //    }
        //    catch
        //    {
        //        ErrorMessage("Error writing to file");
        //    }
        //}


        //private void RunPythonScript(string scriptPath, string arguments = "")
        //{
        //    try
        //    {
        //        ProcessStartInfo start = new ProcessStartInfo();
        //        start.FileName = "python"; // Ensure 'python' is in your PATH environment variable
        //        start.Arguments = $"{scriptPath} {arguments}";
        //        start.UseShellExecute = false;
        //        start.RedirectStandardOutput = true;
        //        start.RedirectStandardError = true;
        //        start.CreateNoWindow = true;

        //        using (Process process = Process.Start(start))
        //        {
        //            using (StreamReader reader = process.StandardOutput)
        //            {
        //                string result = reader.ReadToEnd();
        //                LogMessage(result);
        //            }
        //            using (StreamReader reader = process.StandardError)
        //            {
        //                string error = reader.ReadToEnd();
        //                if (!string.IsNullOrEmpty(error))
        //                {
        //                    ErrorMessage(error);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage($"Error running Python script: {ex.Message}");
        //    }
        //}
        //using System;
        //using System.Diagnostics;
        //using System.IO;




        public double[] RunPythonScriptAndGetParams(string scriptPath, double[] xData, double[] yData, double[] initialGuess)
        {
            NotificationMessage("Running python script ...");
            // Create an object to hold the data to send to Python.
            var dataObject = new
            {
                x_data = xData,
                y_data = yData,
                initial_guess = initialGuess
            };
            // Serialize the object to JSON.
            string jsonData = JsonConvert.SerializeObject(dataObject);

            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python", // Ensure Python is in your PATH.
                    Arguments = scriptPath, // The Python script file.
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true, // Enable sending input to Python.
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(start))
                {
                    // Write the JSON data to the Python script's standard input.
                    using (StreamWriter writer = process.StandardInput)
                    {
                        writer.Write(jsonData);
                    }
                    // Read the output from Python.
                    string result = process.StandardOutput.ReadToEnd();
                    // Read any error messages.
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrEmpty(error))
                    {
                        // Handle errors appropriately.
                        ErrorMessage("Python error: " + error);
                        NotificationMessage("Python error: " + error);
                        // Optionally, you might choose to return an array of -1's.
                        return new double[] { -1, -1, -1 };
                    }

                    // Parse the result string, which should be comma-separated.
                    double[] fitParams = result
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => double.Parse(s.Trim()))
                        .ToArray();
                    return fitParams;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage("Exception: " + ex.ToString());
                return new double[] { -1, -1, -1 };
            }
        }



        //private void LogMessage(string message)
        //{
        //    // Your logging implementation here (e.g., Console.WriteLine or writing to a log file)
        //    Console.WriteLine(message);
        //}

        //private void ErrorMessage(string message)
        //{
        //    // Your error handling implementation here
        //    Console.Error.WriteLine(message);
        //}
        public double[] GetInitialGuess(double[] xData, double[] yData)
        {
            // Amplitude: maximum of yData
            double A_guess = yData.Max();

            // Mean: x-value corresponding to maximum yData
            int indexMax = Array.IndexOf(yData, A_guess);
            double mu_guess = xData[indexMax];

            // Standard Deviation: approximately one-sixth of the range of xData
            double sigma_guess = (xData.Max() - xData.Min()) / 6.0;

            return new double[] { A_guess, mu_guess, sigma_guess };
        }

        private string PulseDurationFromFWHM_asString(double FWHM)
        {
            double delay = 2 * FWHM * 1e-3 / 3e8 * 1/Math.Sqrt(2);
            Tuple<double, string> delayTuple = GetSIDelayWithUnits(delay);
            double adjustedDelay = delayTuple.Item1;
            string unit = delayTuple.Item2;
            return $"{adjustedDelay.ToString("F2")} {unit}";
        }

        private bool CompleteScan()
        {
            NotificationMessage("Commencing scan ...");
            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 0; }));
            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 1 * 100 / numberOfProgressBarIncrementsInScan; }));
            if (saveScanResultsCheckBox.Checked)
            {
                SetFileName();
                // Include position uncertainty column (discrete scans will write 0 for uncertainty)
                HeaderFile(fileName, "Position[mm], PositionUncertainty[mm], Voltage[V]");
            }
            Series scopeTrace = new Series("ScopeTrace")
            {
                ChartType = SeriesChartType.Point, // Use points instead of a line
                MarkerStyle = MarkerStyle.Cross,   // Set marker style to cross
                MarkerSize = 10                    // Adjust size for visibility
            };
            this.Invoke((MethodInvoker)delegate
            {
                NotificationMessage("Configuring chart ...");
                chart1.Series.Clear(); // Clear the chart at the beginning
                chart1.Series.Add(scopeTrace);
                // Customize chart
                chart1.ChartAreas[0].AxisX.Title = "Motor Position [mm]";
                chart1.ChartAreas[0].AxisY.Title = "Voltage [V]";
                chart1.Legends.Clear(); // Remove the legend

                // Format the X axis labels to 2 decimal places
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
            });

            List<double> voltages = new List<double>(); // Initialize voltages
            List<double> positions = new List<double>(); // Initialize positions
            double motorStepSize = (double)((motorEndPositionNumericUpDown.Value - motorStartPositionNumericUpDown.Value) / noScanPointsNumericUpDown.Value);
            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 2 * 100 / numberOfProgressBarIncrementsInScan; }));
            // Move to scan start position
            NotificationMessage("Moving to start position ...");
            MoveToAbsoluteTarget(new double[] { (double)motorStartPositionNumericUpDown.Value });
            while (IsMotorMoving())
            {
                Thread.Sleep(10);
            }
            if (discreteStepsRadioButton.Checked)
            {

                // Begin taking measurements
                int errorCount = 0;
                for (double i = 0;
                     i <= (int)noScanPointsNumericUpDown.Value;
                     i += 1)
                {
                    double newMotorPosition = (double)motorStartPositionNumericUpDown.Value + i * motorStepSize;
                    MoveToAbsoluteTarget(new double[] { newMotorPosition });
                    while (IsMotorMoving())
                    {
                        Thread.Sleep(10); // Wait for motor to stop
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        NotificationMessage($"Thread sleeping for {(int)timeBetweenStepsNumericUpDown.Value} ms");
                        Thread.Sleep((int)timeBetweenStepsNumericUpDown.Value);
                    });
                    // Take a measurement
                    try
                    {
                        if (IsMotorConnected() && IsScopeConnected())
                        {
                            double[] temp_voltages = new double[(int)(averageOverEachMeasurementNumericUpDown.Value)];
                            for (int j = 0; j < (int)(averageOverEachMeasurementNumericUpDown.Value); j++)
                            {
                                double voltage_j;
                                if (useMaxVoltageInScopeTraceRadioButton.Checked)
                                {
                                    voltage_j = GetMaxVoltageFromScopeTrace();
                                }
                                else
                                {
                                    voltage_j = GetMeanVoltageFromScopeTrace();
                                }
                                temp_voltages[j] = voltage_j;
                            }
                            double voltage = temp_voltages.Average();
                            //double voltage = GetMeanVoltageFromScopeTrace();  // temp_voltages.Average();
                            voltages.Add(voltage); // Add voltage to the list
                            double motorPosition = GetMotorPosition();
                            positions.Add(motorPosition);
                            if ((saveScanResultsCheckBox.Checked) && (File.Exists(fileName)))
                            {
                                // Save with zero uncertainty for discrete mode
                                try
                                {
                                    File.AppendAllText(fileName, $"{motorPosition:F6}, {0.0:F6}, {voltage:F6}\n");
                                }
                                catch (Exception ex)
                                {
                                    LogMessage($"Error saving discrete scan line: {ex.Message}");
                                }
                            }

                            // Add the data point to the chart
                            this.Invoke((MethodInvoker)delegate
                            {
                                scopeTrace.Points.AddXY(motorPosition, voltage);
                                var dp = scopeTrace.Points[scopeTrace.Points.Count - 1];
                                dp.ToolTip = $"±{0.0:F6} mm";
                                // optional: show label if desired:
                                // dp.Label = $"{motorPosition:F3}±{0.000:F3} mm";
                                chart1.Invalidate();
                            });

                            // Allow the UI to update
                            Application.DoEvents();
                            //this.Invoke((MethodInvoker)delegate
                            //{ ... });
                        }
                        else
                        {
                            LogMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
                            NotificationMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
                            try
                            {
                                if (IsMotorConnected() == false)
                                {
                                    ConnectToMotor(connectedUsbController);
                                    if (IsMotorConnected())
                                    {
                                        NotificationMessage("Motor reconnected");
                                    }
                                    else { errorCount++; }
                                }
                                if (IsScopeConnected() == false)
                                {
                                    ConnectToScope(scopeName);
                                    if (IsScopeConnected())
                                    {
                                        NotificationMessage("Scope reconnected");
                                    }
                                }
                                else { errorCount++; }
                                if (errorCount >= 3)
                                {
                                    ErrorMessage("Aborting scan ...");
                                    NotificationMessage("Aborting scan ...");
                                    return false;
                                }
                            }
                            catch
                            {
                                ErrorMessage("Error taking measurement");
                                errorCount++;
                                if (errorCount >= 3)
                                {
                                    ErrorMessage("Aborting scan ...");
                                    NotificationMessage("Aborting scan ...");
                                    return false;
                                }
                                ErrorMessage("Error taking measurement");
                                errorCount++;
                                if (errorCount >= 3)
                                {
                                    ErrorMessage("Aborting scan ...");
                                    NotificationMessage("Aborting scan ...");
                                    return false;
                                }
                            }
                        }

                    }
                    catch
                    {
                        ErrorMessage("Error taking measurement");
                        errorCount++;
                        if (errorCount >= 3)
                        {
                            ErrorMessage("Aborting scan ...");
                            NotificationMessage("Aborting scan ...");
                            return false;
                        }
                    }
                }
            }
            else // Continuous motion ! 
            {
                LogMessage("ATTEMPTING CONTINUOUS SCAN");
                // Ensure no division by zero
                double requestedSpeed = (double)motorSpeedNumericUpDown.Value;
                if (requestedSpeed == 0)
                {
                    ErrorMessage("Motor speed must be non-zero for continuous scan.");
                    return false;
                }

                // determine direction sign so velocity has correct sign for MoveToAbsoluteTarget if necessary
                double startPos = (double)motorStartPositionNumericUpDown.Value;
                double endPos = (double)motorEndPositionNumericUpDown.Value;
                double distance = endPos - startPos;
                if (Math.Abs(distance) < 1e-12)
                {
                    ErrorMessage("Start and end positions are the same for continuous scan.");
                    return false;
                }
                double velocityToSet = Math.Abs(requestedSpeed) * Math.Sign(distance); // signed velocity consistent with direction

                if (!SetMotorVelocity(velocityToSet)) { ErrorMessage("Error setting motor velocity"); return false; }
                LogMessage("Moving to start position ...");
                MoveToAbsoluteTarget(new double[] { startPos });
                while (IsMotorMoving())
                {
                    Thread.Sleep(10);
                }

                // compute time between measurement points based on spatial step size and velocity magnitude
                int nPoints = (int)noScanPointsNumericUpDown.Value;
                if (nPoints <= 0) { ErrorMessage("Number of scan points must be > 0"); return false; }

                // motorStepSize already computed above: total distance / nPoints
                double stepSize = motorStepSize; // mm per sample
                double velocityMagnitude = Math.Abs(velocityToSet); // mm/s
                                                                    // time between samples in milliseconds
                double timeBetweenSamplesMs = (stepSize / velocityMagnitude) * 1000.0;
                if (timeBetweenSamplesMs <= 1.0) timeBetweenSamplesMs = 1.0; // avoid zero or extremely small

                LogMessage($"Continuous scan: stepSize={stepSize:F6} mm, velocity={velocityToSet:F6} mm/s, dt ~ {timeBetweenSamplesMs:F1} ms");

                // Move to end position (continuous motion) and start sampling while motor is moving
                LogMessage("Starting continuous motion to end position ...");
                MoveToAbsoluteTarget(new double[] { endPos });

                Stopwatch sampleStopwatch = new Stopwatch();
                sampleStopwatch.Start();
                int samplesTaken = 0;
                int errorCountCont = 0;

                // We will sample until we've taken nPoints+1 samples (to include both ends) or motor stops
                while ((IsMotorMoving() || samplesTaken == 0))// && samplesTaken <= nPoints)
                {
                    long sampleStartTicks = sampleStopwatch.ElapsedMilliseconds;

                    try
                    {
                        if (IsMotorConnected() && IsScopeConnected())
                        {
                            // record position before measurement
                            double posBefore = GetMotorPosition();

                            // acquire voltage averaging requested number of readings
                            int nAvg = Math.Max(1, (int)averageOverEachMeasurementNumericUpDown.Value);
                            double[] tempVoltages = new double[nAvg];
                            for (int j = 0; j < nAvg; j++)
                            {
                                double vj;
                                if (useMaxVoltageInScopeTraceRadioButton.Checked)
                                {
                                    vj = GetMaxVoltageFromScopeTrace();
                                }
                                else
                                {
                                    vj = GetMeanVoltageFromScopeTrace();
                                }
                                tempVoltages[j] = vj;
                                // a small short delay can be added between rapid scope reads if needed:
                                Thread.Sleep(1);
                            }
                            double voltage = tempVoltages.Average();

                            // record position after measurement
                            double posAfter = GetMotorPosition();

                            // compute mean position and uncertainty
                            double posMean = (posBefore + posAfter) / 2.0;
                            double posUncertainty = Math.Abs(posAfter - posBefore) / 2.0;

                            voltages.Add(voltage);
                            positions.Add(posMean);

                            // Save to file if requested (append with uncertainty)
                            if ((saveScanResultsCheckBox.Checked))
                            {
                                try
                                {
                                    // ensure header exists
                                    File.AppendAllText(fileName, $"{posMean:F6}, {posUncertainty:F6}, {voltage:F6}\n");
                                }
                                catch (Exception ex)
                                {
                                    LogMessage($"Error saving continuous scan line: {ex.Message}");
                                }
                            }

                            // Plot the point and add uncertainty as tooltip/label for visualisation
                            this.Invoke((MethodInvoker)delegate
                            {
                                scopeTrace.Points.AddXY(posMean, voltage);
                                var dp = scopeTrace.Points[scopeTrace.Points.Count - 1];
                                dp.ToolTip = $"±{0.0:F6} mm";
                                // optionally show the label directly:
                                // dp.Label = $"{posMean:F3} ± {posUncertainty:F3} mm";
                                chart1.Invalidate();
                            });

                            samplesTaken++;
                            Application.DoEvents();
                        }
                        else
                        {
                            LogMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
                            NotificationMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
                            try
                            {
                                if (IsMotorConnected() == false)
                                {
                                    ConnectToMotor(connectedUsbController);
                                    if (IsMotorConnected())
                                    {
                                        NotificationMessage("Motor reconnected");
                                    }
                                    else { errorCountCont++; }
                                }
                                if (IsScopeConnected() == false)
                                {
                                    ConnectToScope(scopeName);
                                    if (IsScopeConnected())
                                    {
                                        NotificationMessage("Scope reconnected");
                                    }
                                }
                                else { errorCountCont++; }
                                if (errorCountCont >= 3)
                                {
                                    ErrorMessage("Aborting scan ...");
                                    NotificationMessage("Aborting scan ...");
                                    return false;
                                }
                            }
                            catch
                            {
                                ErrorMessage("Error taking measurement");
                                errorCountCont++;
                                if (errorCountCont >= 3)
                                {
                                    ErrorMessage("Aborting scan ...");
                                    NotificationMessage("Aborting scan ...");
                                    return false;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage($"Error during continuous measurement: {ex.Message}");
                        errorCountCont++;
                        if (errorCountCont >= 3)
                        {
                            ErrorMessage("Aborting scan ...");
                            NotificationMessage("Aborting scan ...");
                            return false;
                        }
                    }

                    // keep a roughly constant sampling interval in time corresponding to spatial step
                    long sampleElapsed = sampleStopwatch.ElapsedMilliseconds - sampleStartTicks;
                    double sleepMs = timeBetweenSamplesMs - sampleElapsed;
                    if (sleepMs > 0)
                    {
                        // use smaller sleeps to keep UI responsive
                        int toSleep = (int)Math.Min(100, Math.Round(sleepMs));
                        Thread.Sleep(toSleep);
                    }

                    // break out if motor stopped and we've collected enough points
                    if (!IsMotorMoving() && samplesTaken > nPoints) break;
                }

                // After loop, ensure motor has stopped or attempt to stop it cleanly
                int motorStopWait = 0;
                while (IsMotorMoving() && motorStopWait < 5000)
                {
                    Thread.Sleep(10);
                    motorStopWait += 10;
                }

                LogMessage($"Continuous scan complete: samplesTaken={samplesTaken}");
            }

            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 4 * 100 / numberOfProgressBarIncrementsInScan; }));
            try
            {
                // Fit Gaussian to the data
                // Get the absolute path of the script (assuming it is in the same directory as the executable)
                if (fitGaussianCheckBox.Checked)
                {
                    string scriptName = "fit_gaussian.py"; // Change this to your actual script name
                    string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scriptName);
                    scriptPath = $"\"{scriptPath}\"";
                    LogMessage(scriptPath);
                    //string scriptPath = @"""C:\Users\jdm24\source\repos\LED Autocorrelator\Scripts\Fits\fit_gaussian.py""";+
                    string arguments = $"{fileName} {fitParametersFilePath}"; // Add any arguments if needed

                    double[] initialGuess = GetInitialGuess(positions.ToArray(), voltages.ToArray());
                    double[] fitParams = RunPythonScriptAndGetParams(scriptPath, positions.ToArray(), voltages.ToArray(), initialGuess);
                    //LogMessage("HERE_5");
                    string arrayAsString = string.Join(", ", fitParams.Select(x => x.ToString("F2")));
                    NotificationMessage($"Fit params {arrayAsString.ToString()}");
                    //RunPythonScript(scriptPath, arguments);
                    //var parameters = FitGaussian(positions.ToArray(), voltages.ToArray());

                    // Read the parameters from the file
                    //LogMessage("JD");
                    //double[] parameters = ReadParametersFromFile(fitParametersFilePath);

                    //// Parameters: A = amplitude, mu = mean, sigma = standard deviation

                    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 7 * 100 / numberOfProgressBarIncrementsInScan; }));
                    if (!(fitParams.SequenceEqual(new double[] { -1, -1, -1 })))
                    {
                        double A = fitParams[0];
                        double mu = fitParams[1];
                        double sigma = fitParams[2];

                        //// Calculate the FWHM
                        double FWHM = 2 * Math.Sqrt(2 * Math.Log(2)) * sigma;
                        if (savePulseDurationCheckBox.Checked && File.Exists(pulseDurationFilename))
                        {
                            //LogMessage("Here2");

                            AddDataLineToFile(pulseDurationFilename, DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss"), 2 * FWHM * 1e-3 / 3e8 * 1 / Math.Sqrt(2));
                        }

                        //LogMessage($"FWHM: {FWHM}");
                        //LogMessage(PulseDurationFromFWHM_asString(FWHM));
                        this.Invoke((MethodInvoker)delegate
                        {
                            pulseDurationLabel.Text = "Pulse duration: " + PulseDurationFromFWHM_asString(FWHM);
                        });

                        // Add the series for the fitted Gaussian curve
                        var fitSeries = new Series { Name = "Fitted Gaussian", ChartType = SeriesChartType.Line };

                        // Set the line color to red
                        fitSeries.BorderColor = System.Drawing.Color.Red;
                        fitSeries.BorderWidth = 2;  // Optionally adjust line width for better visibility

                        for (double x = positions[0]; x <= positions[positions.Count - 1]; x += 0.1)
                        {
                            double y = A * Math.Exp(-Math.Pow(x - mu, 2) / (2 * Math.Pow(sigma, 2)));
                            fitSeries.Points.AddXY(x, y);
                        }
                        // Add the data point to the chart
                        this.Invoke((MethodInvoker)delegate
                        {
                            chart1.Series.Add(fitSeries);
                            Application.DoEvents();
                        });
                        progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 9 * 100 / numberOfProgressBarIncrementsInScan; }));
                    }
                    else
                    {
                        NotificationMessage("Fit not possible");
                    }

                }
                progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 10 * 100 / numberOfProgressBarIncrementsInScan; }));
                Application.DoEvents();
                NotificationMessage("Scan complete");
            }
            catch (Exception e) { ErrorMessage($"{e}"); return false; }
            return true;
            //this.Invoke((MethodInvoker)delegate
            //{
            //    Thread.Sleep((int)timeBetweenStepsNumericUpDown.Value);
            //});
        }
        //private bool CompleteScan()
        //{
        //    NotificationMessage("Commencing scan ...");
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 0; }));
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 1 * 100 / numberOfProgressBarIncrementsInScan; }));
        //    if (saveScanResultsCheckBox.Checked)
        //    {
        //        SetFileName();
        //        HeaderFile(fileName, "Position[mm], Voltage[V]");
        //    }
        //    Series scopeTrace = new Series("ScopeTrace")
        //    {
        //        ChartType = SeriesChartType.Point, // Use points instead of a line
        //        MarkerStyle = MarkerStyle.Cross,   // Set marker style to cross
        //        MarkerSize = 10                    // Adjust size for visibility
        //    };
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        NotificationMessage("Configuring chart ...");
        //        chart1.Series.Clear(); // Clear the chart at the beginning
        //        chart1.Series.Add(scopeTrace);
        //        // Customize chart
        //        chart1.ChartAreas[0].AxisX.Title = "Motor Position [mm]";
        //        chart1.ChartAreas[0].AxisY.Title = "Voltage [V]";
        //        chart1.Legends.Clear(); // Remove the legend

        //        // Format the X axis labels to 2 decimal places
        //        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
        //    });

        //    List<double> voltages = new List<double>(); // Initialize voltages
        //    List<double> positions = new List<double>(); // Initialize positions
        //    double motorStepSize = (double)((motorEndPositionNumericUpDown.Value - motorStartPositionNumericUpDown.Value) / noScanPointsNumericUpDown.Value);
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 2 * 100 / numberOfProgressBarIncrementsInScan; }));
        //    // Move to scan start position
        //    NotificationMessage("Moving to start position ...");
        //    MoveToAbsoluteTarget(new double[] { (double)motorStartPositionNumericUpDown.Value });
        //    while (IsMotorMoving())
        //    {
        //        Thread.Sleep(10);
        //    }
        //    if (discreteStepsRadioButton.Checked)
        //    {

        //        // Begin taking measurements
        //        int errorCount = 0;
        //        for (double i = 0;
        //             i <= (int)noScanPointsNumericUpDown.Value;
        //             i += 1)
        //        {
        //            double newMotorPosition = (double)motorStartPositionNumericUpDown.Value + i * motorStepSize;
        //            MoveToAbsoluteTarget(new double[] { newMotorPosition });
        //            while (IsMotorMoving())
        //            {
        //                Thread.Sleep(10); // Wait for motor to stop
        //            }
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                NotificationMessage($"Thread sleeping for {(int)timeBetweenStepsNumericUpDown.Value} ms");
        //                Thread.Sleep((int)timeBetweenStepsNumericUpDown.Value);
        //            });
        //            // Take a measurement
        //            try
        //            {
        //                if (IsMotorConnected() && IsScopeConnected())
        //                {
        //                    double[] temp_voltages = new double[(int)(averageOverEachMeasurementNumericUpDown.Value)];
        //                    for (int j = 0; j < (int)(averageOverEachMeasurementNumericUpDown.Value); j++)
        //                    {
        //                        double voltage_j;
        //                        if (useMaxVoltageInScopeTraceRadioButton.Checked) 
        //                        {
        //                            voltage_j = GetMaxVoltageFromScopeTrace();
        //                        }
        //                        else
        //                        {
        //                            voltage_j = GetMeanVoltageFromScopeTrace();
        //                        }
        //                        temp_voltages[j] = voltage_j;
        //                    }
        //                    double voltage = temp_voltages.Average();
        //                    //double voltage = GetMeanVoltageFromScopeTrace();  // temp_voltages.Average();
        //                    voltages.Add(voltage); // Add voltage to the list
        //                    double motorPosition = GetMotorPosition();
        //                    positions.Add(motorPosition);
        //                    if ((saveScanResultsCheckBox.Checked) && (File.Exists(fileName)))
        //                    {
        //                        //LogMessage("HERE?");
        //                        NotificationMessage("Printing to file");
        //                        AddDataLineToFile(fileName, motorPosition, voltage);
        //                    }

        //                    // Add the data point to the chart
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        scopeTrace.Points.AddXY(motorPosition, voltage);
        //                        chart1.Invalidate();
        //                    });

        //                    // Allow the UI to update
        //                    Application.DoEvents();
        //                    //this.Invoke((MethodInvoker)delegate
        //                    //{
        //                    //    NotificationMessage($"Thread sleeping for {(int)timeBetweenStepsNumericUpDown.Value} ms");
        //                    //    Thread.Sleep((int)timeBetweenStepsNumericUpDown.Value);
        //                    //});
        //                }
        //                else
        //                {
        //                    LogMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
        //                    NotificationMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
        //                    try
        //                    {
        //                        if (IsMotorConnected() == false)
        //                        {
        //                            ConnectToMotor(connectedUsbController);
        //                            if (IsMotorConnected())
        //                            {
        //                                NotificationMessage("Motor reconnected");
        //                            }
        //                            else { errorCount++; }
        //                        }
        //                        if (IsScopeConnected() == false)
        //                        {
        //                            ConnectToScope(scopeName);
        //                            if (IsScopeConnected())
        //                            {
        //                                NotificationMessage("Scope reconnected");
        //                            }
        //                        }
        //                        else { errorCount++; }
        //                        if (errorCount >= 3)
        //                        {
        //                            ErrorMessage("Aborting scan ...");
        //                            NotificationMessage("Aborting scan ...");
        //                            return false;
        //                        }
        //                    }
        //                    catch
        //                    {
        //                        ErrorMessage("Error taking measurement");
        //                        errorCount++;
        //                        if (errorCount >= 3)
        //                        {
        //                            ErrorMessage("Aborting scan ...");
        //                            NotificationMessage("Aborting scan ...");
        //                            return false;
        //                        }
        //                        ErrorMessage("Error taking measurement");
        //                        errorCount++;
        //                        if (errorCount >= 3)
        //                        {
        //                            ErrorMessage("Aborting scan ...");
        //                            NotificationMessage("Aborting scan ...");
        //                            return false;
        //                        }
        //                    }
        //                }

        //            }
        //            catch
        //            {
        //                ErrorMessage("Error taking measurement");
        //                errorCount++;
        //                if (errorCount >= 3)
        //                {
        //                    ErrorMessage("Aborting scan ...");
        //                    NotificationMessage("Aborting scan ...");
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    else // Continuous motion ! 
        //    {
        //        LogMessage("ATTEMPTING CONTINUTOUD");
        //        if (!SetMotorVelocity((double)motorSpeedNumericUpDown.Value)) { ErrorMessage("Error setting motor velocity"); return false; }
        //        LogMessage("Moving to start position ...");
        //        MoveToAbsoluteTarget(new double[] { (double)motorStartPositionNumericUpDown.Value });
        //        while (IsMotorMoving())
        //        {
        //            Thread.Sleep(10);
        //        }
        //        // Now move to end position and collect data


        //        LogMessage("Continuous motion has not been coded yet !");
        //        // Set velocity
        //        // calculate time between measurements
        //        // Get measurements at interval and wait between measurements
        //    }
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 4 * 100 / numberOfProgressBarIncrementsInScan; }));
        //    try
        //    {
        //        // Fit Gaussian to the data
        //        // Get the absolute path of the script (assuming it is in the same directory as the executable)
        //        if (fitGaussianCheckBox.Checked)
        //        {
        //            string scriptName = "fit_gaussian.py"; // Change this to your actual script name
        //            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scriptName);
        //            scriptPath = $"\"{scriptPath}\"";
        //            LogMessage(scriptPath);
        //            //string scriptPath = @"""C:\Users\jdm24\source\repos\LED Autocorrelator\Scripts\Fits\fit_gaussian.py""";+
        //            string arguments = $"{fileName} {fitParametersFilePath}"; // Add any arguments if needed

        //            double[] initialGuess = GetInitialGuess(positions.ToArray(), voltages.ToArray());
        //            double[] fitParams = RunPythonScriptAndGetParams(scriptPath, positions.ToArray(), voltages.ToArray(), initialGuess);
        //            //LogMessage("HERE_5");
        //            string arrayAsString = string.Join(", ", fitParams.Select(x => x.ToString("F2")));
        //            NotificationMessage($"Fit params {arrayAsString.ToString()}");
        //            //RunPythonScript(scriptPath, arguments);
        //            //var parameters = FitGaussian(positions.ToArray(), voltages.ToArray());

        //            // Read the parameters from the file
        //            //LogMessage("JD");
        //            //double[] parameters = ReadParametersFromFile(fitParametersFilePath);

        //            //// Parameters: A = amplitude, mu = mean, sigma = standard deviation

        //            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 7 * 100 / numberOfProgressBarIncrementsInScan; }));
        //            if (!(fitParams.SequenceEqual(new double[] { -1, -1, -1 })))
        //            {
        //                double A = fitParams[0];
        //                double mu = fitParams[1];
        //                double sigma = fitParams[2];

        //                //// Calculate the FWHM
        //                double FWHM = 2 * Math.Sqrt(2 * Math.Log(2)) * sigma;
        //                if (savePulseDurationCheckBox.Checked && File.Exists(pulseDurationFilename))
        //                {
        //                    //LogMessage("Here2");

        //                    AddDataLineToFile(pulseDurationFilename, DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss"), 2 * FWHM * 1e-3 / 3e8 * 1 / Math.Sqrt(2));
        //                }

        //                //LogMessage($"FWHM: {FWHM}");
        //                //LogMessage(PulseDurationFromFWHM_asString(FWHM));
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    pulseDurationLabel.Text = "Pulse duration: " + PulseDurationFromFWHM_asString(FWHM);
        //                });

        //                // Add the series for the fitted Gaussian curve
        //                var fitSeries = new Series { Name = "Fitted Gaussian", ChartType = SeriesChartType.Line };

        //                // Set the line color to red
        //                fitSeries.BorderColor = System.Drawing.Color.Red;
        //                fitSeries.BorderWidth = 2;  // Optionally adjust line width for better visibility

        //                for (double x = positions[0]; x <= positions[positions.Count - 1]; x += 0.1)
        //                {
        //                    double y = A * Math.Exp(-Math.Pow(x - mu, 2) / (2 * Math.Pow(sigma, 2)));
        //                    fitSeries.Points.AddXY(x, y);
        //                }
        //                // Add the data point to the chart
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    chart1.Series.Add(fitSeries);
        //                    Application.DoEvents();
        //                });
        //                progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 9 * 100 / numberOfProgressBarIncrementsInScan; }));
        //            }
        //            else
        //            {
        //                NotificationMessage("Fit not possible");
        //            }

        //        }
        //        progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 10 * 100 / numberOfProgressBarIncrementsInScan; }));
        //        Application.DoEvents();
        //        NotificationMessage("Scan complete");
        //    }
        //    catch (Exception e) { ErrorMessage($"{e}"); return false; }
        //    return true;
        //    //this.Invoke((MethodInvoker)delegate
        //    //{
        //    //    Thread.Sleep((int)timeBetweenStepsNumericUpDown.Value);
        //    //});
        //}

        //private bool CompleteScan()
        //{
        //    NotificationMessage("Commencing scan ...");
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 0; }));
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 1 * 100 / numberOfProgressBarIncrementsInScan; }));

        //    if (saveScanResultsCheckBox.Checked)
        //    {
        //        SetFileName();
        //        HeaderFile(fileName, "Position[mm], Voltage[V]");
        //    }

        //    Series scopeTrace = new Series("ScopeTrace")
        //    {
        //        ChartType = SeriesChartType.Point, // Use points instead of a line
        //        MarkerStyle = MarkerStyle.Cross,   // Set marker style to cross
        //        MarkerSize = 10                    // Adjust size for visibility
        //    };
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        NotificationMessage("Configuring chart ...");
        //        chart1.Series.Clear(); // Clear the chart at the beginning
        //        chart1.Series.Add(scopeTrace);
        //        // Customize chart
        //        chart1.ChartAreas[0].AxisX.Title = "Motor Position [mm]";
        //        chart1.ChartAreas[0].AxisY.Title = "Voltage [V]";
        //        chart1.Legends.Clear(); // Remove the legend

        //        // Format the X axis labels to 2 decimal places
        //        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
        //    });

        //    List<double> voltages = new List<double>(); // Initialize voltages
        //    List<double> positions = new List<double>(); // Initialize positions
        //    double motorStepSize = (double)((motorEndPositionNumericUpDown.Value - motorStartPositionNumericUpDown.Value) / noScanPointsNumericUpDown.Value);
        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 2 * 100 / numberOfProgressBarIncrementsInScan; }));

        //    // Move to scan start position
        //    NotificationMessage("Moving to start position ...");
        //    if (!SetMotorVelocity((double)25.0)) { ErrorMessage("Error setting motor velocity"); return false; }
        //    MoveToAbsoluteTarget(new double[] { (double)motorStartPositionNumericUpDown.Value });
        //    while (IsMotorMoving())
        //    {
        //        Thread.Sleep(10);
        //    }

        //    if (continuousStepsRadioButton.Checked)
        //    {
        //        if (!SetMotorVelocity((double)motorSpeedNumericUpDown.Value)) { ErrorMessage("Error setting motor velocity"); return false; }

        //        // Calculate the sampling interval
        //        double scanRange = (double)(motorEndPositionNumericUpDown.Value - motorStartPositionNumericUpDown.Value);
        //        double motorVelocity = (double)motorSpeedNumericUpDown.Value;
        //        double samplingInterval = scanRange / (motorVelocity * (double)noScanPointsNumericUpDown.Value);

        //        // Start the motor moving to the end position
        //        MoveToAbsoluteTarget(new double[] { (double)motorEndPositionNumericUpDown.Value });

        //        // Create a timer to handle sampling
        //        System.Timers.Timer samplingTimer = new System.Timers.Timer(samplingInterval * 1000); // Convert to milliseconds
        //        samplingTimer.Elapsed += (sender, e) =>
        //        {
        //            // Take a measurement
        //            try
        //            {
        //                if (IsMotorConnected() && IsScopeConnected())
        //                {
        //                    double voltage = GetMaxVoltageFromScopeTrace();
        //                    voltages.Add(voltage); // Add voltage to the list
        //                    double motorPosition = GetMotorPosition();
        //                    positions.Add(motorPosition);
        //                    if ((saveScanResultsCheckBox.Checked) && (File.Exists(fileName)))
        //                    {
        //                        NotificationMessage("Printing to file");
        //                        AddDataLineToFile(fileName, motorPosition, voltage);
        //                    }

        //                    // Add the data point to the chart
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        scopeTrace.Points.AddXY(motorPosition, voltage);
        //                        chart1.Invalidate();
        //                    });

        //                    // Allow the UI to update
        //                    this.Invoke((MethodInvoker)delegate
        //                    {
        //                        Application.DoEvents();
        //                    });
        //                    Application.DoEvents();
        //                }
        //                else
        //                {
        //                    LogMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
        //                    NotificationMessage("Scope or motor not connecting: Skipping measurement & attempting to reconnect");
        //                }
        //            }
        //            catch
        //            {
        //                ErrorMessage("Error taking measurement");
        //            }
        //        };

        //        // Start the timer
        //        samplingTimer.Start();

        //        // Wait for the motor to reach the end position
        //        while (IsMotorMoving())
        //        {
        //            Thread.Sleep(10);
        //        }

        //        // Stop the timer
        //        samplingTimer.Stop();
        //    }
        //    else
        //    {
        //        // Handle discrete steps as before
        //    }

        //    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 4 * 100 / numberOfProgressBarIncrementsInScan; }));
        //    try
        //    {
        //        // Fit Gaussian to the data
        //        if (fitGaussianCheckBox.Checked)
        //        {
        //            string scriptName = "fit_gaussian.py"; // Change this to your actual script name
        //            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, scriptName);
        //            scriptPath = $"\"{scriptPath}\"";
        //            LogMessage(scriptPath);

        //            double[] initialGuess = GetInitialGuess(positions.ToArray(), voltages.ToArray());
        //            double[] fitParams = RunPythonScriptAndGetParams(scriptPath, positions.ToArray(), voltages.ToArray(), initialGuess);
        //            string arrayAsString = string.Join(", ", fitParams.Select(x => x.ToString("F2")));
        //            NotificationMessage($"Fit params {arrayAsString.ToString()}");

        //            progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 7 * 100 / numberOfProgressBarIncrementsInScan; }));
        //            if (!(fitParams.SequenceEqual(new double[] { -1, -1, -1 })))
        //            {
        //                double A = fitParams[0];
        //                double mu = fitParams[1];
        //                double sigma = fitParams[2];

        //                double FWHM = 2 * Math.Sqrt(2 * Math.Log(2)) * sigma;
        //                if (savePulseDurationCheckBox.Checked && File.Exists(pulseDurationFilename))
        //                {
        //                    AddDataLineToFile(pulseDurationFilename, DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss"), 2 * FWHM * 1e-3 / 3e8 * 1 / Math.Sqrt(2));
        //                }

        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    pulseDurationLabel.Text = "Pulse duration: " + PulseDurationFromFWHM_asString(FWHM);
        //                });

        //                var fitSeries = new Series { Name = "Fitted Gaussian", ChartType = SeriesChartType.Line };
        //                fitSeries.BorderColor = System.Drawing.Color.Red;
        //                fitSeries.BorderWidth = 2;

        //                for (double x = positions[0]; x <= positions[positions.Count - 1]; x += 0.1)
        //                {
        //                    double y = A * Math.Exp(-Math.Pow(x - mu, 2) / (2 * Math.Pow(sigma, 2)));
        //                    fitSeries.Points.AddXY(x, y);
        //                }

        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    chart1.Series.Add(fitSeries);
        //                    Application.DoEvents();
        //                });
        //                progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 9 * 100 / numberOfProgressBarIncrementsInScan; }));
        //            }
        //            else
        //            {
        //                NotificationMessage("Fit not possible");
        //            }
        //        }
        //        progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 10 * 100 / numberOfProgressBarIncrementsInScan; }));
        //        Application.DoEvents();
        //        NotificationMessage("Scan complete");
        //    }
        //    catch (Exception e) { ErrorMessage($"{e}"); return false; }
        //    return true;
        //}


        private bool SetMotorVelocity(double velocity)
        {
            try
            { 
            //{
                //double velocity = (double)motorSpeedNumericUpDown.Value;
                if (GCS2.VEL(ControllerId, Axis, new double[] { velocity }) == PI_RESULT_FAILURE)
                {
                    NotificationMessage("Unable to set motor velocity");
                    //throw new GcsCommandError("Unable to set motor velocity");
                    return false;
                }
                // Check motor velocity
                LogMessage("Motor velocity set?");
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage(e.ToString());
                return false;
            }
        }

        private double[] ReadParametersFromFile(string filePath)
        {
            //LogMessage($"{lines}");
            try
            {
                LogMessage($"Reading parameters from file: {filePath}");

                // Ensure the file path is correctly formatted
                filePath = filePath.Trim('"');

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file {filePath} does not exist.");
                }
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);
                LogMessage($"File content: {string.Join(Environment.NewLine, lines)}");

                if (lines.Length < 2)
                {
                    throw new Exception("The file does not contain enough lines.");
                }

                string[] values = lines[1].Split(',');
                if (values.Length < 3)
                {
                    throw new Exception("The second line does not contain enough values.");
                }

                double A = double.Parse(values[0]);
                double mu = double.Parse(values[1]);
                double sigma = double.Parse(values[2]);

                return new double[] { A, mu, sigma };
            }
            catch (Exception ex)
            {
                ErrorMessage($"Error reading parameters from file: {ex.Message}");
                return new double[] { 0, 0, 0 }; // Return default values in case of error
            }
        }


        //private double[] FitGaussian(double[] x_data, double[] y_data)
        //{
        //    // Initial guess for A, mu, sigma
        //    double A_initial = 1.0;
        //    double mu_initial = 0.0;
        //    double sigma_initial = 1.0;

        //    // Function returning residuals instead of summed error
        //    Func<Vector<double>, Vector<double>> residualsFunction = p =>
        //    {
        //        double A = p[0];
        //        double mu = p[1];
        //        double sigma = p[2];

        //        var residuals = Vector<double>.Build.Dense(x_data.Length);
        //        for (int i = 0; i < x_data.Length; i++)
        //        {
        //            double y_model = A * Math.Exp(-Math.Pow(x_data[i] - mu, 2) / (2 * Math.Pow(sigma, 2)));
        //            residuals[i] = y_data[i] - y_model;  // Residuals (difference between observed and model values)
        //        }
        //        return residuals;
        //    };

        //    // Replace the line causing the error with the following:
        //    var objective = ObjectiveFunction.NonlinearModel(residualsFunction);

        //    // Initial parameters
        //    var initialParameters = Vector<double>.Build.DenseOfArray(new double[] { A_initial, mu_initial, sigma_initial });

        //    // Use Levenberg-Marquardt optimization
        //    var optimizer = new LevenbergMarquardtMinimizer();
        //    var result = optimizer.FindMinimum(objective, initialParameters);

        //    // Return the optimized parameters
        //    return result.MinimizingPoint.ToArray();
        //}




        private void BasicScanThreadTask()
        {
            while (isRunningContinuously)
            {
                bool exitStatus = CompleteScan();
                if (exitStatus == false)
                {
                    progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 0; }));
                    break;
                }
            }
            this.Invoke((MethodInvoker)delegate
            {
                startBasicScanButton.Enabled = true;
                startBasicScanButton.Visible = true;
                stopBasicScanButton.Visible = false;
                stopBasicScanButton.Enabled = false;
            });
        }

        private void startBasicScanButton_Click(object sender, EventArgs e)
        {

            if (!IsMotorConnected())
            {
                ErrorMessage("No motor connected/configured!");
                return;
            }
            if (savePulseDurationCheckBox.Checked && pulseDurationsFileNameTextBox.Text != "")
            {
                pulseDurationFilename = pulseDurationsFileNameTextBox.Text.Trim();
                pulseDurationFilename = Path.ChangeExtension(pulseDurationFilename, ".csv");
                pulseDurationFilename = Path.Combine(saveLocationTextBox.Text, pulseDurationFilename);
                if (!File.Exists(pulseDurationFilename))
                {
                    HeaderFile(pulseDurationFilename, "Time_stamp, pulse duration[s]");
                }
            }
            else if (savePulseDurationCheckBox.Checked && pulseDurationsFileNameTextBox.Text == "")
            {
                LogMessage("Enter a filename to save pulse durations!");
            }
            if (runContinuouslyCheckBox.Checked)
            {
                isRunningContinuously = true;
                startBasicScanButton.Enabled = false;
                startBasicScanButton.Visible = false;
                stopBasicScanButton.Visible = true;
                stopBasicScanButton.Enabled = true;
                _basicScanThread = new Thread(() => BasicScanThreadTask())
                {
                    Name = "BasicScanThread"
                };
                _basicScanThread.Start();
            }
            else
            {
                for (int i = 0; i <= numberOfRunsNumericUpDown.Value - 1; i++)
                {
                    bool exitStatus = CompleteScan();
                    if (exitStatus == false)
                    {
                        progressBar1.BeginInvoke((Action)(() => { progressBar1.Value = 0; }));
                        break;
                    }
                }
                //if (saveScanResultsCheckBox.Checked)
                //{
                //    SaveFileDialog saveFileDialog = new SaveFileDialog();
                //    saveFileDialog.Title = "Save Scan Data";
                //    saveFileDialog.DefaultExt = ".txt";
                //    saveFileDialog.AddExtension = true;
                //    DialogResult result = saveFileDialog.ShowDialog();

                //    if (result == DialogResult.OK)
                //    {
                //        fileName = saveFileDialog.FileName;
                //        LogMessage(fileName);
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}

                //chart1.Series.Clear(); // Clear the chart at the beginning
                //Series scopeTrace = new Series("ScopeTrace")
                //{
                //    ChartType = SeriesChartType.Point, // Use points instead of a line
                //    MarkerStyle = MarkerStyle.Cross,   // Set marker style to cross
                //    MarkerSize = 10                    // Adjust size for visibility
                //};
                //chart1.Series.Add(scopeTrace);


                //       // Add the data point to the chart
                //scopeTrace.Points.AddXY(i, maxVoltage);


                //    // Refresh the chart to update the UI
                //chart1.Invalidate();

            }
        }

        private void saveScanResultsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Check to see if there is a valid filename
            if (saveScanResultsCheckBox.Checked)
            {
                folderSaveDialogButton.Enabled = true;
                basicScanFileNameTextBox.Enabled = true;
                iterateFilenamesCheckBox.Enabled = true;
            }
            else
            {
                folderSaveDialogButton.Enabled = false;
                basicScanFileNameTextBox.Enabled = false;
                iterateFilenamesCheckBox.Enabled = false;
            }

        }

        private double GetMeanVoltageFromScopeTrace()
        {
            //io.RawIO.Write("TIM:SCAL 1E-7");

            io.RawIO.Write("CHAN:DATA?");
            if (useTimebaseInVoltageAcquisitionCheckBox.Checked)
            {
                io.RawIO.Write("TIM:SCAL?");
                var setTimeBase = io.RawIO.ReadString().Trim();
                int sleepTime = (int)(12.0 * float.Parse(setTimeBase));
                NotificationMessage($"Using timebase for voltage acquisition: {sleepTime}");
                Thread.Sleep(sleepTime);
            }
            else
            {
                Thread.Sleep(50);
            }
            var idnResponse = io.RawIO.ReadString();
            //LogMessage(idnResponse.ToString());
            List<double> dataPoints = new List<double>(); // Initialize dataPoints
            try
            {
                string[] values = idnResponse.Split(','); // Split by commas
                dataPoints = values
                    .Select(v => double.TryParse(v, out double result) ? result : double.NaN) // Convert to double safely
                    .Where(v => !double.IsNaN(v)) // Remove invalid entries
                    .ToList();
                //LogMessage(dataPoints.ToString());
                double meanVoltage = dataPoints.Mean(); // Return the maximum value from the data points
                //LogMessage("Max voltage: " + maxVoltage.ToString());
                return meanVoltage;

            }
            catch
            {
                ErrorMessage("Not converting comma separated string");
                return double.NaN; // Return a default value in case of error
            }
            return dataPoints.Max(); // Return the maximum value from the data points
        }

        private double GetMaxVoltageFromScopeTrace()
        {
            //io.RawIO.Write("TIM:SCAL 1E-7");
            io.RawIO.Write("CHAN:DATA?");
            Thread.Sleep(50);
            var idnResponse = io.RawIO.ReadString();
            //LogMessage(idnResponse.ToString());
            List<double> dataPoints = new List<double>(); // Initialize dataPoints
            try
            {
                string[] values = idnResponse.Split(','); // Split by commas
                dataPoints = values
                    .Select(v => double.TryParse(v, out double result) ? result : double.NaN) // Convert to double safely
                    .Where(v => !double.IsNaN(v)) // Remove invalid entries
                    .ToList();
                //LogMessage(dataPoints.ToString());
                double maxVoltage = dataPoints.Max(); // Return the maximum value from the data points
                //LogMessage("Max voltage: " + maxVoltage.ToString());
                return maxVoltage;

            }
            catch
            {
                ErrorMessage("Not converting comma separated string");
                return double.NaN; // Return a default value in case of error
            }
            return dataPoints.Max(); // Return the maximum value from the data points
        }

        private void plotTraceButton_Click(object sender, EventArgs e)
        {
            io.RawIO.Write("TIM:SCAL 1E-7");
            io.RawIO.Write("CHAN:DATA?");
            var idnResponse = io.RawIO.ReadString();
            LogMessage(idnResponse.ToString());
            List<double> dataPoints = new List<double>(); // Initialize dataPoints
            try
            {
                string[] values = idnResponse.Split(','); // Split by commas
                dataPoints = values
                    .Select(v => double.TryParse(v, out double result) ? result : double.NaN) // Convert to double safely
                    .Where(v => !double.IsNaN(v)) // Remove invalid entries
                    .ToList();
                LogMessage(dataPoints.ToString());
            }
            catch
            {
                ErrorMessage("Not converting comma separated string");
                return;
            }
            try
            {
                chart1.Series.Clear();
                Series scopeTrace = new Series("ScopeTrace")
                {
                    ChartType = SeriesChartType.Line
                };
                chart1.Series.Add(scopeTrace);

                // Add parsed data to chart
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    scopeTrace.Points.AddXY(i, dataPoints[i]); // X = index, Y = data value
                }
            }
            catch { ErrorMessage("Unable to plot trace"); }
        }

        private Tuple<double, string> GetSIDelayWithUnits(double delay)
        {
            string unit;
            double adjustedDelay;
            if (delay >= 1e-6) // Microseconds
            {
                adjustedDelay = delay * 1e6;
                unit = "us";
            }
            else if (delay >= 1e-9) // Nanoseconds
            {
                adjustedDelay = delay * 1e9;
                unit = "ns";
            }
            else if (delay >= 1e-12) // Picoseconds
            {
                adjustedDelay = delay * 1e12;
                unit = "ps";
            }
            else if (delay >= 1e-15)// Femtoseconds
            {
                adjustedDelay = delay * 1e15;
                unit = "fs";
            }
            else // Attoseconds
            {
                adjustedDelay = delay * 1e18;
                unit = "as";
            }
            return Tuple.Create(adjustedDelay, unit);
        }

        private void UpdateTimeStepLabel()
        {
            double timeStep = (((double)motorEndPositionNumericUpDown.Value - (double)motorStartPositionNumericUpDown.Value) / (double)noScanPointsNumericUpDown.Value) * 2e-3 / 3e8; // Convert to seconds
            Tuple<double, string> timeStepTuple = GetSIDelayWithUnits(timeStep);
            double adjustedTimeStep = timeStepTuple.Item1;
            string unit = timeStepTuple.Item2;
            adjustedTimeStep = Math.Round(adjustedTimeStep, 2);
            timeStepLabel.Text = $"Time step: {adjustedTimeStep:F3} {unit}";
        }
        private void UpdateDelayLabel()
        {
            double delay = 2.0 * 1e-3 * (double)((motorEndPositionNumericUpDown.Value - motorStartPositionNumericUpDown.Value)) / (double)(3e8);


            // Round to 2 decimal places
            Tuple<double, string> delayTuple = GetSIDelayWithUnits(delay);
            double adjustedDelay = delayTuple.Item1;
            string unit = delayTuple.Item2;
            adjustedDelay = Math.Round(adjustedDelay, 2);
            delayLabel.Text = $"Delay: {adjustedDelay:F3} {unit}";
        }

        private void motorStartPositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateDelayLabel();
            UpdateTimeStepLabel();
            Settings.Default.motorStartPosition = (decimal)motorStartPositionNumericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void motorEndPositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateDelayLabel();
            UpdateTimeStepLabel();
            Settings.Default.motorEndPosition = (decimal)motorEndPositionNumericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void disableMotorButton_Click(object sender, EventArgs e)
        {
            DisableMotor();
        }
        private void DisableMotor()
        {
            if (IsMotorConnected())
            {
                ConfigureMotor(0);
            }
        }

        private void enableMotorButton_Click(object sender, EventArgs e)
        {
            if (!IsMotorConnected())
            {
                ConfigureMotor(1);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (runContinuouslyCheckBox.Checked)
            {
                numberOfRunsNumericUpDown.Enabled = false;
            }
            else
            {
                numberOfRunsNumericUpDown.Enabled = true;
                isRunningContinuously = false;
            }
        }

        private void stopBasicScanButton_Click(object sender, EventArgs e)
        {
            isRunningContinuously = false;
            startBasicScanButton.Enabled = true;
            startBasicScanButton.Visible = true;
            stopBasicScanButton.Visible = false;
            stopBasicScanButton.Enabled = false;
        }

        private void folderSaveDialogButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select the folder to save scan results";
                folderBrowserDialog.ShowNewFolderButton = true;

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    saveLocationTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private bool IsScopeConnected()
        {
            try
            {
                //Console.WriteLine(io);
                if (io == null)
                {
                    NotificationMessage("Scope object null: Connection never established");
                    return false;
                }
                io.RawIO.Write("*IDN?");
                var idnResponse = io.RawIO.ReadString();
                NotificationMessage($"Scope connected: {idnResponse.ToString()}");
                return true;
            }
            catch (Ivi.Visa.NativeVisaException ex)
            {
                NotificationMessage($"Scope not connected: Ivi.Visa.NativeVisaException");
                return false;
            }
            catch (Exception ex2)
            {
                NotificationMessage($"{ex2.ToString()}");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                if (IsScopeConnected())
                {
                    var voltage = GetMaxVoltageFromScopeTrace();

                    LogMessage("Max voltage = " + voltage.ToString());
                    voltage = GetMeanVoltageFromScopeTrace();
                    LogMessage("Mean voltage = " + voltage.ToString());
                }
            }
            LogMessage("Done");
            //for (int i = 0; i < 1000; i++)
            //{
            //    if ()
            //}
            //string scriptPath = @"""C:\Users\jdm24\source\repos\LED Autocorrelator\Scripts\Fits\fit_gaussian.py""";
            //string arguments = $"{fileName} {fitParametersFilePath}"; // Add any arguments if needed
            //double[] initialGuess = GetInitialGuess(positions.ToArray(), voltages.ToArray());
            //RunPythonScriptAndGetParams(scriptPath, positions.ToArray(), voltages.ToArray(), initialGuess);
        }

        private void savePulseDurationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (savePulseDurationCheckBox.Checked)
            {
                pulseDurationsFileNameTextBox.Enabled = true;
                fitGaussianCheckBox.Checked = true;
            }
            else
            {
                pulseDurationsFileNameTextBox.Enabled = false;
            }
        }

        private void motorSpeedNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void connectToScopeButton_Click(object sender, EventArgs e)
        {
            try
            {
                GetScopeName();
                ConnectToScope(scopeName);
            }
            catch (Exception ex)
            {
                ErrorMessage("Unable to connect to scope.");
                NotificationMessage("Unable to connect to scope.");
            }
            //ConfigureScope();
        }

        private void scopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            scopeName = scopeComboBox.SelectedItem.ToString();
            ConnectToScope(scopeName);
        }

        private void manuaMoveMotorButton_Click(object sender, EventArgs e)
        {
            if (!IsMotorConnected()) { ErrorMessage("Motor not connected ..."); return;  }
            try
            {
                if (!SetMotorVelocity(defaultMotorSpeed)) { ErrorMessage("Error setting motor velocity"); }
                MoveToAbsoluteTarget(new double[] { (double)moveMotorNumericUpDown.Value });
                
                while (IsMotorMoving()) { NotificationMessage($"Motor moving @ {GetMotorPosition().ToString()} mm"); Thread.Sleep(10);  }
                NotificationMessage($"Motor moved to {GetMotorPosition().ToString()}");
            } catch { ErrorMessage("Unable to move motor"); }
        }

        private void discreteStepsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (discreteStepsRadioButton.Checked) 
            { 
                continuousStepsRadioButton.Checked = false;
                motorSpeedNumericUpDown.Enabled = false;
                timeBetweenStepsNumericUpDown.Enabled = true;                
            }
            else 
            { 
                continuousStepsRadioButton.Checked = true;
                motorSpeedNumericUpDown.Enabled = true;
                timeBetweenStepsNumericUpDown.Enabled = false;
            }
        }

        private void continuousStepsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (continuousStepsRadioButton.Checked)
            {
                discreteStepsRadioButton.Checked = false;
                motorSpeedNumericUpDown.Enabled = true;
                timeBetweenStepsNumericUpDown.Enabled = false;
            }
            else
            {
                discreteStepsRadioButton.Checked = true;
                motorSpeedNumericUpDown.Enabled = false;
                timeBetweenStepsNumericUpDown.Enabled = true;
            }
        }

        private void SetUserTimeBase()
        {
            if (IsScopeConnected())
            {
                try
                {
                    io.RawIO.Write("TIM:SCAL " + setTimeBaseNumericUpDown.Value.ToString("G"));
                    io.RawIO.Write("TIM:SCAL?");
                    var setTimeBase = io.RawIO.ReadString().Trim();
                    if (double.TryParse(setTimeBase, out double receivedValue) &&
    double.TryParse(setTimeBaseNumericUpDown.Value.ToString(), out double sentValue))
                    {
                        NotificationMessage($"Timebase set: {setTimeBase.Trim()} s");
                    }
                    else
                    {
                        ErrorMessage("Not set timebase");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                }

            }
        }
        private void setTimeBaseNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SetUserTimeBase();
            Settings.Default.timeBase = (double)setTimeBaseNumericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void SetUserVoltsPerDiv()
        {
            if (IsScopeConnected())
            {
                try
                {
                    io.RawIO.Write("CHAN1:SCAL " + voltsPerDivNumericUpDown.Value.ToString());
                    io.RawIO.Write("CHAN1:SCAL?");
                    var setVoltsPerDiv = io.RawIO.ReadString();
                    if (double.TryParse(setVoltsPerDiv, out double receivedValue) &&
    double.TryParse(voltsPerDivNumericUpDown.Value.ToString(), out double sentValue))
                    {
                        NotificationMessage($"Volts/div set: {setVoltsPerDiv.Trim()} V");
                    }
                    else
                    {
                        ErrorMessage("Not set volts/div");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                }

            }
        }

        private void voltsPerDivNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SetUserVoltsPerDiv();
            Settings.Default.voltsPerDivisionChannel1 = (double)voltsPerDivNumericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void voltageOffsetNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (IsScopeConnected())
            {
                try
                {
                    io.RawIO.Write("CHAN1:OFFS " + voltageOffsetNumericUpDown.Value.ToString());
                    io.RawIO.Write("CHAN1:OFFS?");
                    var setVoltageOffset = io.RawIO.ReadString();
                    if (double.TryParse(setVoltageOffset, out double receivedValue) &&
    double.TryParse(voltageOffsetNumericUpDown.Value.ToString(), out double sentValue))
                    {
                        NotificationMessage($"Voltage offset: {setVoltageOffset.Trim()} V");
                    }
                    else
                    {
                        ErrorMessage("Not set voltage offset");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                }

            }
        }

        private void trigLevelNumericDropDown_ValueChanged(object sender, EventArgs e)
        {
            if (IsScopeConnected())
            {
                try
                {
                    io.RawIO.Write("TRIGger:A:LEVel1 " + trigLevelNumericDropDown.Value.ToString());
                    io.RawIO.Write("TRIGger:A:LEVel1?");
                    var trigLevel = io.RawIO.ReadString();
                    if (double.TryParse(trigLevel, out double receivedValue) &&
    double.TryParse(trigLevelNumericDropDown.Value.ToString(), out double sentValue))
                    {
                        NotificationMessage($"Trigger level set: {trigLevel.Trim()} V");
                    }
                    else
                    {
                        ErrorMessage("Not set voltage offset");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                }

            }
        }

        private void getMotorPositionButton_Click(object sender, EventArgs e)
        {
            if (IsMotorConnected())
            {
                LogMessage($"Motor position = {GetMotorPosition()} mm");
            }
        }

        private void noScanPointsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimeStepLabel();
            Settings.Default.noScanPoints = (int)noScanPointsNumericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void fitGaussianCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.fitGaussian = fitGaussianCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void useMaxVoltageInScopeTraceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            useMeanVoltageInScopeTraceRadioButton.Checked = !useMaxVoltageInScopeTraceRadioButton.Checked;
        }

        private void useMinVoltageInScopeTraceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            useMaxVoltageInScopeTraceRadioButton.Checked = !useMeanVoltageInScopeTraceRadioButton.Checked;
        }

        private void retrieveScopeTraceButton_Click(object sender, EventArgs e)
        {
            //io.RawIO.Write("TIM:SCAL 1E-7");
            if (IsScopeConnected())
            {
                //io.Timeout = 20000; // 20 seconds
                io.TerminationCharacterEnabled = true;
                io.TerminationCharacter = (byte)'\n';
                io.RawIO.Write("CHAN:DATA?");
             
                var idnResponse = io.RawIO.ReadString();
                //LogMessage(idnResponse.ToString());
                List<double> dataPoints = new List<double>(); // Initialize dataPoints
                try
                {
                    string[] values = idnResponse.Split(','); // Split by commas
                    dataPoints = values
                        .Select(v => double.TryParse(v, out double result) ? result : double.NaN) // Convert to double safely
                        .Where(v => !double.IsNaN(v)) // Remove invalid entries
                        .ToList();
                    //LogMessage(dataPoints.ToString());
                    double maxVoltage = dataPoints.Max(); // Return the maximum value from the data points
                    LogMessage("Max voltage: " + maxVoltage.ToString());

                    Series scopeTrace = new Series("ScopeTrace")
                    {
                        ChartType = SeriesChartType.Line, // Use points instead of a line
                                                          //MarkerStyle = MarkerStyle.Circle,   // Set marker style to cross
                                                          //MarkerSize = 5                    // Adjust size for visibility
                    };
                    NotificationMessage("Configuring chart ...");
                    chart2.Series.Clear(); // Clear the chart at the beginning
                    chart2.Series.Add(scopeTrace);
                    // Customize chart
                    chart2.ChartAreas[0].AxisX.Title = "Time";
                    chart2.ChartAreas[0].AxisY.Title = "Voltage [V]";
                    chart2.Legends.Clear(); // Remove the legend

                    // Format the X axis labels to 2 decimal places
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "F2";

                    int N = dataPoints.Count;

                    // Create time axis
                    List<double> timeAxis = new List<double>(N);
                    io.RawIO.Write("ACQ:SRAT?");
                    double sampleRate = double.Parse(io.RawIO.ReadString(), CultureInfo.InvariantCulture);

                    double dt = 1.0 / sampleRate;
                    for (int i = 0; i < N; i++)
                    {
                        timeAxis.Add(i * dt);
                    }
                    for (int i = 0; i < N; i++)
                    {
                        scopeTrace.Points.AddXY(timeAxis[i], dataPoints[i]);
                    }
                    chart2.Invalidate();
                    LogMessage("Number of points acquired: " + N);
                }
                catch
                {
                    ErrorMessage("Not converting comma separated string");
                    //return double.NaN; // Return a default value in case of error
                }
            }
                //return dataPoints.Max(); // Return the maximum value from the data points
            }
        }

    internal class GcsCommandError : Exception
    {
        public GcsCommandError(string message)
            : base(message)
        {
        }
    }
}
