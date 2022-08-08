using Microsoft.EntityFrameworkCore;
using TwitterWebApi.Models;

namespace TwitterWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserRegister> userRegisters { get; set; }
        public DbSet<TweetMaster> tweetMasters { get; set; }
        public DbSet<TweetReply> tweetReplies { get; set; }
        public DbSet<TweetLike> tweetLikes { get; set; }
    }
}
