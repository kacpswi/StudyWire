using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWire.Application.DTOsModel.News.Validators
{
    public class PostNewsDtoValidator : AbstractValidator<PostNewsDto>
    {
        public PostNewsDtoValidator() {

            RuleFor(n => n.Title)
                .NotEmpty()
                .WithMessage("Title field is required")
                .MaximumLength(70)
                .WithMessage("Maksimum title length is 50 characters");

            RuleFor(n => n.Description)
                .NotEmpty()
                .WithMessage("Description field is required")
                .MaximumLength(100)
                .WithMessage("Maksimum description length is 100 characters");

            RuleFor(n => n.Content)
                .NotEmpty()
                .WithMessage("Content field is required")
                .MinimumLength(200)
                .WithMessage("Minimum content length is 200 characters");


        }
    }
}
