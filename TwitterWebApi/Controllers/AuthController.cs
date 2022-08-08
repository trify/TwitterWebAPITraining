using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterWebApi.Dtos;
using TwitterWebApi.Models;
using TwitterWebApi.Services;

namespace TwitterWebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/tweets")]    
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            try
            {
                var response = await _authService.Register(new UserRegister
                {
                    LoginId = user.LoginId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber,
                }, user.ConfirmPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {


                var response = await _authService.Login(user.UserName, user.Password);
                if (!response.Sucess)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("users/all")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var response = await _authService.GetAllUser();
                if (!response.Sucess)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
