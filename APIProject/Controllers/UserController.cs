using APIProject.ApplicationDbContext;
using APIProject.DTO;
using APIProject.Interfaces;
using APIProject.Iservices;
using APIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UserController : ControllerBase
   {
      private readonly APIDbContext _dbContext;
      private readonly IUser _user;
      public UserController(APIDbContext dbContext, IUser user)
      {
         _dbContext = dbContext;
         _user = user;
      }

      [HttpPost("Registration")]
      public async Task<GetResponse> Registration(userDTO request)
      {
         try
         {
            var res = await _user.Registration(request);
            return new GetResponse { StatusCode = 200, Message = "You have successfully registered", Result = res };
         }
         catch (Exception ex)
         {
            return new GetResponse { Message = ex.Message, StatusCode = 400 };
         }
      }

      [HttpPost("Login")]
      public IActionResult Login(string userName, string password)
      {
            var res = _user.Login(userName, password);
            return Ok(res);
      }
   }
}
