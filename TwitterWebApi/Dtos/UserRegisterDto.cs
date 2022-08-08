using System.ComponentModel.DataAnnotations;

namespace TwitterWebApi.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "First Name should be 100 Character")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name should be 100 Character")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Login Id should be 50 Character")]
        public string LoginId { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Password should be min 8 Character")]
        [MaxLength(16, ErrorMessage = "Password should be max 16 Character ")]       
        public string Password { get; set; }  
        
        [Required]
        [MinLength(8,ErrorMessage ="Password should be min 8 Character")]
        [MaxLength(16, ErrorMessage = "Password should be max 16 Character ")]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword must match")]
        public string ConfirmPassword { get; set; }        

        [Required]        
        public string ContactNumber { get; set; }
    }
}
