using APIProject.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace APIProject.Validation
{
   public class EmployeeValidator : AbstractValidator<employee>
   {
      public EmployeeValidator()
      {
         RuleFor(e => e.Name)
            .NotNull().WithMessage("Name Date cannot be null")
            .NotEmpty().WithMessage("Name field is required");
         RuleFor(e => e.LastName).NotNull().NotEmpty();
         RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is required");
         RuleFor(e => e.PN).Must(e => e.ToString().Length == 11)
            .WithMessage("Please enter correct PN");
         RuleFor(e => e.Position).NotNull().NotEmpty();
         RuleFor(e => e.Status).NotNull().NotEmpty();
         RuleFor(e => e.Phone)
       .NotEmpty()
       .NotNull().WithMessage("Phone Number is required.")
       .MinimumLength(9).WithMessage("PhoneNumber must not be less than 9 characters.")
       .MaximumLength(15).WithMessage("PhoneNumber must not exceed 15 characters.");
            
      }
   }
}
