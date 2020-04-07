using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Configuration;
using CoreLibrary.Exceptions;
using static FeatureLibrary.Extensions.ClaimExtensions;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FeatureLibrary.Models.Entities;

namespace FeatureLibrary.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTConfiguration _jwtConfiguration;

        public UserService(IUserRepository userRepository, IOptions<JWTConfiguration> options)
        {
            _userRepository = userRepository;
            _jwtConfiguration = options.Value;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> Authenticate(Authentication auth)
        {
            var user = await _userRepository.Verify(auth);

            if (user == null)
            {
                throw new BadRequestException($"Username {auth.Username} or password incorrect.");
            }

            return GenerateToken(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<long> CreateUser(Authentication auth)
        {
            var user = new UserEntity {
                UserName = auth.Username,
                Password = auth.Password
            };

            user = await _userRepository.Add(user);
            return user.Id;
        }

        private AuthenticationResult GenerateToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(USER_ID, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult()
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
