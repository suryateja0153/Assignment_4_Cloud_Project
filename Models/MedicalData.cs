using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment_4_Cloud_Project.Controllers;

namespace Assignment_4_Cloud_Project.Models
{
    public class MedicalData
    {
        [Key]
        public string provider_id { get; set; }
        public string drg_definition { get; set; }
        public string provider_name { get; set; }
        public string provider_street_address { get; set; }
        public string provider_city { get; set; }
        public string provider_state { get; set; }
        public int provider_zip_code { get; set; }
        public string hospital_referral_region_description { get; set; }
        public int total_discharges { get; set; }
        public float average_covered_charges { get; set; }
        public float average_medicare_payments { get; set; }
        public float average_medicare_payments_2 { get; set; }
    }
}
