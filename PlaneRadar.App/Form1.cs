using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Net;//For downloading JSON from online
using System.IO;
using System.Device.Location;



namespace PlaneRadar
{
    
    public partial class Form1 : Form
    {
        List<Aircraft> planes;
        string json;
        Uri url = new Uri("https://opensky-network.org/api/states/all?lamin=51.285594&lomin=-0.572355&lamax=51.699371&lomax=0.260495");
        PointF loc = new PointF(0f,0f);
        
        
        public Form1()
        {

            loc = GetCurrentLoc();

            InitializeComponent();
            DownloadData();
            json = ReadJson();
            NearestPlane.Text = "";
            planes = new List<Aircraft>();
            NearestPlane.Text = json;
            string time = "";
            (planes, time) = Aircraft.PlanesUpdate(json);

            LastUpdate.Text = TimeReader(time).ToLocalTime().ToString();
        }

        //Downloads data and saves as LatestData.json, returns time as string
        private string DownloadData()
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(url, "LatestData.json");
            }
            catch
            { }

            return File.ReadAllText("LatestData.json").Substring(8, 10);
        }

        private string ReadJson(string filename = "LatestData.json")
        {
            return File.ReadAllText(filename);
        }

        private DateTime TimeReader(string time)
        {
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return startDate.AddSeconds(Convert.ToInt32(time));
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string time = DownloadData();
            LastUpdate.Text = TimeReader(time).ToLocalTime().ToString();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private GeoCoordinateWatcher Watcher = null;
        private PointF GetCurrentLoc()
        {
            Watcher = new GeoCoordinateWatcher();
            Watcher.Start();
                if (Watcher.Position.Location.IsUnknown)
                    return loc;
                else
                    return new PointF((float)Watcher.Position.Location.Longitude,(float)Watcher.Position.Location.Latitude);
            Watcher.Stop();
        }

        private double Pythag(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X,2) + Math.Pow(p1.Y - p2.Y ,2));
        }


    }
}
