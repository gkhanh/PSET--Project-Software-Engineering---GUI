using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using RestSharp;
using System.Linq;

namespace GUI.Models
{
    public class Receive_connection : GetData
    {

        public int get_temp_db() { return Temperature; }
        public int get_hum_db() { return Humidity; }
        public int get_lig_db() { return LightInfo; }
        public int get_pres_db() { return AirPressure; }
        //public string get_devInfo_db() { return DeviceInfo; }

        public List<GetTemperature> temperatures = new List<GetTemperature>();
        public void RetrieveData() {
            var client = new RestClient("https://setdatabasetest1-fa15.restdb.io/rest/temperature");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "c1bef8305fa02913fe903d7aed4032259bb28");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.ToString());
            temperatures = JsonSerializer.Deserialize<List<GetTemperature>>(response.Content);

            foreach (var temp in temperatures)
            {
                Console.WriteLine(temp.ToString());
            }
        }



    }

 
  
}
