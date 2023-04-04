using AutoMapper;

namespace ContactBookApi.Models
{
    public class ContactBookProfile : Profile
    {
        public ContactBookProfile() {
            CreateMap<User, ProfileDTO>().ReverseMap();
        }
    }
}
