using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment_4_Cloud_Project.Models
{
    public class UpdateRecord
    {
        [Key]
        public int FdcId { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public string NutrientName { get; set; }
        public int ProteinValue { get; set; }
        public int FatValue { get; set; }
        public int CarbohydrateValue { get; set; }
    }
}
