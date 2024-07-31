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
                .Matches("""(?:([+]\d{1,4})[-.\s]?)?(?:[(](\d{1,3})[)][-.\s]?)?(\d{1,4})[-.\s]?(\d{1,4})[-.\s]?(\d{1,9})""")
                .WithMessage("Phone number field is not in valid format");
                

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
