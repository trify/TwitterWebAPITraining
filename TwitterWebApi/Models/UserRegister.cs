using System.ComponentModel.DataAnnotations;

namespace TwitterWebApi.Models
{
    public class UserRegister
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="First Name should be 100 Character")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name should be 100 Character")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="Login Id should be 50 Character")]        
        public string LoginId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        [Range(1,10,ErrorMessage = "Conatct Number should be 10 Digit")]
        public string ContactNumber { get; set; }
    }
}
