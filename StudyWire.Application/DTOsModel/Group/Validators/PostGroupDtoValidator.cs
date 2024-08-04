using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.Group.Validators
{
    public class PostGroupDtoValidator : AbstractValidator<PostGroupDto>
    {
        public PostGroupDtoValidator()
        {
            RuleFor(g => g.GroupName)
                .NotEmpty()
                .WithMessage("Name field is required.");
        }
    }
}
