using System.ComponentModel.DataAnnotations;

namespace FarmersConnectWebApp.Models
{
    public class CreateFarmerViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
