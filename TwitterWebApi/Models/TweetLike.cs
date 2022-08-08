namespace TwitterWebApi.Models
{
    public class TweetLike
    {
        public int Id { get; set; }
        public TweetMaster tweet { get; set; }
        public UserRegister user { get; set; }
    }
}
