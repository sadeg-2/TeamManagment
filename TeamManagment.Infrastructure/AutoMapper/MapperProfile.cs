

namespace TeamManagment.Infrasrtucture.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
     
            CreateMap<User, UserViewModel>().ForMember(x=> x.DOB , x => x.MapFrom(x => x.DOB.ToShortDateString()));

            CreateMap<MyTask, TaskViewModel>()
                .ForMember(x => x.DeadLine, x => x.MapFrom(x => x.DeadLine.ToShortDateString()));
            CreateMap<UpdateTaskDto, MyTask>();
            CreateMap<MyTask, UpdateTaskDto>();
            CreateMap<CreateTaskDto, MyTask>();

            CreateMap<Team, TeamViewModel>();

            CreateMap<UpdateTeamDto, Team>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<Team, UpdateTeamDto>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<CreateTeamDto, Team>();




        }
    }
}
