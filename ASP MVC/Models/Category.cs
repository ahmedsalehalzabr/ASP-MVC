﻿using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}
