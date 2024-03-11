using AutoMapper;
using Domain.Dto;
using Domain.Entity;


namespace BusinessLogic.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
