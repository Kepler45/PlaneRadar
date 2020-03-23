using System;
using System.Collections.Generic;
using System.Drawing;

namespace PlaneRadar
{
    public class Aircraft
    {
        float _lat;
        float _lon;
        PointF coords;
        string _origin;
        string _callsign;
        bool _on_ground;
        string _squawk;
        float _baro_alt;

        public Aircraft(float lat, float lon, string origin, string cs, float alt, bool grounded = true, string squawk = "0000")
        {
            _lat = lat;
            _lon = lon;
            coords = new PointF(lon,lat);
            _origin = origin;
            _callsign = cs;
            _on_ground = grounded;
            _squawk = squawk;
            _baro_alt = alt * 3.2808f;
        }
        public static (List<Aircraft>,string) PlanesUpdate(string json)
        {
                
            //Formatting
            
            string time = json.Substring(8, 10);
            json = json.Remove(0, 29);
            json = json.Remove(json.Length - 2, 2);

            List<string> airplanesrough = new List<string>(json.Split(','));
            List<List<string>> airplanes = new List<List<string>>();
            for (int i = 0; i < airplanesrough.Count; i++)
            {

                if (i % 17 == 0)
                    airplanes.Add(new List<string>());
                //We don't need to add anything into the nest because [0] is not needed and makes the maths easier
                else
                {
                    if(i % 17 == 16)
                        airplanes[airplanes.Count - 1].Add(airplanesrough[i][0].ToString());

                    else
                        airplanes[airplanes.Count - 1].Add(airplanesrough[i]);
                }
            }
            
            

            return (ListToPlane(airplanes), time);
        }

        public static List<Aircraft> ListToPlane(List<List<string>> airplanes)
        {
            List<Aircraft> LA = new List<Aircraft>(airplanes.Count);
            foreach (List<string> plane in airplanes)
            {
                string CS = plane[0].Trim();
                string origin = plane[1];
                float lat = float.Parse(plane[5]);
                float lon = float.Parse(plane[4]);

                plane[6] = plane[6] == "null" ? "0" : plane[6];

                float alt = float.Parse(plane[6]);
                

                bool onGnd = bool.Parse(plane[7]);
                string sqk = plane[13].Remove(0, 1);
                sqk.Remove(sqk.Length-3, 2);





                Aircraft newplane = new Aircraft(lat , lon , origin, CS, alt, onGnd, sqk);
                LA.Add(newplane);
            }

            return LA;
        }

        

    }   

}
