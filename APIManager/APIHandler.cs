using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_4_Cloud_Project.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Assignment_4_Cloud_Project.APIManager
{
    public class APIHandler
    {
        HttpClient httpClient;

        //static string BASE_URL = "https://data.cms.gov/resource/97k6-zzx3.json";
        //static string API_KEY = "ccaaGn94vH85g31bEwmX61RgRgmNLztKoV84Xayd";

        static string BASE_URL = "https://api.nal.usda.gov/fdc/v1/";
        static string API_KEY = "LyYNnCXC8gYruaksP3U6LKvFbmT7KmYAgwOIJUXB";

        public MedicalData GetMedDetails()
        {
            string API_PATH = BASE_URL + "foods/list";

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(API_PATH);

            string medData = "";
            //string finalJson = "";

            MedicalData result = null;

            //httpClient.BaseAddress = new Uri(API_PATH);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    medData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                //string pattern = @":\";
                //medData = Regex.Replace(medData, pattern, string.Empty);

                //string food1 = JsonConvert.DeserializeObject<string>(foodData);
                //medData = Regex.Replace(medData, @"\\", "");
                //string food1 = Regex.Unescape(medData);
                //medData = medData.Replace(@"\\", @"""");

                if (!medData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    //finalJson = "{\"data\":" + medData + "}";
                    //result = JsonConvert.DeserializeObject<MedicalData>(medData);

                    JObject parsedResponse = JObject.Parse(medData);
                    JArray parks = (JArray)parsedResponse["data"];

                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return result;
        }
    }
}