using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace StudyWire.Application.DTOsModel.User.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(user => user.Password)
                .MinimumLength(6)
                .WithMessage("Password must be minimum 6 character long")
                .Matches("""^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$""")
                .WithMessage("Password must have at least one letter, one number and one special character");


            RuleFor(user => user.ConfirmPassword)
                .Equal(user => user.Password)
                .WithMessage("Confirm password and password must be equal");

            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("This is not an email format");


            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Name field is required");

            RuleFor(user => user.Surename)
                .NotEmpty()
                .WithMessage("Surename field is required");
        }
    }
}
