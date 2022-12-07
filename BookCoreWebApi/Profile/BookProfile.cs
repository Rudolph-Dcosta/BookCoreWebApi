using BookCoreWebApi.Models.Domain;
using BookCoreWebApi.Models.DTO;

namespace BookCoreWebApi.Profile
{
    public class BookProfile : AutoMapper.Profile
    {
        public BookProfile()
        {
            CreateMap<Book,BookDTO>().ReverseMap();
        }
    }
}
