using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIProject.Models
{
   public class User
   {
      public int Id { get; set; }
      public string UserName { get; set; } = string.Empty;
      public string LastName { get; set; }
      public string PN { get; set; }
      public string Sex { get; set; }
      public DateTime BirthDate { get; set; }
      public string Email { get; set; }
      public byte[] PasswordHash { get; set; }
      public byte[] PasswordSalt { get; set; }

      public virtual ICollection<employee> Employees { get; set; }

   }
}
