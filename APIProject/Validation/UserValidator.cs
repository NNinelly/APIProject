using APIProject.Models;
using FluentValidation;

namespace APIProject.Validator
{
   public class UserValidator : AbstractValidator<User>
   {
      public UserValidator() 
      { 
         RuleFor(u => u.UserName)
            .NotNull().WithMessage("UserName Date cannot be null")
            .NotEmpty().WithMessage("UserName field is required");
         RuleFor(u => u.LastName).NotNull().NotEmpty();
         RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is required");
         RuleFor(u => u.PN).Must(u => u.ToString().Length == 11)
            .WithMessage("Please enter correct PN");
      }
   }
}
