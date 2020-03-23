using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlaneRadar
{
    public class Aircraft
    {
        float _lat;
        float _lon;
        string _icao;
        string _callsign;
        bool _on_ground;
        string _squawk;
        float _baro_alt;

        public Aircraft(float lat, float lon, string icao, string cs, float alt, bool grounded = true, string squawk = "0000")
        {
            _lat = lat;
            _lon = lon;
            _icao = icao;
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
                else
                    airplanes[airplanes.Count - 1].Add(airplanesrough[i]);
            }
                        

            return (new List<Aircraft>(), time);
        }

        public static void Test()
        {
            Console.WriteLine("Test.");
        }


    }   

}
