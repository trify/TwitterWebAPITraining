using TwitterWebApi.Dtos;
using TwitterWebApi.Models;

namespace TwitterWebApi.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister user, string password);

        Task<ServiceResponse<string>> Login(string username, string password);

        Task<bool> UserExist(string username);

        Task<ServiceResponse<List<UserDto>>> GetAllUser();
    }
}
