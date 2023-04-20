using APIProject.ApplicationDbContext;
using APIProject.DTO;
using APIProject.Interfaces;
using APIProject.Iservices;
using APIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [Authorize]
   public class EmployeeController : ControllerBase
   {
      private readonly APIDbContext _dbContext;
      private readonly IEmployee _employee;
      public EmployeeController(APIDbContext dbContext, IEmployee employee)
      {
         _dbContext = dbContext;
         _employee = employee;
      }

      [HttpPost("AddEmployee")]
      public async Task<GetResponse> AddEmployee(EmployeeDTO employee)
      {
         try
         {
            var res = await _employee.AddEmployee(employee);
            return new GetResponse { StatusCode = 200, Message = "The employee added successfully", Result = res };
         }
         catch (Exception ex)
         {
            return new GetResponse { Message = ex.Message, StatusCode = 400 };
         }
      }

      [HttpGet("GetAllEmployee")]
      public async Task<GetEmployeeResponse> GetEmployees()
      {
         try
         {
            var res = await _employee.GetEmployees();
            return new GetEmployeeResponse { StatusCode = 200, Message = "Success", Result = res };
         }
         catch (Exception ex)
         {
            return new GetEmployeeResponse { Message = ex.Message, StatusCode = 400 };
         }
      }

      [HttpGet("GetEmployeeByName")]
      public async Task<GetResponse> GetEmployeeByName(string name, string lastName)
      {
         try
         {
            var res = await _employee.GetEmployeeByName(name, lastName);
            return new GetResponse { StatusCode = 200, Message = "Success", Result = res };
         }
         catch (Exception ex)
         {
            return new GetResponse { Message = ex.Message, StatusCode = 400 };
         }
      }
      [HttpPut("UpdateEmployee")]
      public async Task<GetResponse> UpdateEmployee(EmployeeDTO employee)
      {
         try
         {
            var res = await _employee.UpdateEmployee(employee);
            return new GetResponse { StatusCode = 200, Message = "The employee updated successfully", Result = res };
         }
         catch (Exception ex)
         {
            return new GetResponse { Message = ex.Message, StatusCode = 400 };
         }
      }

      [HttpDelete("DeleteEmployee")]
      public async Task<GetResponse> DeleteEmployee(EmployeeDTO employee)
      {
         try
         {
            var res = await _employee.DeleteEmployee(employee);
            return new GetResponse { StatusCode = 200, Message = "The employee has been deleted", Result = res };
         }
         catch (Exception ex)
         {
            return new GetResponse { Message = ex.Message, StatusCode = 400 };
         }
      }

   }
}
