using System.ComponentModel.DataAnnotations;

namespace TwitterWebApi.Dtos
{
    public class TweetMasterDto
    {
        [Required]
        [MaxLength(50)]
        public string TweetTitle { get; set; }

        [Required]
        [MaxLength(144)]
        public string TweetBody { get; set; }
    }
}
