namespace FarmersConnectWebApp.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string UserId {  get; set; }

        public string FullName { get; set; }

        public ICollection<Farmer> Farmers { get; set; }
    }
}
