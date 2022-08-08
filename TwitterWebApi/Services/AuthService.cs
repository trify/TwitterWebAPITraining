using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TwitterWebApi.Data;
using TwitterWebApi.Dtos;
using TwitterWebApi.Models;

namespace TwitterWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(DataContext dbContext, IConfiguration _iconfiguration, IMapper mapper)
        {
            _context = dbContext;
            _configuration = _iconfiguration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.userRegisters.FirstOrDefaultAsync(x=>x.LoginId.ToLower() == username.ToLower());
            if (user == null)
            {
                response.Sucess = false;
                response.Message = "LoginId or Password Invalid";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Sucess = false;
                response.Message = "LoginId or Password Invalid";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister user, string password)
        {
                var response = new ServiceResponse<int>();

                if (await UserExist(user.LoginId))
                {
                    response.Sucess = false;
                    response.Message = "User already exist";
                    return response;
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.userRegisters.Add(user);
                await _context.SaveChangesAsync();
                response.Data = user.Id;
                response.Message = "User registered sucessfully";

                return response;

        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.userRegisters.AnyAsync(u => u.LoginId.ToLower() == username.ToLower()))
                return true;
            return false;
        }


        public async Task<ServiceResponse<List<UserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<List<UserDto>>();
            var users = await _context.userRegisters.ToListAsync();
            response.Data = users.Select(x => _mapper.Map<UserDto>(x)).ToList();
            return response;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserRegister user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.LoginId)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor rokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = cred

            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(rokenDescriptor);
            return tokenHandler.WriteToken(securityToken);


        }
    }
}
