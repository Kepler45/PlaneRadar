using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;//For downloading JSON from online
using System.IO;
using PlaneRadar;



namespace PlaneRadar
{
    public partial class Form1 : Form
    {
        List<Aircraft> planes;
        string json;
        Uri url = new Uri("https://opensky-network.org/api/states/all?lamin=51.285594&lomin=-0.572355&lamax=51.699371&lomax=0.260495");

        public Form1()
        {
            
            

            InitializeComponent();
            DownloadData();
            json = ReadJson();
            NearestPlane.Text = "";
            planes = new List<Aircraft>();
            NearestPlane.Text = json;
            (planes, LastUpdate.Text) = Aircraft.PlanesUpdate(json);
        }

        private string DownloadData()
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(url, "LatestData.json");

            return File.ReadAllText("LatestData.json").Substring(8, 10);
        }

        private string ReadJson(string filename = "LatestData.json")
        {
            return File.ReadAllText(filename);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string time = DownloadData();
            LastUpdate.Text = time;
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
