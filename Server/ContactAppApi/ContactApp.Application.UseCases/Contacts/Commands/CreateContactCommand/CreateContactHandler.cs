using AutoMapper;
using ContactApp.Application.UseCases.Commons;
using MediatR;
using System.Text.Json;
using System;
using ContactApp.Domain.Entities;
using System.IO;

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
            var contact = new List<Contact>
            {
             new Contact
             {
                 Id=Guid.NewGuid(),
                 FirstName=command.FirstName,
                 LastName=command.LastName, 
                 Email= command.Email,
             }
        };

            // Define the file path for the JSON file
            string path = FileUtility.GetFilePath("Contact.json");
            var contacts = new List<Contact>();
            string jsonString = string.Empty;
            if (File.Exists(path))
            {
                string existingJson = await File.ReadAllTextAsync(path);

                if (!string.IsNullOrEmpty(existingJson) && !existingJson.Contains("{}"))
                {
                    contacts = JsonSerializer.Deserialize<List<Contact>>(existingJson) ?? new List<Contact>();
                    contacts.Add(new Contact {Id=Guid.NewGuid(), FirstName = command.FirstName, LastName = command.LastName, Email = command.Email });
                    jsonString= JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

                }
                else {
                     jsonString = JsonSerializer.Serialize(contact, new JsonSerializerOptions { WriteIndented = true });
                }

                // Write the JSON string to a file asynchronously
                await File.WriteAllTextAsync(path, jsonString);


            }

            // Serialize the list of objects to JSON



            return response;
        }
    }
}
