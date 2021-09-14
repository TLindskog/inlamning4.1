using System.ComponentModel.DataAnnotations;

namespace APILibrary.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}