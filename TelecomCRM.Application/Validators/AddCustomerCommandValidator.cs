using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands;

namespace TelecomCRM.Application.Validators
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Полное имя обязательно для заполнения.")
            .MaximumLength(60).WithMessage("Длина полного имени не должна превышать 60 символов.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен.")
                .EmailAddress().WithMessage("Введите корректный email.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефона обязателен.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Введите корректный номер телефона.");

            RuleFor(x => x.Address)
                .MaximumLength(150).WithMessage("Длина адреса не должна превышать 150 символов.");
        }
    }
}
