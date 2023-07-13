using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Infrasrtucture.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // src , dist
            CreateMap<CreateUserDto, User>().
                ForMember(x => x.ImageUrl, x => x.Ignore());

            CreateMap<UpdateUserDto, User>().
               ForMember(x => x.ImageUrl, x => x.Ignore());

            CreateMap<User, UpdateUserDto>().
              ForMember(x => x.ImageUrl, x => x.Ignore());
            
            CreateMap<User, UserViewModel>().ForMember(x=> x.DOB , x => x.MapFrom(x => x.DOB.ToShortDateString()));


        }
    }
}
