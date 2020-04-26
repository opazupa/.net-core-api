using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Configuration;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static FeatureLibrary.Extensions.ClaimExtensions;
using static BCrypt.Net.BCrypt;

namespace FeatureLibrary.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTConfiguration _jwtConfiguration;

        public UserService(IUserRepository userRepository, IOptions<JWTConfiguration> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtConfiguration = jwtOptions.Value;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> Authenticate(Authentication auth)
        {
            var user = await _userRepository.GetByUserName(auth.Username);

            if (user == null || !Verify(auth.Password, user.Password))
            {
                throw new UnAuthorizedException($"Username {auth.Username} or password not found or incorrect.");
            }

            return GenerateToken(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<UserEntity> CreateUser(Authentication auth)
        {
            var user = new UserEntity {
                UserName = auth.Username,
                Password = HashPassword(auth.Password)
            };

            user = await _userRepository.Add(user);
            return user;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserEntity> GetById(long userId)
        {
            var user =  await _userRepository.GetById(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} not found.");
            }
            return user;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        /// <summary>
        /// Generate JWT token for user
        /// </summary>
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
                UserName = user.UserName,
                TokenType = TokenType.Bearer,
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = token.ValidTo
            };
        }
    }
}
