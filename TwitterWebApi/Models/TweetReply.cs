using System.ComponentModel.DataAnnotations;

namespace TwitterWebApi.Models
{
    public class TweetReply
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(144)]
        public string replyTweet { get; set; }
        public TweetMaster tweet { get; set; }

        public UserRegister user { get; set; }
    }
}
