using APIProject.DTO;
using APIProject.Models;

namespace APIProject.Iservices
{
   public interface IUser
   {
     Task <bool> Registration(userDTO request);
     string Login(string userName, string password);
   }
}
