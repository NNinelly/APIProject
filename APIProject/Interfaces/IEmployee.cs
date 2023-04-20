using APIProject.DTO;

namespace APIProject.Interfaces
{
   public interface IEmployee
   {
      public Task<bool> AddEmployee(EmployeeDTO employee);
      public Task<List<EmployeeDTO>> GetEmployees();
      public Task<bool> GetEmployeeByName(string name, string lastName);
      public Task<bool> UpdateEmployee(EmployeeDTO employee);
      public Task<bool> DeleteEmployee(EmployeeDTO employee);
   }
}
