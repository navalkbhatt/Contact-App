using AutoMapper;
using ContactApp.Application.Dto;
using ContactApp.Application.UseCases.Contacts.Commands.CreateContactCommand;
using ContactApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactApp.Application.UseCases.Mapping
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactCommand>().ReverseMap();
            
        }
    }
}
