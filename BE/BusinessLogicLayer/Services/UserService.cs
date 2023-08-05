using AutoMapper;
using BusinessLogicLayer.Contracts;
using Common.Models.Database;
using Common.Models.Enumerations;
using Common.Models.Inbound;
using Common.Models.Outbound;
using DatabaseLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.Constants;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserModel> CreateUser(RegisterUser incomingUser)
        {
            incomingUser.UserType = string.Concat(incomingUser.UserType[0].ToString().ToUpper(), incomingUser.UserType.Substring(1));
            var user = _mapper.Map<UserModel>(incomingUser);
            user.Id = Guid.NewGuid();
            user.Password = BCrypt.Net.BCrypt.HashPassword(incomingUser.Password);
            user.ProfilePicture = await ParseProfilePictureToBytes(incomingUser.ProfilePicture);

            var existingId = await _unitOfWork.Users.GetById(user.Id);
            var existingUsername = await _unitOfWork.Users.FindAsync(u => u.Username.Equals(user.Username));
            if (existingId == null && existingUsername == null)
            {
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.SaveChanges();
                return user;
            }
            else
            {
                throw new Exception("User with this username(or Id) already exists.");
            }
        }

        public async Task<LogedInUser> Login(string username, string password)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Username.Equals(username));
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));
                claims.Add(new Claim("UserId", user.Id.ToString()));
                var token = GetNewJwtToken(claims, user.Id.ToString());
                var logedInUser = _mapper.Map<LogedInUser>(user);
                logedInUser.ProfilePicture = await ParseProfilePictureToString(user.ProfilePicture);
                logedInUser.Token = token;
                return logedInUser;
            }
            throw new Exception("There is no user with this username and password.");
        }

        public async Task<LogedInUser> FacebookLogin(FacebookLoginUser facebookUser)
        {
            var userId = await GetUserIdForFacebookUser(facebookUser.Id);

            var guid = new Guid(userId);

            var existingUser = await _unitOfWork.Users.FindAsync(u => u.Id.Equals(new Guid(userId)));
            if (existingUser != null)
            {
                var userr = await GetExistingFacebookUser(existingUser); // DELETE AFTER CHECK 
                return await GetExistingFacebookUser(existingUser);
            }
            else
            {
                var userr = await RegisterNewFacebookUser(facebookUser);
                return userr;
            }
        }

        private async Task<LogedInUser> GetExistingFacebookUser(UserModel existingUser)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, existingUser.UserType.ToString()));
            claims.Add(new Claim("UserId", existingUser.Id.ToString()));
            var token = GetNewJwtToken(claims, existingUser.Id.ToString());
            var logedInUser = _mapper.Map<LogedInUser>(existingUser);
            logedInUser.ProfilePicture = existingUser.ProfilePicture == null ? null : Encoding.ASCII.GetString(existingUser.ProfilePicture);
            logedInUser.Token = token;
            return logedInUser;
        }

        private async Task<LogedInUser> RegisterNewFacebookUser(FacebookLoginUser facebookLoginUser)
        {
            var username = await GetUsernameForFacebookUser(facebookLoginUser.Fullname);

            while ((await _unitOfWork.Users.FindAsync(u => u.Username.Equals(username))) != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(username);
                sb.Append('1');
                username = sb.ToString();
            }

            var userId = await GetUserIdForFacebookUser(facebookLoginUser.Id);
            var randomPassword = BCrypt.Net.BCrypt.HashPassword(RandomString(12));
            var name = facebookLoginUser.Fullname.Split(' ')[0];
            var lastname = string.IsNullOrEmpty(facebookLoginUser.Fullname.Split(' ')[1]) ? "unknown last name" : facebookLoginUser.Fullname.Split(' ')[1];
            var dateOfBirth = new DateTime();
            var adress = "unknown adress";
            var userType = "Buyer";
            var profilePicture = facebookLoginUser.PictureUrl == null ? null : Encoding.ASCII.GetBytes(facebookLoginUser.PictureUrl);

            var user = new UserModel()
            {
                Id = new Guid(userId),
                Username = username,
                Email = facebookLoginUser.Email,
                Password = randomPassword,
                Name = name,
                LastName = lastname,
                DateOfBirth = dateOfBirth,
                Adress = adress,
                UserType = (UserType)Enum.Parse(typeof(UserType), userType),
                ProfilePicture = profilePicture,
                Verified = 0
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));
            claims.Add(new Claim("UserId", user.Id.ToString()));
            var token = GetNewJwtToken(claims, user.Id.ToString());
            var logedInUser = _mapper.Map<LogedInUser>(user);
            logedInUser.ProfilePicture = user.ProfilePicture == null ? null : Encoding.ASCII.GetString(user.ProfilePicture);
            logedInUser.Token = token;
            return logedInUser;
        }
        private async Task<string> GetUsernameForFacebookUser(string fullname)
        {
            string[] names = fullname.Split(' ');
            StringBuilder usernameSB = new StringBuilder();
            usernameSB.Append(names[0]);
            if (!string.IsNullOrEmpty(names[1]))
            {
                usernameSB.Append(names[1][0]);
            }
            return usernameSB.ToString();
        }

        private static Random random = new Random();

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private async Task<byte[]> ParseProfilePictureToBytes(IFormFile incomingPicture)
        {
            byte[] bytePicture;
            if (incomingPicture != null && incomingPicture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await incomingPicture.CopyToAsync(memoryStream);
                    bytePicture = memoryStream.ToArray();
                }
            }
            else
                bytePicture = new byte[0];
            return bytePicture;
        }

        private string GetNewJwtToken(List<Claim> userClaims, string userId)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY_VALUE));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:7264",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials);
            string stringToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return stringToken;
        }
        private async Task<string> ParseProfilePictureToString(byte[] picture)
        {
            if (picture == null)
                return null;
            return $"data:image/jpg;base64,{Convert.ToBase64String(picture)}";
        }
        private Task<string> GetUserIdForFacebookUser(string facebookId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(facebookId.ToUpper());
            for (int i = facebookId.Length; i < 32; i++)
            {
                sb.Append('0');
            }
            sb.Insert(8, '-');
            sb.Insert(13, '-');
            sb.Insert(18, '-');
            sb.Insert(23, '-');
            return Task.FromResult(sb.ToString());
        }

    }
}
