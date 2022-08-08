using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TwitterWebApi.Data;
using TwitterWebApi.Dtos;
using TwitterWebApi.Models;

namespace TwitterWebApi.Services
{
    public class TweetMasterService : ITweetMasterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TweetMasterService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<ServiceResponse<List<TweetMaster>>> AddTweet(TweetMaster tweet, int user)
        {
            var response = new ServiceResponse<List<TweetMaster>>();
            var loginUser = await _context.userRegisters.FirstOrDefaultAsync(x => x.Id == user);
            tweet.user = loginUser;
            _context.tweetMasters.Add(tweet);
            await _context.SaveChangesAsync();
            response.Data = await _context.tweetMasters.ToListAsync();
            return response;

        }

        public async Task<ServiceResponse<TweetMaster>> DeleteTweet(int id)
        {
            var response = new ServiceResponse<TweetMaster>();
            var tweet = await _context.tweetMasters.FirstOrDefaultAsync(x => x.Id == id);
            if(tweet != null)
            {
                _context.tweetMasters.Remove(tweet);
                await _context.SaveChangesAsync();                
                return response;
            }
            response.Sucess = false;
            response.Message = "Tweet not found";
            return response;
            
        }

        public async Task<ServiceResponse<List<TweetMasterDto>>> GetAllTweets()
        {
            var response = new ServiceResponse<List<TweetMasterDto>>();
            var tweet = await _context.tweetMasters.ToListAsync();
            response.Data = tweet.Select(x=>_mapper.Map<TweetMasterDto>(x)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<TweetMasterDto>>> GetAllTweetsByUser(int userId)
        {
            var response = new ServiceResponse<List<TweetMasterDto>>();            
            var tweet = await _context.tweetMasters.Where(x => x.user.Id == userId).ToListAsync();
            response.Data = tweet.Select(x => _mapper.Map<TweetMasterDto>(x)).ToList();
            return response;
        }

        public async Task<ServiceResponse<TweetMasterDto>> UpdateTweet(TweetMasterDto tweet, int tweetId, int userId)
        {
            var response = new ServiceResponse<TweetMasterDto>();
            var loginUser = await _context.userRegisters.FirstOrDefaultAsync(x => x.Id == userId);
            var getTweet = await _context.tweetMasters.FirstOrDefaultAsync(x => x.Id == tweetId && x.user == loginUser);
            if (getTweet == null)
            {
                response.Sucess = false;
                response.Message = "Tweet not found";
                return response;
            }
            getTweet.TweetTitle = tweet.TweetTitle;
            getTweet.TweetBody = tweet.TweetBody;                    
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<TweetMasterDto>(getTweet);
            return response;
        }

       
    }
}
