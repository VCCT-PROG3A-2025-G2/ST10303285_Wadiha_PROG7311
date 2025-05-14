namespace FarmersConnectWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
