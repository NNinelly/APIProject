using System.ComponentModel.DataAnnotations;

namespace APIProject.DTO
{
   public class userDTO
   {
      public string UserName { get; set; } = string.Empty;
      public string LastName { get; set; }
      public string PN { get; set; }
      public string Sex { get; set; }
      public DateTime BirthDate { get; set; }
      public string Email { get; set; }
      public string Password { get; set; } = string.Empty;


      public class GetResponse : ResultClass
      {
         public bool Result { get; set; }
      }
   }
}
