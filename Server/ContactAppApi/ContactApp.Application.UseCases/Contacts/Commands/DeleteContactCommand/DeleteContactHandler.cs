using ContactApp.Application.UseCases.Commons;
using ContactApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Contacts.Commands.DeleteContactCommand
{
    internal class DeleteContactHandler : IRequestHandler<DeleteContactCommand, Response<bool>>
    {
        public async Task<Response<bool>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
           
        
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
                    contacts = contacts.Where(x => x.Id != request.Id).ToList<Contact>();
                    jsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(path, jsonString);
                    response.Body = true;
                    response.Message = "Record Deleted!";

                } else
                {
                    response.Message = "Record Not Deleted!";
                }


                // Write the JSON string to a file asynchronously


            }

            // Serialize the list of objects to JSON



            return response;
        }
    }
}
