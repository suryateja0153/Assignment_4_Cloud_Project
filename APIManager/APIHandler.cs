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

        static string BASE_URL = "https://data.cms.gov/resource/97k6-zzx3.json";
        static string API_KEY = "15wcksihd8ny04c25ehgrwbxtz6afvgcvbhejkk9yfqs2sa07u";

        public List<MedicalData> GetMedDetails()
        {
            string API_PATH = BASE_URL;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(API_PATH);

            string medData = "";

            List<MedicalData> result = null;
            
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    medData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!medData.Equals(""))
                {
                    result = JsonConvert.DeserializeObject<List<MedicalData>>(medData);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
