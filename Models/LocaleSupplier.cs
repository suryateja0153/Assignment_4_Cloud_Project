using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_4_Cloud_Project.Models
{
    public class Locale
    {
        [Key]
        public int location_id { get; set; }
        public string provider_city { get; set; }
        public string provider_state { get; set; }
        public string hospital_referral_region_description { get; set; }

        //[ForeignKey("Supplier")]
        //public string provider_id { get; set; }
        //public virtual Supplier Supplier { get; set; }
        public List<Supplier> Supplier { get; set; }
    }

    public class Supplier
    {
        [Key]
        //public int id { get; set; }
        public string provider_id { get; set; }
        public string provider_name { get; set; }
        public string provider_street_address { get; set; }
        public int provider_zip_code { get; set; }
        public int total_discharges { get; set; }
        public string drg_definition { get; set; }
        public float average_covered_charges { get; set; }
        public float average_medicare_payments { get; set; }
        public float average_medicare_payments_2 { get; set; }
        public Locale Locale { get; set; }
    }
}
