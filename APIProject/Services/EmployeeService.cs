using APIProject.ApplicationDbContext;
using APIProject.DTO;
using APIProject.Interfaces;
using APIProject.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace APIProject.Services
{
   public class EmployeeService : IEmployee
   {
      private readonly APIDbContext ent = new APIDbContext();
      private readonly IConfiguration _configuration;
      private readonly IValidator<employee> validator;
      public EmployeeService(APIDbContext entity, IConfiguration configuration, IValidator<employee> validator)
      {
         ent = entity;
         _configuration = configuration;
         this.validator = validator;
      }
      
      public async Task<bool> AddEmployee(EmployeeDTO employee)
      {
         if (ent.employees.Any(i => i.PN == employee.PN || i.Email == employee.Email))
         {
            throw new Exception("This Employee Already Exist");

         }
         employee person = new employee()
         {
            UserId = employee.UserId,
            Name = employee.Name,
            LastName = employee.LastName,
            Email = employee.Email,
            PN = employee.PN,
            Sex = employee.Sex,
            BirthDate = employee.BirthDate,
            Position = employee.Position,
            Status = employee.status,
            FiredDate = employee.FiredDate,
            Phone = employee.Phone
         };
         await validator.ValidateAndThrowAsync(person);
         await ent.employees.AddAsync(person);
         await ent.SaveChangesAsync();
         return true;
      }

      public async Task<List<EmployeeDTO>> GetEmployees()
      {
         var res = await ent.employees.Select(i => new EmployeeDTO
         {
            Id = i.Id,
            UserId= i.UserId,
            Name = i.Name,
            LastName = i.LastName,
            Email = i.Email,
            PN = i.PN,
            Sex = i.Sex,
            BirthDate = i.BirthDate,
            Position = i.Position,
            status = i.Status,
            FiredDate = i.FiredDate,
            Phone = i.Phone
         }).ToListAsync();

         return res;
      }
      public async Task<bool> GetEmployeeByName(string name, string lastName)
      {

         var person = await ent.employees.Where(x => x.Name == name && x.LastName == lastName).FirstOrDefaultAsync();

         var employee = new EmployeeDTO
         {
            Id = person.Id,
            UserId = person.UserId,
            Name = person.Name,
            LastName = person.LastName,
            Email = person.Email,
            PN = person.PN,
            Sex = person.Sex,
            BirthDate = person.BirthDate,
            Position = person.Position,
            status = person.Status,
            FiredDate = person.FiredDate,
            Phone = person.Phone
         };
         return true;
      }
      public async Task<bool> UpdateEmployee(EmployeeDTO employee)
      {
         var person = await ent.employees.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();
         person.Id = employee.Id;
         person.UserId = employee.UserId;
         person.Name = employee.Name;
         person.LastName = employee.LastName;
         person.Email = employee.Email;
         person.PN = employee.PN;
         person.Sex = employee.Sex;
         person.BirthDate = employee.BirthDate;
         person.Position = employee.Position;
         person.Status = employee.status;
         person.FiredDate = employee.FiredDate;
         person.Phone = employee.Phone;

         await validator.ValidateAndThrowAsync(person);
         ent.employees.Update(person);
         await ent.SaveChangesAsync();
         return true;
      }

      public async Task<bool> DeleteEmployee(EmployeeDTO employee)
      {
         var person = await ent.employees.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();

         person.UserId = employee.UserId;
         person.PN = employee.PN;

         ent.employees.Remove(person);
         await ent.SaveChangesAsync();
         return true;
      }
   }
}
