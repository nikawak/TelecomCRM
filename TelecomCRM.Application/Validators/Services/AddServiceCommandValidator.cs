using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.Commands.Services;

namespace TelecomCRM.Application.Validators.Services
{
    public class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Не может быть пустым")
                .MaximumLength(50).WithMessage("Не может быть таким длинным")
                .MinimumLength(10).WithMessage("Не может быть таким коротким");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Не может быть пустым")
                .MaximumLength(500).WithMessage("Не может быть таким длинным")
                .MinimumLength(50).WithMessage("Не может быть таким коротким");

            RuleFor(x => x.MonthlyFee)
                .NotEmpty().WithMessage("Не может быть пустым")
                .GreaterThanOrEqualTo(0).WithMessage("Неправильная цена");
        }
    }
}
