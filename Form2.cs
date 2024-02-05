using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace myWeather
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (this.textBoxStation.Text == "zero")
            {
                this.textBoxStation.Text = "####";
                this.textBoxTemperature.Text = "10";
                this.textBoxDewpoint.Text = "08";
                this.textBoxWindDirection.Text = "280";
                this.textBoxWindSpeed.Text = "05KT";
                this.textBoxVisibility.Text = "1/8SM";
                this.textBoxClouds1.Text = "OVC010";
                this.textBoxClouds2.Text = "BKN040";
                this.textBoxClouds3.Text = "FEW100";
                this.textBoxBarometer.Text = "A2989";
                this.textBoxPrecipitation.Text = "+RA";
            }

            if (this.textBoxStation.Text == "good")
            {
                this.textBoxStation.Text = "####";
                this.textBoxTemperature.Text = "15";
                this.textBoxDewpoint.Text = "08";
                this.textBoxWindDirection.Text = "280";
                this.textBoxWindSpeed.Text = "05KT";
                this.textBoxVisibility.Text = "10SM";
                this.textBoxClouds1.Text = "FEW100";
                this.textBoxClouds2.Text = "";
                this.textBoxClouds3.Text = "";
                this.textBoxBarometer.Text = "A3012";
                this.textBoxPrecipitation.Text = "";
            }

            if (this.textBoxStation.Text == "ts")
            {
                this.textBoxStation.Text = "####";
                this.textBoxTemperature.Text = "15";
                this.textBoxDewpoint.Text = "08";
                this.textBoxWindDirection.Text = "280";
                this.textBoxWindSpeed.Text = "05KT";
                this.textBoxVisibility.Text = "2SM";
                this.textBoxClouds1.Text = "OVC020";
                this.textBoxClouds2.Text = "BKN080";
                this.textBoxClouds3.Text = "FEW100";
                this.textBoxBarometer.Text = "A3012";
                this.textBoxPrecipitation.Text = "+TSRA";
            }

            if (this.textBoxStation.Text == "snow")
            {
                this.textBoxStation.Text = "####";
                this.textBoxTemperature.Text = "00";
                this.textBoxDewpoint.Text = "M08";
                this.textBoxWindDirection.Text = "280";
                this.textBoxWindSpeed.Text = "05KT";
                this.textBoxVisibility.Text = "1SM";
                this.textBoxClouds1.Text = "OVC020";
                this.textBoxClouds2.Text = "BKN080";
                this.textBoxClouds3.Text = "FEW100";
                this.textBoxBarometer.Text = "A3012";
                this.textBoxPrecipitation.Text = "+SN";
            }

            if (this.textBoxStation.Text == "####")
            {
                return;
            }

            this.Close();
        }
    }
}
