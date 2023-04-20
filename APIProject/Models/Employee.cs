using static APIProject.DTO.EmployeeDTO;

namespace APIProject.Models
{
   public class employee
   {
      public int Id { get; set; }
      public string PN { get; set; }
      public string Name { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string Sex { get; set; }
      public DateTime BirthDate { get; set; }
      public string Position { get; set; }
      public Status Status { get; set; }
      public DateTime? FiredDate { get; set; }
      public string Phone { get; set; }

      public int UserId { get; set; }
      public User user { get; set; }
   }
}
