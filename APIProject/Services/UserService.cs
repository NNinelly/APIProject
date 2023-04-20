using APIProject.ApplicationDbContext;
using APIProject.DTO;
using APIProject.Iservices;
using APIProject.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace APIProject.Services
{
   public class UserService : IUser
   {
      private readonly APIDbContext ent = new APIDbContext();
      private readonly IConfiguration _configuration;
      private readonly IValidator<User> _validator;
      public UserService(APIDbContext entity, IConfiguration configuration, IValidator<User> validator)
      {
         ent = entity;
         _configuration = configuration;
         this._validator = validator;
      }
      public async Task<bool> Registration(userDTO request)
      {
         var users = new User();
         if (ent.users.Any(i => i.Email == request.Email || i.PN == request.PN))
         {
            throw new Exception("This User Already Exist");
         }
         if(request.Password.IsNullOrEmpty())
         {
            throw new Exception("Password fiels is required");
         }
         CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
         users.UserName = request.UserName;
         users.LastName = request.LastName;
         users.PN = request.PN;
         users.BirthDate = request.BirthDate;
         users.Sex = request.Sex;
         users.Email = request.Email;
         users.PasswordHash = passwordHash;
         users.PasswordSalt = passwordSalt;

         await _validator.ValidateAndThrowAsync(users);       
         await ent.users.AddAsync(users);
         await ent.SaveChangesAsync();
         return true;
      }

      public string Login(string userName, string password)
      {
         if (ent.users.Any(i=> i.UserName != userName))
         {
            throw new Exception ("The User does not exist");
         }
         var user = ent.users.Where(x => x.UserName == userName).FirstOrDefault();
         var passwordhash = user.PasswordHash;

         var passwordSalt = user.PasswordSalt;
         if (!VerifyPasswordHash(password, passwordhash, passwordSalt))
         {
            throw new Exception("Wrong Password");
         }

         string token = CreateToken(user);
         return token;
      }

      private string CreateToken(User user)
      {
         List<Claim> claims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, user.UserName)
         };

         var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );

         var jwt = new JwtSecurityTokenHandler().WriteToken(token);

         return jwt;
      }
      private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
      {
         using (var hmac = new HMACSHA512())
         {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
         }
      }

      private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
      {
         using (var hmac = new HMACSHA512(passwordSalt))
         {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
         }
      }
   }
}
