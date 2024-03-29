using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;
using Yonetimsell.Shared.ViewModels.PTaskViewModels;

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
        }
    }
}
