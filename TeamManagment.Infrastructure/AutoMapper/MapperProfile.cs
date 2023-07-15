

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

            CreateMap<MyTask, TaskViewModel>()
                .ForMember(x => x.DeadLine, x => x.MapFrom(x => x.DeadLine.ToShortDateString()));
            CreateMap<UpdateTaskDto, MyTask>();
            CreateMap<MyTask, UpdateTaskDto>();
            CreateMap<CreateTaskDto, MyTask>();


        }
    }
}
