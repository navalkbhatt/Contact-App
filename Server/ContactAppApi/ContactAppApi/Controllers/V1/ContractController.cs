using ContactApp.Application.Dto;
using ContactApp.Application.UseCases.Commons;
using ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [HttpPost("contacts/add")]
        public async Task<Response<bool>> GetAllAsync()
        {
            var response = await _mediator.Send(new CreateContactCommand());

            return new Response<bool> { Body = response.Body};
        }
    }
}
