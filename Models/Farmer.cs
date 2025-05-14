namespace FarmersConnectWebApp.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class Farmer
    {
        [Key]
        public int Id { get; set; }

        [BindNever]
        public string UserId { get; set; }

        [Required]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Product> Products { get; set; }

        [ForeignKey("Employee")]

        [BindNever]
        public int EmployeeId {  get; set; }
        public Employee Employee { get; set; }
    }
}
