using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DapperAdvanced.Models
{
    public class Bike
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Price of Bike")]
        public int Price { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Company Name")]
        public string Company { get; set; }
        
        }
    }
