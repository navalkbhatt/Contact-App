using AutoMapper;
using ContactApp.Application.UseCases.Commons;
using MediatR;

namespace ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand, Response<bool>>
    {

        private readonly IMapper _mapper;

        public CreateContactHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<bool>> Handle(CreateContactCommand command, CancellationToken cancellationToken)
        {

            var response = new Response<bool>();
            string path = FileUtility.GetFilePath("Contact.json");

            return response;
        }
    }
}
