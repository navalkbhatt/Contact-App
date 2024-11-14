using ContactApp.Application.UseCases.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Commands.DeleteContactCommand
{
    public class DeleteContactCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }
}
