using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterWebApi.Dtos;
using TwitterWebApi.Models;
using TwitterWebApi.Services;

namespace TwitterWebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/tweets")]
    [ApiController]
    [Authorize]
    public class TweetMasterController : ControllerBase
    {
        private readonly ITweetMasterService _tweetMasterService;

        public TweetMasterController(ITweetMasterService tweetMasterService)
        {
            _tweetMasterService = tweetMasterService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTweet(TweetMasterDto tweet)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null && !string.IsNullOrWhiteSpace(userId))
                {
                    var response = await _tweetMasterService.AddTweet( new TweetMaster { TweetTitle = tweet.TweetTitle, TweetBody=tweet.TweetBody} , int.Parse(userId));
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTweet()
        {
            try
            {
                var response = await _tweetMasterService.GetAllTweets();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("getbyusername")]
        public async Task<IActionResult> GetTweetByUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null && !string.IsNullOrWhiteSpace(userId))
                {
                    var response = await _tweetMasterService.GetAllTweetsByUser(int.Parse(userId));
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTweet(TweetMasterDto tweet, int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null && !string.IsNullOrWhiteSpace(userId))
                {
                    var response = await _tweetMasterService.UpdateTweet(tweet, id, int.Parse(userId));
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null && !string.IsNullOrWhiteSpace(userId))
                {
                    var response = await _tweetMasterService.DeleteTweet(id);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
