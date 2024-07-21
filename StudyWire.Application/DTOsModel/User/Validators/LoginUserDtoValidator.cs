using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.User.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email field is required");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password field is required");

        }
    }
}
