using AutoMapper;
using ContactApp.Application.Dto;
using ContactApp.Application.UseCases.Commons;
using ContactApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Queries.GetAllContacts
{
    public class GetAllContactHandler : IRequestHandler<GetAllContactQuery, Response<IEnumerable<ContactDto>>>
    {
        private readonly IMapper _mapper;
        public GetAllContactHandler(IMapper mapper)
        {
                this._mapper =mapper;
        }
        public async Task<Response<IEnumerable<ContactDto>>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<ContactDto>>();

            try
            {

                string path = FileUtility.GetFilePath("Contact.json");
                var contacts = new List<Contact>();
                string jsonString = string.Empty;
                if (File.Exists(path))
                {
                    string existingJson = await File.ReadAllTextAsync(path);

                    if (!string.IsNullOrEmpty(existingJson) && !existingJson.Contains("{}"))
                    {
                        contacts = JsonSerializer.Deserialize<List<Contact>>(existingJson) ?? new List<Contact>();
                        var contactDto = _mapper.Map<IEnumerable<ContactDto>>(contacts);

                        return new Response<IEnumerable<ContactDto>> { Body = contactDto };
                        
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
