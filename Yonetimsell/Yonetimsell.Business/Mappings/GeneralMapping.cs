using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;
using Yonetimsell.Shared.ViewModels.TeamMemberViewModels;

namespace Yonetimsell.Business.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping() 
        {
            CreateMap<PTask, PTaskViewModel>().ReverseMap();
            CreateMap<TeamMember, TeamMemberViewModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<AddProjectViewModel, Project>().ReverseMap();
            CreateMap<ProjectViewModel, Project>().ReverseMap();
            CreateMap<Friendship, FriendshipViewModel>()
                .ForMember(dest => dest.SenderUserName, opt => opt.MapFrom(src => src.SenderUser.UserName))
                .ForMember(dest => dest.ReceiverUserName, opt => opt.MapFrom(src => src.ReceiverUser.UserName));
            CreateMap<List<Friendship>, List<FriendshipViewModel>>();
        }
    }
}
