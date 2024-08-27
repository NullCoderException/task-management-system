using FluentValidation;
using TaskManager.Api.DTOs;

namespace TaskManager.Api.Validators
{
    public class TaskDtoValidator : AbstractValidator<TaskItemDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.DueDate).GreaterThanOrEqualTo(DateTime.Today);
        }
    }
}