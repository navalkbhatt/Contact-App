using ContactApp.Application.UseCases.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand
{
    public class CreateContactCommand : IRequest<Response<bool>>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
