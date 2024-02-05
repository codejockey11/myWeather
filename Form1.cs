using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using System.Threading;

using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace myWeather
{
    public partial class myWeather : Form
    {
        Int32 requestCount;
        String metar;
        String station;
        String wd;
        String ws;
        String gust;
        String vis;
        String cb;
        String sc;
        String clouds;
        Boolean isMaint;

        WebRequest myRequest;
        WebResponse response;

        List<Station> stations;
        Form3 metarForm;

        List<WindsAloft> winds;

        Thread thread;

        SimConnect simConnect;

        const int WM_USER_SIMCONNECT = 0x0402;

        // this is how you declare a data structure so that simConnect knows how to fill it/read it.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct Struct1
        {
            // this is how you declare a fixed size string 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String title; 
            public Double latitude;
            public Double longitude;
            public Double altitude;
        }

        enum DEFINITIONS
        {
            Struct1
        }

        enum DATA_REQUESTS
        {
            REQUEST_1
        }

        public myWeather()
        {
            InitializeComponent();

            this.message.Text = "Enter ICAO Station";

            this.textBoxStation.Focus();

            //this.GetWindsAloft();

            this.metarForm = new Form3();

            this.stations = new List<Station>();
        }

        private Boolean ConnectToSimulator()
        {
            if (this.simConnect != null)
            {
                return true;
            }

            this.simConnect = null;

            try
            {
                this.simConnect = new SimConnect("Managed Data Request", this.Handle, WM_USER_SIMCONNECT, null, 0);
            }
            catch (SystemException se)
            {
                this.SetForm3Message(se.Message);
                this.SetForm3Message("Simulator Not Running");
                this.SetMessage("Enter ICAO Station");
                return false;
            }

            return true;
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_USER_SIMCONNECT)
            {
                if (this.simConnect != null)
                {
                    this.simConnect.ReceiveMessage();
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        private void InitDataRequest()
        {
            try
            {
                this.simConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Title", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                this.simConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                this.simConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                this.simConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                this.simConnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);

                this.simConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(this.simConnect_OnRecvSimobjectDataBytype);
                this.simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(this.simConnect_OnRecvQuit);
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void simConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            this.DisconnectFromSimulator();
            Application.Exit();
        }

        void simConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            switch ((DATA_REQUESTS)data.dwRequestID)
            {
                case DATA_REQUESTS.REQUEST_1:
                    {
                        if (this.requestCount > 0)
                        {
                            break;
                        }

                        this.requestCount = 1;

                        Struct1 s1 = (Struct1)data.dwData[0];
                        String url = "http://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=1&radialDistance=" +
                            Convert.ToInt32(this.numericUpDownMiles.Value) + ";" +
                                        s1.longitude + "," + s1.latitude;

                        this.DoWebRequest(url);

                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        String xml = reader.ReadToEnd();

                        XmlDocument xdcDocument = new XmlDocument();
                        xdcDocument.LoadXml(xml);
                        XmlElement xmlRoot = xdcDocument.DocumentElement;
                        XmlNodeList xmlNodes = xmlRoot.SelectNodes("data/METAR");

                        Station stat = null;
                        this.stations.Clear();

                        foreach (XmlNode n in xmlNodes)
                        {
                            foreach (XmlNode node in n.ChildNodes)
                            {
                                if (String.Compare(node.Name, "raw_text") == 0)
                                {
                                    stat = new Station();
                                    this.metar = new String(StripMetarRemarks(node.InnerText)).ToUpper();
                                    //this.metar = new String(node.InnerText.ToCharArray()).ToUpper();
                                    stat.metar = this.metar;
                                }
                                if (String.Compare(node.Name, "station_id") == 0)
                                {
                                    stat.station = node.InnerText;

                                    Boolean isFound = false;
                                    foreach (Station s in this.stations)
                                    {
                                        if (s.station == stat.station)
                                        {
                                            isFound = true;
                                        }
                                    }
                                    if (!isFound)
                                    {
                                        /*foreach (WindsAloft wa in winds)
                                        {
                                            if (String.Compare(wa.station, stat.station) == 0)
                                            {
                                                //@@@ A T D S | A T D S | ..... 
                                                stat.metar += " @@@ " + wa.data[0].altitude + " " + wa.data[0].temperature + " " + wa.data[0].direction + " " + wa.data[0].speed + " | ";
                                                stat.metar += wa.data[1].altitude + " " + wa.data[1].temperature + " " + wa.data[1].direction + " " + wa.data[1].speed + " | ";
                                                stat.metar += wa.data[2].altitude + " " + wa.data[2].temperature + " " + wa.data[2].direction + " " + wa.data[2].speed + " | ";
                                                stat.metar += wa.data[3].altitude + " " + wa.data[3].temperature + " " + wa.data[3].direction + " " + wa.data[3].speed + " | ";
                                                stat.metar += wa.data[4].altitude + " " + wa.data[4].temperature + " " + wa.data[4].direction + " " + wa.data[4].speed + " | ";
                                                stat.metar += wa.data[5].altitude + " " + wa.data[5].temperature + " " + wa.data[5].direction + " " + wa.data[5].speed + " | ";
                                                stat.metar += wa.data[6].altitude + " " + wa.data[6].temperature + " " + wa.data[6].direction + " " + wa.data[6].speed + " | ";
                                                stat.metar += wa.data[7].altitude + " " + wa.data[7].temperature + " " + wa.data[7].direction + " " + wa.data[7].speed + " | ";
                                                stat.metar += wa.data[8].altitude + " " + wa.data[8].temperature + " " + wa.data[8].direction + " " + wa.data[8].speed + " | ";
                                            }
                                        }*/

                                        this.simConnect.WeatherSetObservation(0, stat.metar);
                                        this.stations.Add(stat);
                                    }
                                }
                            }
                        }
                        
                        this.stations.Sort();

                        foreach (Station s in this.stations)
                        {
                            this.SetForm3Message(s.metar);
                        }
                        this.SetForm3Message("----------");
                        this.metarForm.Refresh();

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void DisconnectFromSimulator()
        {
            if (this.simConnect != null)
            {
                this.simConnect.Dispose();
                this.simConnect = null;
            }

            if (this.thread != null)
            {
                this.thread.Abort();
                this.thread = null;
            }

            this.SetMessage("Disconnected From Simulator");
        }

        private void GetWindsAloft()
        {
            this.myRequest = WebRequest.Create("http://weather.noaa.gov/pub/data/raw/fb/fbus31.kwno.fd1.us1.txt");

            try
            {
                this.response = this.myRequest.GetResponse();
            }
            catch (System.SystemException se)
            {
                MessageBox.Show(se.ToString());
                return;
            }

            StreamReader sr = new StreamReader(this.response.GetResponseStream(), Encoding.UTF8);

            String srr = sr.ReadLine();
            Boolean isDone = false;
            while (!isDone)
            {
                if (srr.Length >= 2)
                {
                    if ((srr[0] == 'F') && (srr[1] == 'T'))
                    {
                        isDone = true;
                    }
                }
                srr = sr.ReadLine();
            }

            this.winds = new List<WindsAloft>();

            while (!sr.EndOfStream)
            {
                if (String.Compare(srr, " ") > 0)
                {
                    WindsAloft wa = new WindsAloft();
                    wa.data = new WindsAloftData[9];

                    wa.station = "K" + srr[1].ToString() + srr[2].ToString() + srr[3].ToString();

                    for (Int32 d = 0; d < 9; d++)
                    {
                        wa.data[d] = new WindsAloftData();

                        switch (d)
                        {
                            case 0:
                                {
                                    wa.data[d].altitude = "3000";
                                    if (srr[5] > ' ')
                                    {
                                        wa.data[d].direction = srr[5].ToString() + srr[6].ToString() + "0";
                                    }
                                    else
                                    {
                                        wa.data[d].direction = "000";
                                    }
                                    if (String.Compare(wa.data[d].direction, "990") == 0)
                                    {
                                        wa.data[d].direction = "099";
                                    }
                                    if (srr[7] > ' ')
                                    {
                                        wa.data[d].speed = srr[7].ToString() + srr[8].ToString();
                                    }
                                    else
                                    {
                                        wa.data[d].speed = "00";
                                    }

                                    wa.data[0].temperature = "00";

                                    break;
                                }
                            case 1:
                                {
                                    wa.data[d].altitude = "6000";
                                    if (srr[10] > ' ')
                                    {
                                        wa.data[d].direction = srr[10].ToString() + srr[11].ToString() + "0";
                                    }
                                    else
                                    {
                                        wa.data[d].direction = "000";
                                    }
                                    if (String.Compare(wa.data[d].direction, "990") == 0)
                                    {
                                        wa.data[d].direction = "099";
                                    }
                                    if (srr[12] > ' ')
                                    {
                                        wa.data[d].speed = srr[12].ToString() + srr[13].ToString();
                                    }
                                    else
                                    {
                                        wa.data[d].speed = "00";
                                    }
                                    if (srr[14] > ' ')
                                    {
                                        if (srr[14] == '-')
                                        {
                                            wa.data[d].temperature = srr[14].ToString() + srr[15].ToString() + srr[16].ToString();
                                        }
                                        else
                                        {
                                            wa.data[d].temperature = srr[15].ToString() + srr[16].ToString();
                                        }
                                    }
                                    else
                                    {
                                        wa.data[d].temperature = "00";
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    wa.data[d].altitude = "9000";
                                    wa.data[d].direction = srr[18].ToString() + srr[18].ToString() + "0";
                                    if (String.Compare(wa.data[d].direction, "990") == 0)
                                    {
                                        wa.data[d].direction = "099";
                                    }
                                    wa.data[d].speed = srr[20].ToString() + srr[21].ToString();
                                    if (srr[22] > ' ')
                                    {
                                        if (srr[22] == '-')
                                        {
                                            wa.data[d].temperature = srr[22].ToString() + srr[23].ToString() + srr[24].ToString();
                                        }
                                        else
                                        {
                                            wa.data[d].temperature = srr[23].ToString() + srr[24].ToString();
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    wa.data[d].altitude = "12000";
                                    wa.data[d].direction = srr[26].ToString() + srr[27].ToString() + "0";
                                    if (String.Compare(wa.data[d].direction, "990") == 0)
                                    {
                                        wa.data[d].direction = "099";
                                    }
                                    wa.data[d].speed = srr[28].ToString() + srr[29].ToString();
                                    if (srr[30] > ' ')
                                    {
                                        if (srr[30] == '-')
                                        {
                                            wa.data[d].temperature = srr[30].ToString() + srr[31].ToString() + srr[32].ToString();
                                        }
                                        else
                                        {
                                            wa.data[d].temperature = srr[31].ToString() + srr[32].ToString();
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    wa.data[d].altitude = "18000";
                                    wa.data[d].direction = srr[34].ToString() + srr[35].ToString() + "0";
                                    wa.data[d].speed = srr[36].ToString() + srr[37].ToString();
                                    if (srr[38] == '-')
                                    {
                                        wa.data[d].temperature = srr[38].ToString() + srr[39].ToString() + srr[40].ToString();
                                    }
                                    else
                                    {
                                        wa.data[d].temperature = srr[39].ToString() + srr[40].ToString();
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    wa.data[d].altitude = "24000";
                                    wa.data[d].direction = srr[42].ToString() + srr[43].ToString() + "0";
                                    wa.data[d].speed = srr[44].ToString() + srr[45].ToString();
                                    if (srr[46] == '-')
                                    {
                                        wa.data[d].temperature = srr[46].ToString() + srr[47].ToString() + srr[48].ToString();
                                    }
                                    else
                                    {
                                        wa.data[d].temperature = srr[47].ToString() + srr[48].ToString();
                                    }
                                    break;
                                }
                            case 6:
                                {
                                    wa.data[d].altitude = "30000";
                                    wa.data[d].direction = srr[50].ToString() + srr[51].ToString() + "0";
                                    wa.data[d].speed = srr[52].ToString() + srr[53].ToString();
                                    wa.data[d].temperature = '-' + srr[54].ToString() + srr[55].ToString();
                                    break;
                                }
                            case 7:
                                {
                                    wa.data[d].altitude = "34000";
                                    wa.data[d].direction = srr[57].ToString() + srr[58].ToString() + "0";
                                    wa.data[d].speed = srr[59].ToString() + srr[60].ToString();
                                    wa.data[d].temperature = '-' + srr[61].ToString() + srr[62].ToString();
                                    break;
                                }
                            case 8:
                                {
                                    wa.data[d].altitude = "39000";
                                    wa.data[d].direction = srr[64].ToString() + srr[65].ToString() + "0";
                                    wa.data[d].speed = srr[66].ToString() + srr[67].ToString();
                                    wa.data[d].temperature = '-' + srr[68].ToString() + srr[69].ToString();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }

                    this.winds.Add(wa);
                }

                srr = sr.ReadLine();
            }

            sr.Close();
        }

        private void buttonGetStation_Click(object sender, EventArgs e)
        {
            this.textBoxStation.Text = this.textBoxStation.Text.ToUpper();
            this.GetWeatherFromWeb(this.textBoxStation.Text);
        }

        private void GetWeatherFromWeb(String s)
        {
            this.textBoxMetar.Text = "";
            this.textBoxTempC.Text = "";
            this.textBoxTempF.Text = "";
            this.textBoxDewPointC.Text = "";
            this.textBoxDewPointF.Text = "";
            this.textBoxPressure.Text = "";
            this.textBoxAltitude.Text = "";
            this.textBoxPressureAltitude.Text = "";
            this.textBoxCloudBaseAGL.Text = "";
            this.textBoxDensityAltitude.Text = "";

	        if (s == "")
	        {
                this.SetMessage("Enter ICAO Station");
		        return;
	        }

            this.DoWebRequest("http://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=1&stationString=" + s);

            StreamReader reader = new StreamReader(response.GetResponseStream());
            String xml = reader.ReadToEnd();

            XmlDocument xdcDocument = new XmlDocument();
            xdcDocument.LoadXml(xml);
            XmlElement xmlRoot = xdcDocument.DocumentElement;
            XmlNodeList xmlNodes = xmlRoot.SelectNodes("data/METAR");

            bool metarReturned = false;
            this.isMaint = false;
            int metarCount = 0;

            foreach (XmlNode n in xmlNodes)
            {
                if (metarCount > 0)
                {
                    break;
                }
                
                if (String.Compare(n.Name, "METAR") == 0)
                {
                    metarReturned = true;
                }
                
                foreach (XmlNode node in n.ChildNodes)
                {
                    if (String.Compare(node.Name, "raw_text") == 0)
                    {
                        this.metar = node.InnerText;
                        this.textBoxMetar.Text = new String(this.StripMetarRemarks(this.metar)).ToUpper().Trim();
                        //this.textBoxMetar.Text = new String(node.InnerText.ToCharArray()).ToUpper();
                    }
                    if (String.Compare(node.Name, "station_id") == 0)
                    {
                        this.station = node.InnerText;
                    }
                    if (String.Compare(node.Name, "maintenance_indicator_on") == 0)
                    {
                        String ind = node.InnerText;
                        if (String.Compare(ind, "TRUE") == 0)
                        {
                            this.isMaint = true;
                        }
                    }
                    if (String.Compare(node.Name, "temp_c") == 0)
                    {
                        this.textBoxTempC.Text = node.InnerText;
                        this.textBoxTempF.Text = this.CelciusToFahrenheit(Convert.ToDouble(this.textBoxTempC.Text)).ToString("F2");
                    }
                    if (String.Compare(node.Name, "dewpoint_c") == 0)
                    {
                        this.textBoxDewPointC.Text = node.InnerText;
                        this.textBoxDewPointF.Text = this.CelciusToFahrenheit(Convert.ToDouble(this.textBoxDewPointC.Text)).ToString("F2");
                    }
                    if (String.Compare(node.Name, "wind_dir_degrees") == 0)
                    {
                        this.wd = node.InnerText;
                    }
                    if (String.Compare(node.Name, "wind_speed_kt") == 0)
                    {
                        this.ws = node.InnerText;
                    }
                    if (String.Compare(node.Name, "wind_gust_kt") == 0)
                    {
                        this.gust = node.InnerText;
                    }
                    if (String.Compare(node.Name, "visibility_statute_mi") == 0)
                    {
                        vis = node.InnerText;
                    }
                    if (String.Compare(node.Name, "altim_in_hg") == 0)
                    {
                        this.textBoxPressure.Text = Convert.ToDouble(node.InnerText).ToString("F2");
                    }
                    if (String.Compare(node.Name, "sky_condition cloud_base_ft_agl") == 0)
                    {
                        this.cb = node.InnerText;
                    }
                    if (String.Compare(node.Name, "sky_cover") == 0)
                    {
                        this.sc = node.InnerText;
                        this.clouds += sc + cb + " ";
                    }
                    if (String.Compare(node.Name, "elevation_m") == 0)
                    {
                        this.textBoxAltitude.Text = Convert.ToDouble(Convert.ToDouble(node.InnerText) * 3.2808399).ToString("F2");
                        metarCount++;
                    }
                }
            }

            if (metarReturned)
            {
                System.Double alt = 0;
                System.Double press = 0;

                isMaint = metar.Contains("$");

                if (this.textBoxAltitude.Text != "")
                {
                    alt =  Convert.ToDouble(this.textBoxAltitude.Text);
                }

                if (this.textBoxPressure.Text != "")
                {
                    press = Convert.ToDouble(this.textBoxPressure.Text);
                }

                System.Double tc = 0;
                if (this.textBoxTempC.Text != "")
                {
                    tc = Convert.ToDouble(this.textBoxTempC.Text);
                }

                System.Double dc = 0;
                if (this.textBoxDewPointC.Text != "")
                {
                    dc = Convert.ToDouble(this.textBoxDewPointC.Text);
                }

                System.Double tf = 0;
                if (this.textBoxTempF.Text != "")
                {
                    tf = Convert.ToDouble(this.textBoxTempF.Text);
                }

                System.Double df = 0;
                if (this.textBoxDewPointF.Text != "")
                {
                    df = Convert.ToDouble(this.textBoxDewPointF.Text);
                }

                System.Double pa = this.PressureAltitude(press);
                System.Double x = pa + alt;
                this.textBoxPressureAltitude.Text = x.ToString("F2");

                System.Double da = 0;
                if (this.textBoxTempC.Text != "")
                {
                    da = this.DensityAltitude(Convert.ToDouble(this.textBoxTempC.Text), press, Convert.ToDouble(this.textBoxDewPointC.Text));
                }
                this.textBoxDensityAltitude.Text = da.ToString("F2");

                // =100*(EXP((17.625*TD)/(243.04+TD))/EXP((17.625*T)/(243.04+T)))
                System.Double rh = 100 * (Math.Exp((17.625 * dc) / (243.04 + dc)) / Math.Exp((17.625 * tc) / (243.04 + tc)));
                this.textBoxRelativeHumidity.Text = rh.ToString("F2");

                System.Double cbagl = this.CloudBaseAGL(tf, df, 'F');
                this.textBoxCloudBaseAGL.Text = cbagl.ToString("F2");

                System.Double tda = da + alt;

                if (isMaint == true)
                {
                    this.SetMessage("Station " + this.station + " Retrieved (maintenance):" + tda.ToString("F2"));
                }
                else
                {
                    this.SetMessage("Station " + this.station + " Retrieved:" + tda.ToString("F2"));
                }
            }
            else
            {
                this.SetMessage("Station " + s + " Unknown");
            }

            this.textBoxStation.Focus();
        }

        private System.Boolean DoWebRequest(String url)
        {
            this.myRequest = WebRequest.Create(url);

            try
            {
                this.response = this.myRequest.GetResponse();
            }
            catch (System.SystemException se)
            {
                MessageBox.Show(se.ToString());
                return false;
            }

            return true;
        }

        private Char[] StripMetarRemarks(String m)
        {
            Char[] stripped = new Char[128];

            //String s01 = m.Replace("VRB", "147");
            String s02 = m.Replace("COR", " ");
            String s03 = s02.Replace("TCU", " ");
            String s04 = s03.Replace("NCD", " ");
            String s05 = s04.Replace("NSC", " ");
            String s06 = s05.Replace("SKC", " ");
            String s07 = s06.Replace("///", " ");
            String s08 = s07.Replace("//", " ");
            String s09 = s08.Replace("VV", " ");
            String s10 = s09.Replace("CB ", " ");
            String s11 = s10.Replace("9999", " ");
            String s12 = s11.Replace("8000", " ");
            String s13 = s12.Replace("AUTO", " ");
            String s14 = s13.Replace("CAVOK", " ");
            String s15 = s14.Replace("WHT", " ");
            String s16 = s15.Replace("BLU", " ");
            String s17 = s16.Replace("4900", " ");
            String s18 = s17.Replace("9000", " ");
            String s19 = s18.Replace(" / ", " ");
            String s20 = s19.Replace(" KT ", " 00000KT ");
            String s21 = s20.Replace("\0", " ");

            String strippedMetar = s21.Trim();

            int a = 0;
            int i = 0;
            bool done = false;

            while (!done)
            {
                if (i >= strippedMetar.Length)
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'R') && (strippedMetar[i + 1] == 'M') && (strippedMetar[i + 2] == 'K'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'G') && (strippedMetar[i + 1] == 'R') && (strippedMetar[i + 2] == 'N'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'Y') && (strippedMetar[i + 1] == 'L'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'N') && (strippedMetar[i + 1] == 'O') && (strippedMetar[i + 2] == 'S') && (strippedMetar[i + 3] == 'I') && (strippedMetar[i + 4] == 'G'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'N') && (strippedMetar[i + 1] == 'O') && (strippedMetar[i + 2] == 'S') && (strippedMetar[i + 3] == ' ') && (strippedMetar[i + 4] == 'I') && (strippedMetar[i + 5] == 'G'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'N') && (strippedMetar[i + 1] == 'O') && (strippedMetar[i + 2] == ' ') && (strippedMetar[i + 3] == 'S') && (strippedMetar[i + 4] == 'I') && (strippedMetar[i + 5] == 'G'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'T') && (strippedMetar[i + 1] == 'E') && (strippedMetar[i + 2] == 'M'))
                {
                    done = true;
                }
                else if ((strippedMetar[i + 0] == 'R') && (strippedMetar[i + 1] == 'E'))
                {
                    done = true;
                }
                else
                {
                    stripped[a] = strippedMetar[i];
                    a++;
                    i++;
                }
            }

            String s90 = new String(stripped);
            String s91 = s90.Replace("\0", " ");
            String s92 = s91.Replace("   ", " ");
            String s93 = s92.Replace("  ", " ");
            String s94 = s93.Trim();

            stripped = s94.ToCharArray();

            return stripped;
        }

        private void SetWeatherWithAPI(String m)
        {
            this.textBoxMetar.Text = this.textBoxMetar.Text.ToUpper();
            this.simConnect.WeatherSetObservation(0, this.textBoxMetar.Text);
        }

        private void SetMessage(String s)
        {
            this.message.Text = s;
        }

        private void SetForm3Message(String s)
        {
            if (this.metarForm.textBox1.TextLength > (this.metarForm.textBox1.MaxLength - 1000))
            {
                this.metarForm.textBox1.Text = "";
            }

            if (this.metarForm.textBox1.TextLength == 0)
            {
                this.metarForm.textBox1.Text = s;
            }
            else
            {
                this.metarForm.textBox1.Text += Environment.NewLine + s;
            }
        }

        private void textBoxStation_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxStation.Text.Length == 4)
            {
                this.buttonGetStation.Focus();
            }
        }

        private void buttonSetStation_Click(object sender, EventArgs e)
        {
            if (this.ConnectToSimulator())
            {
                this.textBoxMetar.Text = this.textBoxMetar.Text.ToUpper();
                this.simConnect.WeatherSetObservation(0, this.textBoxMetar.Text);
                this.SetMessage("Station " + this.station + " Set");
            }
            else
            {
                this.SetMessage("Simulator Not Running");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (this.ConnectToSimulator())
            {
                this.InitDataRequest();
            }

            if (this.thread != null)
            {
                this.thread.Abort();
                this.thread = null;

                this.textBoxMetar.Text = "";
                this.buttonStart.Text = "Start";

                this.SetMessage("Real Weather Stopped");

                return;
            }

            if (this.simConnect != null)
            {
                if (this.thread != null)
                {
                    this.thread.Abort();
                    this.thread = null;
                }

                this.thread = new Thread(new ThreadStart(this.WorkerLoop));
                this.thread.Start();

                this.buttonStart.Text = "Stop";
                this.SetMessage("Real Weather Started");
            }
            else
            {
                this.SetMessage("Simulator Not Running");
            }
        }

        public void WorkerLoop()
        {
            while (true)
            {
                this.requestCount = 0;
                this.simConnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                Thread.Sleep(1000 * 60 * Convert.ToInt32(this.numericUpDownMinutes.Value));
            }
        }

        private void buttonSetWeather_Click(object sender, EventArgs e)
        {
            if (this.ConnectToSimulator())
            {
                this.InitDataRequest();
            }

            Form2 customWeather = new Form2();
            customWeather.ShowDialog();

            if (customWeather.textBoxStation.Text == "")
            {
                customWeather.Dispose();
                return;
            }

            this.textBoxMetar.Text = customWeather.textBoxStation.Text;
            this.textBoxMetar.Text += " 010100Z ";
            this.textBoxMetar.Text += customWeather.textBoxWindDirection.Text + customWeather.textBoxWindSpeed.Text + " ";
            this.textBoxMetar.Text += customWeather.textBoxVisibility.Text + " ";
            
            if (customWeather.textBoxPrecipitation.Text != "")
            {
                this.textBoxMetar.Text += customWeather.textBoxPrecipitation.Text + " ";
            }
            
            this.textBoxMetar.Text += customWeather.textBoxClouds1.Text + " ";
            
            if (customWeather.textBoxClouds2.Text != "")
            {
                this.textBoxMetar.Text += customWeather.textBoxClouds2.Text + " ";
            }
            
            if (customWeather.textBoxClouds3.Text != "")
            {
                this.textBoxMetar.Text += customWeather.textBoxClouds3.Text + " ";
            }
            
            this.textBoxMetar.Text += customWeather.textBoxTemperature.Text + "/" + customWeather.textBoxDewpoint.Text + " ";
            this.textBoxMetar.Text += customWeather.textBoxBarometer.Text;

            this.textBoxMetar.Text = this.textBoxMetar.Text.ToUpper();

            this.station = customWeather.textBoxStation.Text.ToUpper();

            customWeather.Dispose();

            if (this.simConnect != null)
            {
                this.textBoxMetar.Text = this.textBoxMetar.Text.ToUpper();
                this.simConnect.WeatherSetObservation(0, this.textBoxMetar.Text);
                this.SetMessage("Station " + station + " Set");
            }
            else
            {
                this.SetMessage("Simulator Not Running");
            }
        }

        private void buttonMetars_Click(object sender, EventArgs e)
        {
            this.metarForm.ShowDialog();
        }

        private Double CelciusToFahrenheit(Double c)
        {
            Double f = (c * (9.00 / 5.00)) + 32.00;
            return f;
        }

        private Double FahrenheitToCelcius(Double f)
        {
            Double c = (f - 32.00) * (5.00 / 9.00);
            return c;
        }

        private Double PressureAltitude(Double p)
        {
            Double pa = 145366.45 * (1 - Math.Pow(((33.8639 * p) / 1013.25), 0.190284));
            return pa;
        }

        private Double CloudBaseAGL(Double t, Double d, Char tp)
        {
            // 2.5 for celcius 4.4 for farenheit
            Double cb = 0;
            if (tp == 'C')
            {
                cb = ((t - d) / 2.5) * 1000;
            }
            else
            {
                cb = ((t - d) / 4.4) * 1000;
            }
            return cb;
        }

        private Double DensityAltitude(Double temp, Double INpressure, Double Cdewpoint)
        {
            Double Tv = this.virtualTemperature(temp, INpressure, Cdewpoint);
            Double Da = this.densityAltitude(INpressure, Tv);

            return Da;
        }

        private Double convertCtoK(Double Cels)
        {
	        Double Kel;
	        Kel = Cels + 273.15;
	        return Kel;
        }

        private Double convertCtoF(Double Cels)
        {
	        Double Fahr;
	        Fahr = 1.8 * Cels + 32;
	        return Fahr;
        }

        private Double convertKtoC(Double Kel)
        {
	        Double Cels;
	        Cels = Kel - 273.15;
	        return Cels;
        }

        private Double convertKtoR(Double Kel)
        {
	        Double Fahr, Cels, Rank;
	        Cels = this.convertKtoC(Kel);
            Fahr = this.convertCtoF(Cels);
	        Rank = Fahr + 459.69;
	        return Rank;
        }

        private Double virtualTemperature(Double temp, Double INpressure, Double Cdewpoint)
        {
            Double Kel = this.convertCtoK(temp);
            Double E = 6.11 * Math.Pow(10, (7.5 * Cdewpoint / (237.7 + Cdewpoint)));
            Double mbpressure = 33.8639 * INpressure;
	        return Kel / ( 1 - (E / mbpressure ) * ( 1 - 0.622));
        }

        private Double densityAltitude(Double INpressure, Double Tempv)
        {
            Double Rank = this.convertKtoR(Tempv);
            Double dummy = (17.326 * INpressure) / Rank;
	        return 145366 * (1 - (Math.Pow(dummy, 0.235)));
        }

        private Double roundOff(Double value)
        {
	        value = Math.Round(10*value)/10;
	        return value;
        }

    }

    //@@@ A T D S | A T D S | ..... 
    public class WindsAloftData
    {
        public String altitude { get; set; }
        public String temperature { get; set; }
        public String direction { get; set; }
        public String speed { get; set; }
    }

    public class WindsAloft
    {
        public String station { get; set; }
        public WindsAloftData[] data { get; set; }
    }

    public class Station : IComparable<Station>
    {
        public String station { get; set; }
        public String metar { get; set; }

        public override string ToString()
        {
            return "station: " + station + "   metar: " + metar;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Station objAsPart = obj as Station;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public int SortByNameAscending(string stat1, string stat2)
        {

            return stat1.CompareTo(stat2);
        }

        // Default comparer for Part type. 
        public int CompareTo(Station compareStat)
        {
            // A null value means that this object is greater. 
            if (compareStat == null)
                return 1;

            else
                return this.station.CompareTo(compareStat.station);
        }
        
        public override int GetHashCode()
        {
            return Convert.ToInt32(station);
        }

        public bool Equals(Station other)
        {
            if (other == null) return false;
            return (this.station.Equals(other.station));
        }
    }
}