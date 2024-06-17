using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.Models.User
{
	public class User : IdentityUser
	{
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
