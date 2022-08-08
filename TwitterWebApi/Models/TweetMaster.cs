using System.ComponentModel.DataAnnotations;

namespace TwitterWebApi.Models
{
    public class TweetMaster
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TweetTitle { get; set; }

        [Required]
        [MaxLength (144)]
        public string TweetBody { get; set; }

        public UserRegister user { get; set; }
    }
}
