using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.School.Validators
{
    public class PostSchoolDtoValidator : AbstractValidator<PostSchoolDto>
    {
        public PostSchoolDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Name field is required");

            RuleFor(s => s.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number field is required")
                .Matches("""^[0-9]*$""")
                .WithMessage("Phone number can have only numbers");
                

            RuleFor(s => s.Street)
                .NotEmpty()
                .WithMessage("Street field is required");

            RuleFor(s => s.City)
                .NotEmpty()
                .WithMessage("City field is required");

            RuleFor(s => s.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code field is required");
        }
    }
}
