namespace Yonetimsell.UI.Areas.Admin.Models
{
    public class HomePageViewModel
    {
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int CompletedProjectCount { get; set; }
        public int TotalPTaskCount { get; set; }
        public int ActivePTaskCount { get; set; }
        public int CompletedPTaskCount { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalSubscriptionCount { get; set; }
    }
}
