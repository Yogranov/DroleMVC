using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drole.Models {
    public class Country {
        
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string EnglishName { get; set; }

        [Required]
        public string Laws { get; set; }
        
        [Required]
        public string User { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        [Required]
        public string BackgroundImageUrl { get; set; }
    }
}