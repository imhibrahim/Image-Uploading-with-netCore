﻿using System.ComponentModel.DataAnnotations;

namespace Image_crud.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public IFormFile photo { get; set; } = null!;
    }
}
