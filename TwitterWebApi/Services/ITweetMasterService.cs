using TwitterWebApi.Dtos;
using TwitterWebApi.Models;

namespace TwitterWebApi.Services
{
    public interface ITweetMasterService
    {
        Task<ServiceResponse<List<TweetMasterDto>>> GetAllTweets();

        Task<ServiceResponse<List<TweetMasterDto>>> GetAllTweetsByUser(int userId);

        Task<ServiceResponse<List<TweetMaster>>> AddTweet(TweetMaster tweet, int user);
        Task<ServiceResponse<TweetMasterDto>> UpdateTweet(TweetMasterDto tweet, int tweetId, int userId);
        Task<ServiceResponse<TweetMaster>> DeleteTweet(int id);

    }
}
