using AutoMapper;
using ContactsApi.Dto;
using ContactsApi.Models;

namespace ContactsApi.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
        }
    }
}
