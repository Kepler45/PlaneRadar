using System;
using System.Collections.Generic;

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
        public static List<Aircraft> PlanesInit(List<Aircraft> planes, string json)
        {

            return new List<Aircraft>();
        }

        public static void Test()
        {
            Console.WriteLine("Test.");
        }


    }   

}
