namespace myWeather
{
    partial class myWeather
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
            if (simConnect != null)
            {
                this.DisconnectFromSimulator();
            }

            if (thread != null)
            {
                thread.Abort();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(myWeather));
            this.labelStation = new System.Windows.Forms.Label();
            this.textBoxStation = new System.Windows.Forms.TextBox();
            this.buttonGetStation = new System.Windows.Forms.Button();
            this.textBoxMetar = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxTempC = new System.Windows.Forms.TextBox();
            this.textBoxTempF = new System.Windows.Forms.TextBox();
            this.textBoxDewPointC = new System.Windows.Forms.TextBox();
            this.textBoxDewPointF = new System.Windows.Forms.TextBox();
            this.textBoxPressure = new System.Windows.Forms.TextBox();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.textBoxPressureAltitude = new System.Windows.Forms.TextBox();
            this.buttonSetStation = new System.Windows.Forms.Button();
            this.textBoxCloudBaseAGL = new System.Windows.Forms.TextBox();
            this.message = new System.Windows.Forms.Label();
            this.buttonSetWeather = new System.Windows.Forms.Button();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMiles = new System.Windows.Forms.NumericUpDown();
            this.buttonMetars = new System.Windows.Forms.Button();
            this.labelTempC = new System.Windows.Forms.Label();
            this.labelTempF = new System.Windows.Forms.Label();
            this.labelDewPointC = new System.Windows.Forms.Label();
            this.labelDewPointF = new System.Windows.Forms.Label();
            this.labelPressure = new System.Windows.Forms.Label();
            this.labelStationAltitude = new System.Windows.Forms.Label();
            this.labelPressureAltitude = new System.Windows.Forms.Label();
            this.labelCloudBase = new System.Windows.Forms.Label();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelMiles = new System.Windows.Forms.Label();
            this.textBoxDensityAltitude = new System.Windows.Forms.TextBox();
            this.labelDensityAltitude = new System.Windows.Forms.Label();
            this.labelRelativeHumidity = new System.Windows.Forms.Label();
            this.textBoxRelativeHumidity = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMiles)).BeginInit();
            this.SuspendLayout();
            // 
            // labelStation
            // 
            this.labelStation.AutoSize = true;
            this.labelStation.Location = new System.Drawing.Point(9, 14);
            this.labelStation.Name = "labelStation";
            this.labelStation.Size = new System.Drawing.Size(48, 16);
            this.labelStation.TabIndex = 0;
            this.labelStation.Text = "Station";
            // 
            // textBoxStation
            // 
            this.textBoxStation.Location = new System.Drawing.Point(62, 13);
            this.textBoxStation.Name = "textBoxStation";
            this.textBoxStation.Size = new System.Drawing.Size(60, 23);
            this.textBoxStation.TabIndex = 1;
            this.textBoxStation.TextChanged += new System.EventHandler(this.textBoxStation_TextChanged);
            // 
            // buttonGetStation
            // 
            this.buttonGetStation.Location = new System.Drawing.Point(128, 11);
            this.buttonGetStation.Name = "buttonGetStation";
            this.buttonGetStation.Size = new System.Drawing.Size(87, 23);
            this.buttonGetStation.TabIndex = 2;
            this.buttonGetStation.Text = "Get Station";
            this.buttonGetStation.UseVisualStyleBackColor = true;
            this.buttonGetStation.Click += new System.EventHandler(this.buttonGetStation_Click);
            // 
            // textBoxMetar
            // 
            this.textBoxMetar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMetar.Location = new System.Drawing.Point(12, 40);
            this.textBoxMetar.Name = "textBoxMetar";
            this.textBoxMetar.Size = new System.Drawing.Size(544, 23);
            this.textBoxMetar.TabIndex = 7;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(480, 11);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxTempC
            // 
            this.textBoxTempC.Location = new System.Drawing.Point(12, 102);
            this.textBoxTempC.Name = "textBoxTempC";
            this.textBoxTempC.ReadOnly = true;
            this.textBoxTempC.Size = new System.Drawing.Size(73, 23);
            this.textBoxTempC.TabIndex = 0;
            this.textBoxTempC.TabStop = false;
            this.textBoxTempC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTempF
            // 
            this.textBoxTempF.Location = new System.Drawing.Point(95, 101);
            this.textBoxTempF.Name = "textBoxTempF";
            this.textBoxTempF.ReadOnly = true;
            this.textBoxTempF.Size = new System.Drawing.Size(76, 23);
            this.textBoxTempF.TabIndex = 6;
            this.textBoxTempF.TabStop = false;
            this.textBoxTempF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDewPointC
            // 
            this.textBoxDewPointC.Location = new System.Drawing.Point(183, 101);
            this.textBoxDewPointC.Name = "textBoxDewPointC";
            this.textBoxDewPointC.ReadOnly = true;
            this.textBoxDewPointC.Size = new System.Drawing.Size(72, 23);
            this.textBoxDewPointC.TabIndex = 7;
            this.textBoxDewPointC.TabStop = false;
            this.textBoxDewPointC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDewPointF
            // 
            this.textBoxDewPointF.Location = new System.Drawing.Point(276, 101);
            this.textBoxDewPointF.Name = "textBoxDewPointF";
            this.textBoxDewPointF.ReadOnly = true;
            this.textBoxDewPointF.Size = new System.Drawing.Size(72, 23);
            this.textBoxDewPointF.TabIndex = 8;
            this.textBoxDewPointF.TabStop = false;
            this.textBoxDewPointF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPressure
            // 
            this.textBoxPressure.Location = new System.Drawing.Point(95, 163);
            this.textBoxPressure.Name = "textBoxPressure";
            this.textBoxPressure.ReadOnly = true;
            this.textBoxPressure.Size = new System.Drawing.Size(72, 23);
            this.textBoxPressure.TabIndex = 10;
            this.textBoxPressure.TabStop = false;
            this.textBoxPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Location = new System.Drawing.Point(12, 163);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.ReadOnly = true;
            this.textBoxAltitude.Size = new System.Drawing.Size(73, 23);
            this.textBoxAltitude.TabIndex = 11;
            this.textBoxAltitude.TabStop = false;
            this.textBoxAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPressureAltitude
            // 
            this.textBoxPressureAltitude.Location = new System.Drawing.Point(183, 163);
            this.textBoxPressureAltitude.Name = "textBoxPressureAltitude";
            this.textBoxPressureAltitude.ReadOnly = true;
            this.textBoxPressureAltitude.Size = new System.Drawing.Size(76, 23);
            this.textBoxPressureAltitude.TabIndex = 12;
            this.textBoxPressureAltitude.TabStop = false;
            this.textBoxPressureAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSetStation
            // 
            this.buttonSetStation.Location = new System.Drawing.Point(221, 11);
            this.buttonSetStation.Name = "buttonSetStation";
            this.buttonSetStation.Size = new System.Drawing.Size(94, 23);
            this.buttonSetStation.TabIndex = 3;
            this.buttonSetStation.Text = "Set Station";
            this.buttonSetStation.UseVisualStyleBackColor = true;
            this.buttonSetStation.Click += new System.EventHandler(this.buttonSetStation_Click);
            // 
            // textBoxCloudBaseAGL
            // 
            this.textBoxCloudBaseAGL.Location = new System.Drawing.Point(364, 102);
            this.textBoxCloudBaseAGL.Name = "textBoxCloudBaseAGL";
            this.textBoxCloudBaseAGL.ReadOnly = true;
            this.textBoxCloudBaseAGL.Size = new System.Drawing.Size(72, 23);
            this.textBoxCloudBaseAGL.TabIndex = 17;
            this.textBoxCloudBaseAGL.TabStop = false;
            this.textBoxCloudBaseAGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(9, 189);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(69, 16);
            this.message.TabIndex = 0;
            this.message.Text = "Label Here";
            // 
            // buttonSetWeather
            // 
            this.buttonSetWeather.Location = new System.Drawing.Point(318, 11);
            this.buttonSetWeather.Name = "buttonSetWeather";
            this.buttonSetWeather.Size = new System.Drawing.Size(75, 23);
            this.buttonSetWeather.TabIndex = 4;
            this.buttonSetWeather.Text = "Custom";
            this.buttonSetWeather.UseVisualStyleBackColor = true;
            this.buttonSetWeather.Click += new System.EventHandler(this.buttonSetWeather_Click);
            // 
            // numericUpDownMinutes
            // 
            this.numericUpDownMinutes.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMinutes.Location = new System.Drawing.Point(454, 102);
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownMinutes.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericUpDownMinutes.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownMinutes.TabIndex = 8;
            this.numericUpDownMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMinutes.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numericUpDownMiles
            // 
            this.numericUpDownMiles.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMiles.Location = new System.Drawing.Point(511, 102);
            this.numericUpDownMiles.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDownMiles.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMiles.Name = "numericUpDownMiles";
            this.numericUpDownMiles.Size = new System.Drawing.Size(44, 23);
            this.numericUpDownMiles.TabIndex = 9;
            this.numericUpDownMiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMiles.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // buttonMetars
            // 
            this.buttonMetars.Location = new System.Drawing.Point(399, 11);
            this.buttonMetars.Name = "buttonMetars";
            this.buttonMetars.Size = new System.Drawing.Size(75, 23);
            this.buttonMetars.TabIndex = 5;
            this.buttonMetars.Text = "Metars";
            this.buttonMetars.UseVisualStyleBackColor = true;
            this.buttonMetars.Click += new System.EventHandler(this.buttonMetars_Click);
            // 
            // labelTempC
            // 
            this.labelTempC.AutoSize = true;
            this.labelTempC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTempC.Location = new System.Drawing.Point(9, 67);
            this.labelTempC.Name = "labelTempC";
            this.labelTempC.Size = new System.Drawing.Size(83, 32);
            this.labelTempC.TabIndex = 19;
            this.labelTempC.Text = "Temperature\r\nCelcius";
            this.labelTempC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTempF
            // 
            this.labelTempF.AutoSize = true;
            this.labelTempF.Location = new System.Drawing.Point(88, 66);
            this.labelTempF.Name = "labelTempF";
            this.labelTempF.Size = new System.Drawing.Size(83, 32);
            this.labelTempF.TabIndex = 20;
            this.labelTempF.Text = "Temperature\r\nFahrenheit";
            this.labelTempF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDewPointC
            // 
            this.labelDewPointC.AutoSize = true;
            this.labelDewPointC.Location = new System.Drawing.Point(188, 66);
            this.labelDewPointC.Name = "labelDewPointC";
            this.labelDewPointC.Size = new System.Drawing.Size(61, 32);
            this.labelDewPointC.TabIndex = 21;
            this.labelDewPointC.Text = "Dewpoint\r\nCelcius";
            this.labelDewPointC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDewPointF
            // 
            this.labelDewPointF.AutoSize = true;
            this.labelDewPointF.Location = new System.Drawing.Point(273, 66);
            this.labelDewPointF.Name = "labelDewPointF";
            this.labelDewPointF.Size = new System.Drawing.Size(69, 32);
            this.labelDewPointF.TabIndex = 22;
            this.labelDewPointF.Text = "Dewpoint\r\nFahrenheit";
            this.labelDewPointF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPressure
            // 
            this.labelPressure.AutoSize = true;
            this.labelPressure.Location = new System.Drawing.Point(105, 127);
            this.labelPressure.Name = "labelPressure";
            this.labelPressure.Size = new System.Drawing.Size(58, 32);
            this.labelPressure.TabIndex = 24;
            this.labelPressure.Text = "\r\nPressure\r\n";
            this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStationAltitude
            // 
            this.labelStationAltitude.AutoSize = true;
            this.labelStationAltitude.Location = new System.Drawing.Point(27, 128);
            this.labelStationAltitude.Name = "labelStationAltitude";
            this.labelStationAltitude.Size = new System.Drawing.Size(51, 32);
            this.labelStationAltitude.TabIndex = 25;
            this.labelStationAltitude.Text = "Station\r\nAltitude";
            this.labelStationAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPressureAltitude
            // 
            this.labelPressureAltitude.AutoSize = true;
            this.labelPressureAltitude.Location = new System.Drawing.Point(188, 127);
            this.labelPressureAltitude.Name = "labelPressureAltitude";
            this.labelPressureAltitude.Size = new System.Drawing.Size(58, 32);
            this.labelPressureAltitude.TabIndex = 26;
            this.labelPressureAltitude.Text = "Pressure\r\nAltitude";
            this.labelPressureAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCloudBase
            // 
            this.labelCloudBase.AutoSize = true;
            this.labelCloudBase.Location = new System.Drawing.Point(361, 66);
            this.labelCloudBase.Name = "labelCloudBase";
            this.labelCloudBase.Size = new System.Drawing.Size(71, 32);
            this.labelCloudBase.TabIndex = 28;
            this.labelCloudBase.Text = "Cloud Base\r\nAGL";
            this.labelCloudBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(451, 83);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(51, 16);
            this.labelInterval.TabIndex = 29;
            this.labelInterval.Text = "Interval";
            // 
            // labelMiles
            // 
            this.labelMiles.AutoSize = true;
            this.labelMiles.Location = new System.Drawing.Point(508, 82);
            this.labelMiles.Name = "labelMiles";
            this.labelMiles.Size = new System.Drawing.Size(37, 16);
            this.labelMiles.TabIndex = 30;
            this.labelMiles.Text = "Miles";
            this.labelMiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDensityAltitude
            // 
            this.textBoxDensityAltitude.Location = new System.Drawing.Point(276, 163);
            this.textBoxDensityAltitude.Name = "textBoxDensityAltitude";
            this.textBoxDensityAltitude.ReadOnly = true;
            this.textBoxDensityAltitude.Size = new System.Drawing.Size(72, 23);
            this.textBoxDensityAltitude.TabIndex = 31;
            this.textBoxDensityAltitude.TabStop = false;
            this.textBoxDensityAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDensityAltitude
            // 
            this.labelDensityAltitude.AutoSize = true;
            this.labelDensityAltitude.Location = new System.Drawing.Point(282, 128);
            this.labelDensityAltitude.Name = "labelDensityAltitude";
            this.labelDensityAltitude.Size = new System.Drawing.Size(51, 32);
            this.labelDensityAltitude.TabIndex = 32;
            this.labelDensityAltitude.Text = "Density\r\nAltitude";
            this.labelDensityAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRelativeHumidity
            // 
            this.labelRelativeHumidity.AutoSize = true;
            this.labelRelativeHumidity.Location = new System.Drawing.Point(375, 127);
            this.labelRelativeHumidity.Name = "labelRelativeHumidity";
            this.labelRelativeHumidity.Size = new System.Drawing.Size(57, 32);
            this.labelRelativeHumidity.TabIndex = 33;
            this.labelRelativeHumidity.Text = "Relative\r\nHumidity";
            this.labelRelativeHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxRelativeHumidity
            // 
            this.textBoxRelativeHumidity.Location = new System.Drawing.Point(364, 163);
            this.textBoxRelativeHumidity.Name = "textBoxRelativeHumidity";
            this.textBoxRelativeHumidity.ReadOnly = true;
            this.textBoxRelativeHumidity.Size = new System.Drawing.Size(72, 23);
            this.textBoxRelativeHumidity.TabIndex = 34;
            this.textBoxRelativeHumidity.TabStop = false;
            this.textBoxRelativeHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // myWeather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 208);
            this.Controls.Add(this.textBoxRelativeHumidity);
            this.Controls.Add(this.labelRelativeHumidity);
            this.Controls.Add(this.labelDensityAltitude);
            this.Controls.Add(this.textBoxDensityAltitude);
            this.Controls.Add(this.labelMiles);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.labelCloudBase);
            this.Controls.Add(this.labelPressureAltitude);
            this.Controls.Add(this.labelStationAltitude);
            this.Controls.Add(this.labelPressure);
            this.Controls.Add(this.labelDewPointF);
            this.Controls.Add(this.labelDewPointC);
            this.Controls.Add(this.labelTempF);
            this.Controls.Add(this.labelTempC);
            this.Controls.Add(this.buttonMetars);
            this.Controls.Add(this.numericUpDownMiles);
            this.Controls.Add(this.numericUpDownMinutes);
            this.Controls.Add(this.buttonSetWeather);
            this.Controls.Add(this.message);
            this.Controls.Add(this.textBoxCloudBaseAGL);
            this.Controls.Add(this.buttonSetStation);
            this.Controls.Add(this.textBoxPressureAltitude);
            this.Controls.Add(this.textBoxAltitude);
            this.Controls.Add(this.textBoxPressure);
            this.Controls.Add(this.textBoxDewPointF);
            this.Controls.Add(this.textBoxDewPointC);
            this.Controls.Add(this.textBoxTempF);
            this.Controls.Add(this.textBoxTempC);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxMetar);
            this.Controls.Add(this.buttonGetStation);
            this.Controls.Add(this.textBoxStation);
            this.Controls.Add(this.labelStation);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(584, 246);
            this.Name = "myWeather";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "My Weather";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStation;
        private System.Windows.Forms.TextBox textBoxStation;
        private System.Windows.Forms.Button buttonGetStation;
        private System.Windows.Forms.TextBox textBoxMetar;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxTempC;
        private System.Windows.Forms.TextBox textBoxTempF;
        private System.Windows.Forms.TextBox textBoxDewPointC;
        private System.Windows.Forms.TextBox textBoxDewPointF;
        private System.Windows.Forms.TextBox textBoxPressure;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.TextBox textBoxPressureAltitude;
        private System.Windows.Forms.Button buttonSetStation;
        private System.Windows.Forms.TextBox textBoxCloudBaseAGL;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Button buttonSetWeather;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownMiles;
        private System.Windows.Forms.Button buttonMetars;
        private System.Windows.Forms.Label labelTempC;
        private System.Windows.Forms.Label labelTempF;
        private System.Windows.Forms.Label labelDewPointC;
        private System.Windows.Forms.Label labelDewPointF;
        private System.Windows.Forms.Label labelPressure;
        private System.Windows.Forms.Label labelStationAltitude;
        private System.Windows.Forms.Label labelPressureAltitude;
        private System.Windows.Forms.Label labelCloudBase;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelMiles;
        private System.Windows.Forms.TextBox textBoxDensityAltitude;
        private System.Windows.Forms.Label labelDensityAltitude;
        private System.Windows.Forms.Label labelRelativeHumidity;
        private System.Windows.Forms.TextBox textBoxRelativeHumidity;
    }
}

