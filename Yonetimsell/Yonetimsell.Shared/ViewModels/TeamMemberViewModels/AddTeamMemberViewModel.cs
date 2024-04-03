﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Shared.ViewModels.FriendshipViewModels;

namespace Yonetimsell.Shared.ViewModels.TeamMemberViewModels
{
    public class AddTeamMemberViewModel
    {
        public TeamMemberViewModel TeamMemberViewModel { get; set; }
        public List<FriendshipViewModel> Friendships { get; set; }
        public string CurrentUserId { get; set; }
    }
}

