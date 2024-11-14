using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand
{
    public class CreateCustomerValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
        }
    }
}
