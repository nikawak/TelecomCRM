using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands.Customer;

namespace TelecomCRM.Application.Validators.Customers
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.FullName)
           .NotEmpty().WithMessage("Полное имя обязательно для заполнения.")
           .MaximumLength(60).WithMessage("Длина полного имени не должна превышать 60 символов.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефона обязателен.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Введите корректный номер телефона.");

            RuleFor(x => x.Address)
                .MaximumLength(150).WithMessage("Длина адреса не должна превышать 150 символов.");
        }
    }
}
