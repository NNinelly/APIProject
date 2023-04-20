namespace APIProject.DTO
{
   public class EmployeeDTO 
   {
      public int Id { get; set; }
      public int UserId { get; set; }
      public string PN { get; set; }
      public string Name { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string Sex { get; set; }
      public DateTime BirthDate { get; set; }
      public string Position { get; set; }
      public Status status{ get; set; }
      public DateTime? FiredDate { get; set; }
      public string Phone { get; set; }

      public enum Status
      {
         InStaff = 1,
         OutOfStaff = 2,
         Fired = 3
      }
   }
   public class GetResponse : ResultClass
   {
      public bool Result { get; set; }
   }
   public class GetEmployeeResponse : ResultClass
   {
      public List<EmployeeDTO> Result { get; set; }
   }
}
