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
using PlaneRadar;



namespace PlaneRadar
{
    public partial class Form1 : Form
    {
        List<Aircraft> planes;
        string json;
        
        public Form1()
        {
            string url = "https://opensky-network.org/api/states/all?lamin=51.285594&lomin=-0.572355&lamax=51.699371&lomax=0.260495";
            json = new WebClient().DownloadString(url).ToString();

            InitializeComponent();
            NearestPlane.Text = "";
            planes = new List<Aircraft>();
            NearestPlane.Text = json;
            Aircraft.PlanesInit(planes, json);
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
