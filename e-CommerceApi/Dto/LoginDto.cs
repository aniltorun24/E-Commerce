using System.ComponentModel.DataAnnotations;

namespace e_CommerceApi.Dto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
