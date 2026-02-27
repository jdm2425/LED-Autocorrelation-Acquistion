namespace LED_Autocorrelator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel2 = new System.Windows.Forms.Panel();
            this.connectToScopeButton = new System.Windows.Forms.Button();
            this.enableMotorButton = new System.Windows.Forms.Button();
            this.disableMotorButton = new System.Windows.Forms.Button();
            this.scopeComboBox = new System.Windows.Forms.ComboBox();
            this.scopeLabel = new System.Windows.Forms.Label();
            this.motorUSBComboBox = new System.Windows.Forms.ComboBox();
            this.motorLabel = new System.Windows.Forms.Label();
            this.motorIdentifyButton = new System.Windows.Forms.Button();
            this.DevicesLabel = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.MessageBox = new System.Windows.Forms.RichTextBox();
            this.ClearMessageBoxButton = new System.Windows.Forms.Button();
            this.MessagesLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.fitParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.fitGaussianCheckBox = new System.Windows.Forms.CheckBox();
            this.pulseDurationsFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.savePulseDurationCheckBox = new System.Windows.Forms.CheckBox();
            this.pulseDurationLabel = new System.Windows.Forms.Label();
            this.scanGroupBox = new System.Windows.Forms.GroupBox();
            this.averageOverEachMeasurementNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AvgEachMeasurementOverLabel = new System.Windows.Forms.Label();
            this.timeStepLabel = new System.Windows.Forms.Label();
            this.noScanPointsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.optionsForVoltageExtractionForTraceDropDown = new System.Windows.Forms.ComboBox();
            this.motorSpeedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.motorSpeedLabel = new System.Windows.Forms.Label();
            this.continuousStepsRadioButton = new System.Windows.Forms.RadioButton();
            this.timeBetweenStepsLabel = new System.Windows.Forms.Label();
            this.timeBetweenStepsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.discreteStepsRadioButton = new System.Windows.Forms.RadioButton();
            this.iterateFilenamesCheckBox = new System.Windows.Forms.CheckBox();
            this.folderSaveDialogButton = new System.Windows.Forms.Button();
            this.saveFolderPathLabel = new System.Windows.Forms.Label();
            this.saveLocationTextBox = new System.Windows.Forms.TextBox();
            this.stopBasicScanButton = new System.Windows.Forms.Button();
            this.numberOfRunsLabel = new System.Windows.Forms.Label();
            this.numberOfRunsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.runContinuouslyCheckBox = new System.Windows.Forms.CheckBox();
            this.delayLabel = new System.Windows.Forms.Label();
            this.basicScanFileNameLabel = new System.Windows.Forms.Label();
            this.basicScanFileNameTextBox = new System.Windows.Forms.TextBox();
            this.saveScanResultsCheckBox = new System.Windows.Forms.CheckBox();
            this.startBasicScanButton = new System.Windows.Forms.Button();
            this.numberOfScanPointsLabel = new System.Windows.Forms.Label();
            this.endScanPositionLabel = new System.Windows.Forms.Label();
            this.motorEndPositionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.motorStartPositionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.startScanPositionLabel = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BasicScanBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.saveLocationFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.triggeringLabel = new System.Windows.Forms.Label();
            this.getMotorPositionButton = new System.Windows.Forms.Button();
            this.voltLabel = new System.Windows.Forms.Label();
            this.trigLevelNumericDropDown = new System.Windows.Forms.NumericUpDown();
            this.triggerLevelLabel = new System.Windows.Forms.Label();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.voltageOffsetNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.voltsLabel = new System.Windows.Forms.Label();
            this.voltsPerDivNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.VoltsPerDivLabel = new System.Windows.Forms.Label();
            this.sLabel = new System.Windows.Forms.Label();
            this.setTimeBaseLabel = new System.Windows.Forms.Label();
            this.scopeSettingsLabel = new System.Windows.Forms.Label();
            this.setTimeBaseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mmLabel = new System.Windows.Forms.Label();
            this.motorCommandsLabel = new System.Windows.Forms.Label();
            this.manuaMoveMotorButton = new System.Windows.Forms.Button();
            this.moveMotorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.notificationTextBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.useTimebaseInVoltageAcquisitionCheckBox = new System.Windows.Forms.CheckBox();
            this.useMaxVoltageInScopeTraceRadioButton = new System.Windows.Forms.RadioButton();
            this.useMeanVoltageInScopeTraceRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.fitParametersGroupBox.SuspendLayout();
            this.scanGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.averageOverEachMeasurementNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noScanPointsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorSpeedNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBetweenStepsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRunsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorEndPositionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorStartPositionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trigLevelNumericDropDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltageOffsetNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltsPerDivNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setTimeBaseNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveMotorNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.connectToScopeButton);
            this.panel2.Controls.Add(this.enableMotorButton);
            this.panel2.Controls.Add(this.disableMotorButton);
            this.panel2.Controls.Add(this.scopeComboBox);
            this.panel2.Controls.Add(this.scopeLabel);
            this.panel2.Controls.Add(this.motorUSBComboBox);
            this.panel2.Controls.Add(this.motorLabel);
            this.panel2.Controls.Add(this.motorIdentifyButton);
            this.panel2.Location = new System.Drawing.Point(12, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 57);
            this.panel2.TabIndex = 4;
            // 
            // connectToScopeButton
            // 
            this.connectToScopeButton.Location = new System.Drawing.Point(176, 27);
            this.connectToScopeButton.Name = "connectToScopeButton";
            this.connectToScopeButton.Size = new System.Drawing.Size(75, 23);
            this.connectToScopeButton.TabIndex = 26;
            this.connectToScopeButton.Text = "Connect";
            this.connectToScopeButton.UseVisualStyleBackColor = true;
            this.connectToScopeButton.Click += new System.EventHandler(this.connectToScopeButton_Click);
            // 
            // enableMotorButton
            // 
            this.enableMotorButton.Location = new System.Drawing.Point(284, 6);
            this.enableMotorButton.Name = "enableMotorButton";
            this.enableMotorButton.Size = new System.Drawing.Size(53, 23);
            this.enableMotorButton.TabIndex = 25;
            this.enableMotorButton.Text = "Enable";
            this.enableMotorButton.UseVisualStyleBackColor = true;
            this.enableMotorButton.Click += new System.EventHandler(this.enableMotorButton_Click);
            // 
            // disableMotorButton
            // 
            this.disableMotorButton.Location = new System.Drawing.Point(231, 6);
            this.disableMotorButton.Name = "disableMotorButton";
            this.disableMotorButton.Size = new System.Drawing.Size(55, 23);
            this.disableMotorButton.TabIndex = 24;
            this.disableMotorButton.Text = "Disable";
            this.disableMotorButton.UseVisualStyleBackColor = true;
            this.disableMotorButton.Click += new System.EventHandler(this.disableMotorButton_Click);
            // 
            // scopeComboBox
            // 
            this.scopeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scopeComboBox.FormattingEnabled = true;
            this.scopeComboBox.Location = new System.Drawing.Point(53, 29);
            this.scopeComboBox.Name = "scopeComboBox";
            this.scopeComboBox.Size = new System.Drawing.Size(121, 21);
            this.scopeComboBox.TabIndex = 23;
            this.scopeComboBox.SelectedIndexChanged += new System.EventHandler(this.scopeComboBox_SelectedIndexChanged);
            // 
            // scopeLabel
            // 
            this.scopeLabel.AutoSize = true;
            this.scopeLabel.Location = new System.Drawing.Point(6, 32);
            this.scopeLabel.Name = "scopeLabel";
            this.scopeLabel.Size = new System.Drawing.Size(41, 13);
            this.scopeLabel.TabIndex = 22;
            this.scopeLabel.Text = "Scope:";
            // 
            // motorUSBComboBox
            // 
            this.motorUSBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.motorUSBComboBox.FormattingEnabled = true;
            this.motorUSBComboBox.Location = new System.Drawing.Point(53, 6);
            this.motorUSBComboBox.Name = "motorUSBComboBox";
            this.motorUSBComboBox.Size = new System.Drawing.Size(121, 21);
            this.motorUSBComboBox.TabIndex = 1;
            this.motorUSBComboBox.DropDown += new System.EventHandler(this.motorUSBComboBox_DropDown);
            this.motorUSBComboBox.SelectedIndexChanged += new System.EventHandler(this.motorUSBComboBox_SelectedIndexChanged);
            // 
            // motorLabel
            // 
            this.motorLabel.AutoSize = true;
            this.motorLabel.Location = new System.Drawing.Point(4, 7);
            this.motorLabel.Name = "motorLabel";
            this.motorLabel.Size = new System.Drawing.Size(37, 13);
            this.motorLabel.TabIndex = 0;
            this.motorLabel.Text = "Motor:";
            // 
            // motorIdentifyButton
            // 
            this.motorIdentifyButton.Location = new System.Drawing.Point(176, 6);
            this.motorIdentifyButton.Name = "motorIdentifyButton";
            this.motorIdentifyButton.Size = new System.Drawing.Size(57, 23);
            this.motorIdentifyButton.TabIndex = 21;
            this.motorIdentifyButton.Text = "Identify";
            this.motorIdentifyButton.UseVisualStyleBackColor = true;
            this.motorIdentifyButton.Click += new System.EventHandler(this.identifyButton_Click);
            // 
            // DevicesLabel
            // 
            this.DevicesLabel.AutoSize = true;
            this.DevicesLabel.Location = new System.Drawing.Point(12, 30);
            this.DevicesLabel.Name = "DevicesLabel";
            this.DevicesLabel.Size = new System.Drawing.Size(49, 13);
            this.DevicesLabel.TabIndex = 12;
            this.DevicesLabel.Text = "Devices:";
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.IndianRed;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitButton.Location = new System.Drawing.Point(830, 789);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(109, 24);
            this.ExitButton.TabIndex = 13;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // MessageBox
            // 
            this.MessageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessageBox.Location = new System.Drawing.Point(12, 434);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ReadOnly = true;
            this.MessageBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.MessageBox.Size = new System.Drawing.Size(336, 348);
            this.MessageBox.TabIndex = 14;
            this.MessageBox.Text = "";
            // 
            // ClearMessageBoxButton
            // 
            this.ClearMessageBoxButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClearMessageBoxButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearMessageBoxButton.Location = new System.Drawing.Point(0, 788);
            this.ClearMessageBoxButton.Name = "ClearMessageBoxButton";
            this.ClearMessageBoxButton.Size = new System.Drawing.Size(174, 24);
            this.ClearMessageBoxButton.TabIndex = 15;
            this.ClearMessageBoxButton.Text = "Clear Messages";
            this.ClearMessageBoxButton.UseVisualStyleBackColor = false;
            this.ClearMessageBoxButton.Click += new System.EventHandler(this.ClearMessageBoxButton_Click);
            // 
            // MessagesLabel
            // 
            this.MessagesLabel.AutoSize = true;
            this.MessagesLabel.Location = new System.Drawing.Point(9, 414);
            this.MessagesLabel.Name = "MessagesLabel";
            this.MessagesLabel.Size = new System.Drawing.Size(58, 13);
            this.MessagesLabel.TabIndex = 16;
            this.MessagesLabel.Text = "Messages:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(945, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.userGuideToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.CheckOnClick = true;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.userGuideToolStripMenuItem.Text = "User guide";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.chart2);
            this.panel1.Controls.Add(this.fitParametersGroupBox);
            this.panel1.Controls.Add(this.scanGroupBox);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Location = new System.Drawing.Point(380, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 739);
            this.panel1.TabIndex = 18;
            // 
            // chart2
            // 
            chartArea5.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart2.Legends.Add(legend5);
            this.chart2.Location = new System.Drawing.Point(3, 270);
            this.chart2.Name = "chart2";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart2.Series.Add(series5);
            this.chart2.Size = new System.Drawing.Size(547, 123);
            this.chart2.TabIndex = 25;
            this.chart2.Text = "chart2";
            // 
            // fitParametersGroupBox
            // 
            this.fitParametersGroupBox.Controls.Add(this.fitGaussianCheckBox);
            this.fitParametersGroupBox.Controls.Add(this.pulseDurationsFileNameTextBox);
            this.fitParametersGroupBox.Controls.Add(this.label1);
            this.fitParametersGroupBox.Controls.Add(this.savePulseDurationCheckBox);
            this.fitParametersGroupBox.Controls.Add(this.pulseDurationLabel);
            this.fitParametersGroupBox.Location = new System.Drawing.Point(9, 676);
            this.fitParametersGroupBox.Name = "fitParametersGroupBox";
            this.fitParametersGroupBox.Size = new System.Drawing.Size(534, 60);
            this.fitParametersGroupBox.TabIndex = 24;
            this.fitParametersGroupBox.TabStop = false;
            this.fitParametersGroupBox.Text = "Fit Parameters";
            // 
            // fitGaussianCheckBox
            // 
            this.fitGaussianCheckBox.AutoSize = true;
            this.fitGaussianCheckBox.Checked = true;
            this.fitGaussianCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fitGaussianCheckBox.Location = new System.Drawing.Point(441, 20);
            this.fitGaussianCheckBox.Name = "fitGaussianCheckBox";
            this.fitGaussianCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fitGaussianCheckBox.Size = new System.Drawing.Size(88, 17);
            this.fitGaussianCheckBox.TabIndex = 26;
            this.fitGaussianCheckBox.Text = "?Fit gaussian";
            this.fitGaussianCheckBox.UseVisualStyleBackColor = true;
            this.fitGaussianCheckBox.CheckedChanged += new System.EventHandler(this.fitGaussianCheckBox_CheckedChanged);
            // 
            // pulseDurationsFileNameTextBox
            // 
            this.pulseDurationsFileNameTextBox.Enabled = false;
            this.pulseDurationsFileNameTextBox.Location = new System.Drawing.Point(208, 17);
            this.pulseDurationsFileNameTextBox.Name = "pulseDurationsFileNameTextBox";
            this.pulseDurationsFileNameTextBox.Size = new System.Drawing.Size(221, 20);
            this.pulseDurationsFileNameTextBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "File name:";
            // 
            // savePulseDurationCheckBox
            // 
            this.savePulseDurationCheckBox.AutoSize = true;
            this.savePulseDurationCheckBox.Location = new System.Drawing.Point(6, 19);
            this.savePulseDurationCheckBox.Name = "savePulseDurationCheckBox";
            this.savePulseDurationCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.savePulseDurationCheckBox.Size = new System.Drawing.Size(131, 17);
            this.savePulseDurationCheckBox.TabIndex = 20;
            this.savePulseDurationCheckBox.Text = "?Save pulse durations";
            this.savePulseDurationCheckBox.UseVisualStyleBackColor = true;
            this.savePulseDurationCheckBox.CheckedChanged += new System.EventHandler(this.savePulseDurationCheckBox_CheckedChanged);
            // 
            // pulseDurationLabel
            // 
            this.pulseDurationLabel.AutoSize = true;
            this.pulseDurationLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.pulseDurationLabel.Location = new System.Drawing.Point(6, 39);
            this.pulseDurationLabel.Name = "pulseDurationLabel";
            this.pulseDurationLabel.Size = new System.Drawing.Size(104, 13);
            this.pulseDurationLabel.TabIndex = 2;
            this.pulseDurationLabel.Text = "Pulse duration: ____";
            // 
            // scanGroupBox
            // 
            this.scanGroupBox.Controls.Add(this.averageOverEachMeasurementNumericUpDown);
            this.scanGroupBox.Controls.Add(this.AvgEachMeasurementOverLabel);
            this.scanGroupBox.Controls.Add(this.timeStepLabel);
            this.scanGroupBox.Controls.Add(this.noScanPointsNumericUpDown);
            this.scanGroupBox.Controls.Add(this.optionsForVoltageExtractionForTraceDropDown);
            this.scanGroupBox.Controls.Add(this.motorSpeedNumericUpDown);
            this.scanGroupBox.Controls.Add(this.motorSpeedLabel);
            this.scanGroupBox.Controls.Add(this.continuousStepsRadioButton);
            this.scanGroupBox.Controls.Add(this.timeBetweenStepsLabel);
            this.scanGroupBox.Controls.Add(this.timeBetweenStepsNumericUpDown);
            this.scanGroupBox.Controls.Add(this.discreteStepsRadioButton);
            this.scanGroupBox.Controls.Add(this.iterateFilenamesCheckBox);
            this.scanGroupBox.Controls.Add(this.folderSaveDialogButton);
            this.scanGroupBox.Controls.Add(this.saveFolderPathLabel);
            this.scanGroupBox.Controls.Add(this.saveLocationTextBox);
            this.scanGroupBox.Controls.Add(this.stopBasicScanButton);
            this.scanGroupBox.Controls.Add(this.numberOfRunsLabel);
            this.scanGroupBox.Controls.Add(this.numberOfRunsNumericUpDown);
            this.scanGroupBox.Controls.Add(this.runContinuouslyCheckBox);
            this.scanGroupBox.Controls.Add(this.delayLabel);
            this.scanGroupBox.Controls.Add(this.basicScanFileNameLabel);
            this.scanGroupBox.Controls.Add(this.basicScanFileNameTextBox);
            this.scanGroupBox.Controls.Add(this.saveScanResultsCheckBox);
            this.scanGroupBox.Controls.Add(this.startBasicScanButton);
            this.scanGroupBox.Controls.Add(this.numberOfScanPointsLabel);
            this.scanGroupBox.Controls.Add(this.endScanPositionLabel);
            this.scanGroupBox.Controls.Add(this.motorEndPositionNumericUpDown);
            this.scanGroupBox.Controls.Add(this.motorStartPositionNumericUpDown);
            this.scanGroupBox.Controls.Add(this.startScanPositionLabel);
            this.scanGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.scanGroupBox.Location = new System.Drawing.Point(7, 456);
            this.scanGroupBox.Name = "scanGroupBox";
            this.scanGroupBox.Size = new System.Drawing.Size(528, 214);
            this.scanGroupBox.TabIndex = 1;
            this.scanGroupBox.TabStop = false;
            this.scanGroupBox.Text = "Scan Details";
            // 
            // averageOverEachMeasurementNumericUpDown
            // 
            this.averageOverEachMeasurementNumericUpDown.Location = new System.Drawing.Point(85, 38);
            this.averageOverEachMeasurementNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.averageOverEachMeasurementNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.averageOverEachMeasurementNumericUpDown.Name = "averageOverEachMeasurementNumericUpDown";
            this.averageOverEachMeasurementNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.averageOverEachMeasurementNumericUpDown.TabIndex = 25;
            this.averageOverEachMeasurementNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AvgEachMeasurementOverLabel
            // 
            this.AvgEachMeasurementOverLabel.AutoSize = true;
            this.AvgEachMeasurementOverLabel.Location = new System.Drawing.Point(4, 40);
            this.AvgEachMeasurementOverLabel.Name = "AvgEachMeasurementOverLabel";
            this.AvgEachMeasurementOverLabel.Size = new System.Drawing.Size(76, 13);
            this.AvgEachMeasurementOverLabel.TabIndex = 43;
            this.AvgEachMeasurementOverLabel.Text = "Average Over:";
            // 
            // timeStepLabel
            // 
            this.timeStepLabel.AutoSize = true;
            this.timeStepLabel.ForeColor = System.Drawing.Color.Red;
            this.timeStepLabel.Location = new System.Drawing.Point(294, 112);
            this.timeStepLabel.Name = "timeStepLabel";
            this.timeStepLabel.Size = new System.Drawing.Size(89, 13);
            this.timeStepLabel.TabIndex = 28;
            this.timeStepLabel.Text = "Time step: _____";
            // 
            // noScanPointsNumericUpDown
            // 
            this.noScanPointsNumericUpDown.Location = new System.Drawing.Point(466, 14);
            this.noScanPointsNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.noScanPointsNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.noScanPointsNumericUpDown.Name = "noScanPointsNumericUpDown";
            this.noScanPointsNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.noScanPointsNumericUpDown.TabIndex = 6;
            this.noScanPointsNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.noScanPointsNumericUpDown.ValueChanged += new System.EventHandler(this.noScanPointsNumericUpDown_ValueChanged);
            // 
            // optionsForVoltageExtractionForTraceDropDown
            // 
            this.optionsForVoltageExtractionForTraceDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optionsForVoltageExtractionForTraceDropDown.FormattingEnabled = true;
            this.optionsForVoltageExtractionForTraceDropDown.Items.AddRange(new object[] {
            "Maximum",
            "Average"});
            this.optionsForVoltageExtractionForTraceDropDown.Location = new System.Drawing.Point(262, 186);
            this.optionsForVoltageExtractionForTraceDropDown.Name = "optionsForVoltageExtractionForTraceDropDown";
            this.optionsForVoltageExtractionForTraceDropDown.Size = new System.Drawing.Size(121, 21);
            this.optionsForVoltageExtractionForTraceDropDown.TabIndex = 27;
            this.optionsForVoltageExtractionForTraceDropDown.Visible = false;
            // 
            // motorSpeedNumericUpDown
            // 
            this.motorSpeedNumericUpDown.Enabled = false;
            this.motorSpeedNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.motorSpeedNumericUpDown.Location = new System.Drawing.Point(338, 90);
            this.motorSpeedNumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.motorSpeedNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.motorSpeedNumericUpDown.Name = "motorSpeedNumericUpDown";
            this.motorSpeedNumericUpDown.Size = new System.Drawing.Size(79, 20);
            this.motorSpeedNumericUpDown.TabIndex = 25;
            this.motorSpeedNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.motorSpeedNumericUpDown.ValueChanged += new System.EventHandler(this.motorSpeedNumericUpDown_ValueChanged);
            // 
            // motorSpeedLabel
            // 
            this.motorSpeedLabel.AutoSize = true;
            this.motorSpeedLabel.Location = new System.Drawing.Point(256, 92);
            this.motorSpeedLabel.Name = "motorSpeedLabel";
            this.motorSpeedLabel.Size = new System.Drawing.Size(76, 13);
            this.motorSpeedLabel.TabIndex = 24;
            this.motorSpeedLabel.Text = "Speed (mm/s):";
            // 
            // continuousStepsRadioButton
            // 
            this.continuousStepsRadioButton.AutoSize = true;
            this.continuousStepsRadioButton.Location = new System.Drawing.Point(324, 64);
            this.continuousStepsRadioButton.Name = "continuousStepsRadioButton";
            this.continuousStepsRadioButton.Size = new System.Drawing.Size(112, 17);
            this.continuousStepsRadioButton.TabIndex = 23;
            this.continuousStepsRadioButton.Text = "Continuous motion";
            this.continuousStepsRadioButton.UseVisualStyleBackColor = true;
            this.continuousStepsRadioButton.CheckedChanged += new System.EventHandler(this.continuousStepsRadioButton_CheckedChanged);
            // 
            // timeBetweenStepsLabel
            // 
            this.timeBetweenStepsLabel.AutoSize = true;
            this.timeBetweenStepsLabel.Location = new System.Drawing.Point(6, 92);
            this.timeBetweenStepsLabel.Name = "timeBetweenStepsLabel";
            this.timeBetweenStepsLabel.Size = new System.Drawing.Size(100, 13);
            this.timeBetweenStepsLabel.TabIndex = 22;
            this.timeBetweenStepsLabel.Text = "Time btx steps (ms):";
            // 
            // timeBetweenStepsNumericUpDown
            // 
            this.timeBetweenStepsNumericUpDown.Location = new System.Drawing.Point(112, 89);
            this.timeBetweenStepsNumericUpDown.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.timeBetweenStepsNumericUpDown.Name = "timeBetweenStepsNumericUpDown";
            this.timeBetweenStepsNumericUpDown.Size = new System.Drawing.Size(79, 20);
            this.timeBetweenStepsNumericUpDown.TabIndex = 21;
            // 
            // discreteStepsRadioButton
            // 
            this.discreteStepsRadioButton.AutoSize = true;
            this.discreteStepsRadioButton.Checked = true;
            this.discreteStepsRadioButton.Location = new System.Drawing.Point(58, 64);
            this.discreteStepsRadioButton.Name = "discreteStepsRadioButton";
            this.discreteStepsRadioButton.Size = new System.Drawing.Size(92, 17);
            this.discreteStepsRadioButton.TabIndex = 19;
            this.discreteStepsRadioButton.TabStop = true;
            this.discreteStepsRadioButton.Text = "Discrete steps";
            this.discreteStepsRadioButton.UseVisualStyleBackColor = true;
            this.discreteStepsRadioButton.CheckedChanged += new System.EventHandler(this.discreteStepsRadioButton_CheckedChanged);
            // 
            // iterateFilenamesCheckBox
            // 
            this.iterateFilenamesCheckBox.AutoSize = true;
            this.iterateFilenamesCheckBox.Enabled = false;
            this.iterateFilenamesCheckBox.Location = new System.Drawing.Point(366, 161);
            this.iterateFilenamesCheckBox.Name = "iterateFilenamesCheckBox";
            this.iterateFilenamesCheckBox.Size = new System.Drawing.Size(103, 17);
            this.iterateFilenamesCheckBox.TabIndex = 18;
            this.iterateFilenamesCheckBox.Text = "Iterate filenames";
            this.iterateFilenamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // folderSaveDialogButton
            // 
            this.folderSaveDialogButton.Enabled = false;
            this.folderSaveDialogButton.Image = ((System.Drawing.Image)(resources.GetObject("folderSaveDialogButton.Image")));
            this.folderSaveDialogButton.Location = new System.Drawing.Point(475, 132);
            this.folderSaveDialogButton.Name = "folderSaveDialogButton";
            this.folderSaveDialogButton.Size = new System.Drawing.Size(48, 23);
            this.folderSaveDialogButton.TabIndex = 17;
            this.folderSaveDialogButton.UseVisualStyleBackColor = true;
            this.folderSaveDialogButton.Click += new System.EventHandler(this.folderSaveDialogButton_Click);
            // 
            // saveFolderPathLabel
            // 
            this.saveFolderPathLabel.AutoSize = true;
            this.saveFolderPathLabel.Location = new System.Drawing.Point(8, 137);
            this.saveFolderPathLabel.Name = "saveFolderPathLabel";
            this.saveFolderPathLabel.Size = new System.Drawing.Size(75, 13);
            this.saveFolderPathLabel.TabIndex = 16;
            this.saveFolderPathLabel.Text = "Save location:";
            // 
            // saveLocationTextBox
            // 
            this.saveLocationTextBox.Enabled = false;
            this.saveLocationTextBox.Location = new System.Drawing.Point(89, 134);
            this.saveLocationTextBox.Name = "saveLocationTextBox";
            this.saveLocationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.saveLocationTextBox.Size = new System.Drawing.Size(380, 20);
            this.saveLocationTextBox.TabIndex = 15;
            // 
            // stopBasicScanButton
            // 
            this.stopBasicScanButton.Enabled = false;
            this.stopBasicScanButton.Location = new System.Drawing.Point(445, 184);
            this.stopBasicScanButton.Name = "stopBasicScanButton";
            this.stopBasicScanButton.Size = new System.Drawing.Size(75, 23);
            this.stopBasicScanButton.TabIndex = 14;
            this.stopBasicScanButton.Text = "Stop Scan!";
            this.stopBasicScanButton.UseVisualStyleBackColor = true;
            this.stopBasicScanButton.Visible = false;
            this.stopBasicScanButton.Click += new System.EventHandler(this.stopBasicScanButton_Click);
            // 
            // numberOfRunsLabel
            // 
            this.numberOfRunsLabel.AutoSize = true;
            this.numberOfRunsLabel.Location = new System.Drawing.Point(123, 189);
            this.numberOfRunsLabel.Name = "numberOfRunsLabel";
            this.numberOfRunsLabel.Size = new System.Drawing.Size(82, 13);
            this.numberOfRunsLabel.TabIndex = 13;
            this.numberOfRunsLabel.Text = "Number of runs:";
            // 
            // numberOfRunsNumericUpDown
            // 
            this.numberOfRunsNumericUpDown.Location = new System.Drawing.Point(208, 186);
            this.numberOfRunsNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberOfRunsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfRunsNumericUpDown.Name = "numberOfRunsNumericUpDown";
            this.numberOfRunsNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.numberOfRunsNumericUpDown.TabIndex = 12;
            this.numberOfRunsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // runContinuouslyCheckBox
            // 
            this.runContinuouslyCheckBox.AutoSize = true;
            this.runContinuouslyCheckBox.Location = new System.Drawing.Point(9, 188);
            this.runContinuouslyCheckBox.Name = "runContinuouslyCheckBox";
            this.runContinuouslyCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.runContinuouslyCheckBox.Size = new System.Drawing.Size(108, 17);
            this.runContinuouslyCheckBox.TabIndex = 11;
            this.runContinuouslyCheckBox.Text = "Run continuously";
            this.runContinuouslyCheckBox.UseVisualStyleBackColor = true;
            this.runContinuouslyCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.ForeColor = System.Drawing.Color.Red;
            this.delayLabel.Location = new System.Drawing.Point(429, 113);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(93, 13);
            this.delayLabel.TabIndex = 10;
            this.delayLabel.Text = "Delay = 33.300 ps";
            // 
            // basicScanFileNameLabel
            // 
            this.basicScanFileNameLabel.AutoSize = true;
            this.basicScanFileNameLabel.Location = new System.Drawing.Point(10, 162);
            this.basicScanFileNameLabel.Name = "basicScanFileNameLabel";
            this.basicScanFileNameLabel.Size = new System.Drawing.Size(55, 13);
            this.basicScanFileNameLabel.TabIndex = 9;
            this.basicScanFileNameLabel.Text = "File name:";
            // 
            // basicScanFileNameTextBox
            // 
            this.basicScanFileNameTextBox.Enabled = false;
            this.basicScanFileNameTextBox.Location = new System.Drawing.Point(90, 159);
            this.basicScanFileNameTextBox.Name = "basicScanFileNameTextBox";
            this.basicScanFileNameTextBox.Size = new System.Drawing.Size(270, 20);
            this.basicScanFileNameTextBox.TabIndex = 8;
            // 
            // saveScanResultsCheckBox
            // 
            this.saveScanResultsCheckBox.AutoSize = true;
            this.saveScanResultsCheckBox.Location = new System.Drawing.Point(6, 112);
            this.saveScanResultsCheckBox.Name = "saveScanResultsCheckBox";
            this.saveScanResultsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.saveScanResultsCheckBox.Size = new System.Drawing.Size(116, 17);
            this.saveScanResultsCheckBox.TabIndex = 7;
            this.saveScanResultsCheckBox.Text = "?Save scan results";
            this.saveScanResultsCheckBox.UseVisualStyleBackColor = true;
            this.saveScanResultsCheckBox.CheckedChanged += new System.EventHandler(this.saveScanResultsCheckBox_CheckedChanged);
            // 
            // startBasicScanButton
            // 
            this.startBasicScanButton.Location = new System.Drawing.Point(445, 184);
            this.startBasicScanButton.Name = "startBasicScanButton";
            this.startBasicScanButton.Size = new System.Drawing.Size(75, 23);
            this.startBasicScanButton.TabIndex = 6;
            this.startBasicScanButton.Text = "Start Scan!";
            this.startBasicScanButton.UseVisualStyleBackColor = true;
            this.startBasicScanButton.Click += new System.EventHandler(this.startBasicScanButton_Click);
            // 
            // numberOfScanPointsLabel
            // 
            this.numberOfScanPointsLabel.AutoSize = true;
            this.numberOfScanPointsLabel.Location = new System.Drawing.Point(403, 16);
            this.numberOfScanPointsLabel.Name = "numberOfScanPointsLabel";
            this.numberOfScanPointsLabel.Size = new System.Drawing.Size(59, 13);
            this.numberOfScanPointsLabel.TabIndex = 5;
            this.numberOfScanPointsLabel.Text = "No. Points:";
            // 
            // endScanPositionLabel
            // 
            this.endScanPositionLabel.AutoSize = true;
            this.endScanPositionLabel.Location = new System.Drawing.Point(205, 16);
            this.endScanPositionLabel.Name = "endScanPositionLabel";
            this.endScanPositionLabel.Size = new System.Drawing.Size(105, 13);
            this.endScanPositionLabel.TabIndex = 4;
            this.endScanPositionLabel.Text = "Motor End Pos (mm):";
            // 
            // motorEndPositionNumericUpDown
            // 
            this.motorEndPositionNumericUpDown.DecimalPlaces = 10;
            this.motorEndPositionNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.motorEndPositionNumericUpDown.Location = new System.Drawing.Point(311, 14);
            this.motorEndPositionNumericUpDown.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.motorEndPositionNumericUpDown.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.motorEndPositionNumericUpDown.Name = "motorEndPositionNumericUpDown";
            this.motorEndPositionNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.motorEndPositionNumericUpDown.TabIndex = 3;
            this.motorEndPositionNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.motorEndPositionNumericUpDown.ValueChanged += new System.EventHandler(this.motorEndPositionNumericUpDown_ValueChanged);
            // 
            // motorStartPositionNumericUpDown
            // 
            this.motorStartPositionNumericUpDown.DecimalPlaces = 10;
            this.motorStartPositionNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.motorStartPositionNumericUpDown.Location = new System.Drawing.Point(109, 14);
            this.motorStartPositionNumericUpDown.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.motorStartPositionNumericUpDown.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.motorStartPositionNumericUpDown.Name = "motorStartPositionNumericUpDown";
            this.motorStartPositionNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.motorStartPositionNumericUpDown.TabIndex = 2;
            this.motorStartPositionNumericUpDown.ValueChanged += new System.EventHandler(this.motorStartPositionNumericUpDown_ValueChanged);
            // 
            // startScanPositionLabel
            // 
            this.startScanPositionLabel.AutoSize = true;
            this.startScanPositionLabel.Location = new System.Drawing.Point(3, 16);
            this.startScanPositionLabel.Name = "startScanPositionLabel";
            this.startScanPositionLabel.Size = new System.Drawing.Size(108, 13);
            this.startScanPositionLabel.TabIndex = 0;
            this.startScanPositionLabel.Text = "Motor Start Pos (mm):";
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(547, 245);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.useMeanVoltageInScopeTraceRadioButton);
            this.panel3.Controls.Add(this.useMaxVoltageInScopeTraceRadioButton);
            this.panel3.Controls.Add(this.triggeringLabel);
            this.panel3.Controls.Add(this.getMotorPositionButton);
            this.panel3.Controls.Add(this.voltLabel);
            this.panel3.Controls.Add(this.trigLevelNumericDropDown);
            this.panel3.Controls.Add(this.triggerLevelLabel);
            this.panel3.Controls.Add(this.offsetLabel);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.voltageOffsetNumericUpDown);
            this.panel3.Controls.Add(this.voltsLabel);
            this.panel3.Controls.Add(this.voltsPerDivNumericUpDown);
            this.panel3.Controls.Add(this.VoltsPerDivLabel);
            this.panel3.Controls.Add(this.sLabel);
            this.panel3.Controls.Add(this.setTimeBaseLabel);
            this.panel3.Controls.Add(this.scopeSettingsLabel);
            this.panel3.Controls.Add(this.setTimeBaseNumericUpDown);
            this.panel3.Controls.Add(this.mmLabel);
            this.panel3.Controls.Add(this.motorCommandsLabel);
            this.panel3.Controls.Add(this.manuaMoveMotorButton);
            this.panel3.Controls.Add(this.moveMotorNumericUpDown);
            this.panel3.Location = new System.Drawing.Point(12, 109);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 299);
            this.panel3.TabIndex = 26;
            // 
            // triggeringLabel
            // 
            this.triggeringLabel.AutoSize = true;
            this.triggeringLabel.Location = new System.Drawing.Point(6, 144);
            this.triggeringLabel.Name = "triggeringLabel";
            this.triggeringLabel.Size = new System.Drawing.Size(57, 13);
            this.triggeringLabel.TabIndex = 43;
            this.triggeringLabel.Text = "Triggering:";
            // 
            // getMotorPositionButton
            // 
            this.getMotorPositionButton.Location = new System.Drawing.Point(218, 24);
            this.getMotorPositionButton.Name = "getMotorPositionButton";
            this.getMotorPositionButton.Size = new System.Drawing.Size(118, 23);
            this.getMotorPositionButton.TabIndex = 42;
            this.getMotorPositionButton.Text = "Get motor pos:";
            this.getMotorPositionButton.UseVisualStyleBackColor = true;
            this.getMotorPositionButton.Click += new System.EventHandler(this.getMotorPositionButton_Click);
            // 
            // voltLabel
            // 
            this.voltLabel.AutoSize = true;
            this.voltLabel.Location = new System.Drawing.Point(159, 172);
            this.voltLabel.Name = "voltLabel";
            this.voltLabel.Size = new System.Drawing.Size(14, 13);
            this.voltLabel.TabIndex = 41;
            this.voltLabel.Text = "V";
            this.voltLabel.Visible = false;
            // 
            // trigLevelNumericDropDown
            // 
            this.trigLevelNumericDropDown.DecimalPlaces = 10;
            this.trigLevelNumericDropDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.trigLevelNumericDropDown.Location = new System.Drawing.Point(58, 170);
            this.trigLevelNumericDropDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.trigLevelNumericDropDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.trigLevelNumericDropDown.Name = "trigLevelNumericDropDown";
            this.trigLevelNumericDropDown.Size = new System.Drawing.Size(90, 20);
            this.trigLevelNumericDropDown.TabIndex = 40;
            this.trigLevelNumericDropDown.Visible = false;
            this.trigLevelNumericDropDown.ValueChanged += new System.EventHandler(this.trigLevelNumericDropDown_ValueChanged);
            // 
            // triggerLevelLabel
            // 
            this.triggerLevelLabel.AutoSize = true;
            this.triggerLevelLabel.Location = new System.Drawing.Point(7, 172);
            this.triggerLevelLabel.Name = "triggerLevelLabel";
            this.triggerLevelLabel.Size = new System.Drawing.Size(45, 13);
            this.triggerLevelLabel.TabIndex = 39;
            this.triggerLevelLabel.Text = "Trig Lvl:";
            this.triggerLevelLabel.Visible = false;
            // 
            // offsetLabel
            // 
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(7, 119);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(38, 13);
            this.offsetLabel.TabIndex = 38;
            this.offsetLabel.Text = "Offset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "V";
            // 
            // voltageOffsetNumericUpDown
            // 
            this.voltageOffsetNumericUpDown.DecimalPlaces = 10;
            this.voltageOffsetNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.voltageOffsetNumericUpDown.Location = new System.Drawing.Point(69, 115);
            this.voltageOffsetNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.voltageOffsetNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.voltageOffsetNumericUpDown.Name = "voltageOffsetNumericUpDown";
            this.voltageOffsetNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.voltageOffsetNumericUpDown.TabIndex = 36;
            this.voltageOffsetNumericUpDown.ValueChanged += new System.EventHandler(this.voltageOffsetNumericUpDown_ValueChanged);
            // 
            // voltsLabel
            // 
            this.voltsLabel.AutoSize = true;
            this.voltsLabel.Location = new System.Drawing.Point(161, 95);
            this.voltsLabel.Name = "voltsLabel";
            this.voltsLabel.Size = new System.Drawing.Size(14, 13);
            this.voltsLabel.TabIndex = 35;
            this.voltsLabel.Text = "V";
            // 
            // voltsPerDivNumericUpDown
            // 
            this.voltsPerDivNumericUpDown.DecimalPlaces = 10;
            this.voltsPerDivNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.voltsPerDivNumericUpDown.Location = new System.Drawing.Point(69, 93);
            this.voltsPerDivNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.voltsPerDivNumericUpDown.Name = "voltsPerDivNumericUpDown";
            this.voltsPerDivNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.voltsPerDivNumericUpDown.TabIndex = 34;
            this.voltsPerDivNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.voltsPerDivNumericUpDown.ValueChanged += new System.EventHandler(this.voltsPerDivNumericUpDown_ValueChanged);
            // 
            // VoltsPerDivLabel
            // 
            this.VoltsPerDivLabel.AutoSize = true;
            this.VoltsPerDivLabel.Location = new System.Drawing.Point(7, 95);
            this.VoltsPerDivLabel.Name = "VoltsPerDivLabel";
            this.VoltsPerDivLabel.Size = new System.Drawing.Size(52, 13);
            this.VoltsPerDivLabel.TabIndex = 33;
            this.VoltsPerDivLabel.Text = "Volts/div:";
            // 
            // sLabel
            // 
            this.sLabel.AutoSize = true;
            this.sLabel.Location = new System.Drawing.Point(161, 74);
            this.sLabel.Name = "sLabel";
            this.sLabel.Size = new System.Drawing.Size(12, 13);
            this.sLabel.TabIndex = 32;
            this.sLabel.Text = "s";
            // 
            // setTimeBaseLabel
            // 
            this.setTimeBaseLabel.AutoSize = true;
            this.setTimeBaseLabel.Location = new System.Drawing.Point(7, 73);
            this.setTimeBaseLabel.Name = "setTimeBaseLabel";
            this.setTimeBaseLabel.Size = new System.Drawing.Size(60, 13);
            this.setTimeBaseLabel.TabIndex = 31;
            this.setTimeBaseLabel.Text = "Time Base:";
            // 
            // scopeSettingsLabel
            // 
            this.scopeSettingsLabel.AutoSize = true;
            this.scopeSettingsLabel.Location = new System.Drawing.Point(7, 55);
            this.scopeSettingsLabel.Name = "scopeSettingsLabel";
            this.scopeSettingsLabel.Size = new System.Drawing.Size(82, 13);
            this.scopeSettingsLabel.TabIndex = 30;
            this.scopeSettingsLabel.Text = "Scope Settings:";
            // 
            // setTimeBaseNumericUpDown
            // 
            this.setTimeBaseNumericUpDown.DecimalPlaces = 10;
            this.setTimeBaseNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.setTimeBaseNumericUpDown.Location = new System.Drawing.Point(69, 71);
            this.setTimeBaseNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.setTimeBaseNumericUpDown.Name = "setTimeBaseNumericUpDown";
            this.setTimeBaseNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.setTimeBaseNumericUpDown.TabIndex = 29;
            this.setTimeBaseNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.setTimeBaseNumericUpDown.ValueChanged += new System.EventHandler(this.setTimeBaseNumericUpDown_ValueChanged);
            // 
            // mmLabel
            // 
            this.mmLabel.AutoSize = true;
            this.mmLabel.Location = new System.Drawing.Point(100, 28);
            this.mmLabel.Name = "mmLabel";
            this.mmLabel.Size = new System.Drawing.Size(23, 13);
            this.mmLabel.TabIndex = 28;
            this.mmLabel.Text = "mm";
            // 
            // motorCommandsLabel
            // 
            this.motorCommandsLabel.AutoSize = true;
            this.motorCommandsLabel.Location = new System.Drawing.Point(6, 9);
            this.motorCommandsLabel.Name = "motorCommandsLabel";
            this.motorCommandsLabel.Size = new System.Drawing.Size(92, 13);
            this.motorCommandsLabel.TabIndex = 27;
            this.motorCommandsLabel.Text = "Motor Commands:";
            // 
            // manuaMoveMotorButton
            // 
            this.manuaMoveMotorButton.Location = new System.Drawing.Point(133, 24);
            this.manuaMoveMotorButton.Name = "manuaMoveMotorButton";
            this.manuaMoveMotorButton.Size = new System.Drawing.Size(75, 23);
            this.manuaMoveMotorButton.TabIndex = 4;
            this.manuaMoveMotorButton.Text = "Move motor";
            this.manuaMoveMotorButton.UseVisualStyleBackColor = true;
            this.manuaMoveMotorButton.Click += new System.EventHandler(this.manuaMoveMotorButton_Click);
            // 
            // moveMotorNumericUpDown
            // 
            this.moveMotorNumericUpDown.DecimalPlaces = 10;
            this.moveMotorNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.moveMotorNumericUpDown.Location = new System.Drawing.Point(7, 25);
            this.moveMotorNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.moveMotorNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.moveMotorNumericUpDown.Name = "moveMotorNumericUpDown";
            this.moveMotorNumericUpDown.Size = new System.Drawing.Size(91, 20);
            this.moveMotorNumericUpDown.TabIndex = 3;
            // 
            // notificationTextBox
            // 
            this.notificationTextBox.Enabled = false;
            this.notificationTextBox.Location = new System.Drawing.Point(352, 791);
            this.notificationTextBox.Name = "notificationTextBox";
            this.notificationTextBox.Size = new System.Drawing.Size(452, 20);
            this.notificationTextBox.TabIndex = 28;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(180, 789);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(166, 23);
            this.progressBar1.TabIndex = 29;
            // 
            // useTimebaseInVoltageAcquisitionCheckBox
            // 
            this.useTimebaseInVoltageAcquisitionCheckBox.AutoSize = true;
            this.useTimebaseInVoltageAcquisitionCheckBox.Location = new System.Drawing.Point(81, 414);
            this.useTimebaseInVoltageAcquisitionCheckBox.Name = "useTimebaseInVoltageAcquisitionCheckBox";
            this.useTimebaseInVoltageAcquisitionCheckBox.Size = new System.Drawing.Size(197, 17);
            this.useTimebaseInVoltageAcquisitionCheckBox.TabIndex = 44;
            this.useTimebaseInVoltageAcquisitionCheckBox.Text = "Use timebase for wait in voltage acq";
            this.useTimebaseInVoltageAcquisitionCheckBox.UseVisualStyleBackColor = true;
            // 
            // useMaxVoltageInScopeTraceRadioButton
            // 
            this.useMaxVoltageInScopeTraceRadioButton.AutoSize = true;
            this.useMaxVoltageInScopeTraceRadioButton.Location = new System.Drawing.Point(13, 253);
            this.useMaxVoltageInScopeTraceRadioButton.Name = "useMaxVoltageInScopeTraceRadioButton";
            this.useMaxVoltageInScopeTraceRadioButton.Size = new System.Drawing.Size(174, 17);
            this.useMaxVoltageInScopeTraceRadioButton.TabIndex = 44;
            this.useMaxVoltageInScopeTraceRadioButton.TabStop = true;
            this.useMaxVoltageInScopeTraceRadioButton.Text = "Use max voltage in scope trace";
            this.useMaxVoltageInScopeTraceRadioButton.UseVisualStyleBackColor = true;
            this.useMaxVoltageInScopeTraceRadioButton.CheckedChanged += new System.EventHandler(this.useMaxVoltageInScopeTraceRadioButton_CheckedChanged);
            // 
            // useMeanVoltageInScopeTraceRadioButton
            // 
            this.useMeanVoltageInScopeTraceRadioButton.AutoSize = true;
            this.useMeanVoltageInScopeTraceRadioButton.Checked = true;
            this.useMeanVoltageInScopeTraceRadioButton.Location = new System.Drawing.Point(13, 276);
            this.useMeanVoltageInScopeTraceRadioButton.Name = "useMeanVoltageInScopeTraceRadioButton";
            this.useMeanVoltageInScopeTraceRadioButton.Size = new System.Drawing.Size(181, 17);
            this.useMeanVoltageInScopeTraceRadioButton.TabIndex = 45;
            this.useMeanVoltageInScopeTraceRadioButton.TabStop = true;
            this.useMeanVoltageInScopeTraceRadioButton.Text = "Use mean voltage in scope trace";
            this.useMeanVoltageInScopeTraceRadioButton.UseVisualStyleBackColor = true;
            this.useMeanVoltageInScopeTraceRadioButton.CheckedChanged += new System.EventHandler(this.useMinVoltageInScopeTraceRadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(945, 815);
            this.Controls.Add(this.useTimebaseInVoltageAcquisitionCheckBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.notificationTextBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MessagesLabel);
            this.Controls.Add(this.ClearMessageBoxButton);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DevicesLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "LED Autocorrelator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.fitParametersGroupBox.ResumeLayout(false);
            this.fitParametersGroupBox.PerformLayout();
            this.scanGroupBox.ResumeLayout(false);
            this.scanGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.averageOverEachMeasurementNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noScanPointsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorSpeedNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeBetweenStepsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRunsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorEndPositionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorStartPositionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trigLevelNumericDropDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltageOffsetNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voltsPerDivNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setTimeBaseNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveMotorNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label DevicesLabel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.RichTextBox MessageBox;
        private System.Windows.Forms.Button ClearMessageBoxButton;
        private System.Windows.Forms.Label MessagesLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label motorLabel;
        private System.Windows.Forms.ComboBox motorUSBComboBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button motorIdentifyButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox scopeComboBox;
        private System.Windows.Forms.Label scopeLabel;
        private System.Windows.Forms.GroupBox scanGroupBox;
        private System.Windows.Forms.Label startScanPositionLabel;
        private System.Windows.Forms.Label numberOfScanPointsLabel;
        private System.Windows.Forms.Label endScanPositionLabel;
        private System.Windows.Forms.NumericUpDown motorEndPositionNumericUpDown;
        private System.Windows.Forms.NumericUpDown motorStartPositionNumericUpDown;
        private System.Windows.Forms.NumericUpDown noScanPointsNumericUpDown;
        private System.Windows.Forms.Button startBasicScanButton;
        private System.Windows.Forms.Label basicScanFileNameLabel;
        private System.Windows.Forms.TextBox basicScanFileNameTextBox;
        private System.Windows.Forms.CheckBox saveScanResultsCheckBox;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.Button enableMotorButton;
        private System.Windows.Forms.Button disableMotorButton;
        private System.Windows.Forms.NumericUpDown numberOfRunsNumericUpDown;
        private System.Windows.Forms.CheckBox runContinuouslyCheckBox;
        private System.Windows.Forms.Label numberOfRunsLabel;
        private System.Windows.Forms.Button stopBasicScanButton;
        private System.ComponentModel.BackgroundWorker BasicScanBackgroundWorker;
        private System.Windows.Forms.Label saveFolderPathLabel;
        private System.Windows.Forms.TextBox saveLocationTextBox;
        private System.Windows.Forms.Button folderSaveDialogButton;
        private System.Windows.Forms.FolderBrowserDialog saveLocationFolderBrowserDialog;
        private System.Windows.Forms.CheckBox iterateFilenamesCheckBox;
        private System.Windows.Forms.GroupBox fitParametersGroupBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton discreteStepsRadioButton;
        private System.Windows.Forms.TextBox notificationTextBox;
        private System.Windows.Forms.Label pulseDurationLabel;
        private System.Windows.Forms.TextBox pulseDurationsFileNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox savePulseDurationCheckBox;
        private System.Windows.Forms.NumericUpDown timeBetweenStepsNumericUpDown;
        private System.Windows.Forms.RadioButton continuousStepsRadioButton;
        private System.Windows.Forms.Label timeBetweenStepsLabel;
        private System.Windows.Forms.NumericUpDown motorSpeedNumericUpDown;
        private System.Windows.Forms.Label motorSpeedLabel;
        private System.Windows.Forms.Button connectToScopeButton;
        private System.Windows.Forms.Button manuaMoveMotorButton;
        private System.Windows.Forms.NumericUpDown moveMotorNumericUpDown;
        private System.Windows.Forms.CheckBox fitGaussianCheckBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label mmLabel;
        private System.Windows.Forms.Label motorCommandsLabel;
        private System.Windows.Forms.Label scopeSettingsLabel;
        private System.Windows.Forms.NumericUpDown setTimeBaseNumericUpDown;
        private System.Windows.Forms.Label sLabel;
        private System.Windows.Forms.Label setTimeBaseLabel;
        private System.Windows.Forms.Label voltsLabel;
        private System.Windows.Forms.NumericUpDown voltsPerDivNumericUpDown;
        private System.Windows.Forms.Label VoltsPerDivLabel;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown voltageOffsetNumericUpDown;
        private System.Windows.Forms.Label triggerLevelLabel;
        private System.Windows.Forms.Label voltLabel;
        private System.Windows.Forms.NumericUpDown trigLevelNumericDropDown;
        private System.Windows.Forms.ComboBox optionsForVoltageExtractionForTraceDropDown;
        private System.Windows.Forms.NumericUpDown averageOverEachMeasurementNumericUpDown;
        private System.Windows.Forms.Label timeStepLabel;
        private System.Windows.Forms.Button getMotorPositionButton;
        private System.Windows.Forms.Label AvgEachMeasurementOverLabel;
        private System.Windows.Forms.CheckBox useTimebaseInVoltageAcquisitionCheckBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label triggeringLabel;
        private System.Windows.Forms.RadioButton useMeanVoltageInScopeTraceRadioButton;
        private System.Windows.Forms.RadioButton useMaxVoltageInScopeTraceRadioButton;
    }
}

