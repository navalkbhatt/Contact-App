using ContactApp.Application.Dto;
using ContactApp.Application.UseCases.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Queries.GetAllContacts
{
    public class GetAllContactQuery : IRequest<Response<IEnumerable<ContactDto>>>
    {
    }
}
