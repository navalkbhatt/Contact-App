using ContactApp.Application.Dto;
using ContactApp.Application.UseCases.Commons;
using ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand;
using ContactApp.Application.UseCases.Contacts.Commands.DeleteContactCommand;
using ContactApp.Application.UseCases.Contacts.Queries.GetAllContacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContractController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<Response<IEnumerable<ContactDto>>> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllContactQuery());

            return response;
        }
        [HttpPost]
        public async Task<Response<bool>> AddContact([FromBody] CreateContactCommand command)
        {
            var response = await _mediator.Send(command);

            return new Response<bool> { Body = response.Body };
        }
        [HttpDelete]
        public async Task<Response<bool>> DeleteContact([FromBody] DeleteContactCommand command)
        {
            var response = await _mediator.Send(command);

            return new Response<bool> { Body = response.Body };
        }
    }
}
