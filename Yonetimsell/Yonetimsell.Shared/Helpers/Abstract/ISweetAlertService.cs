namespace Yonetimsell.Shared.Helpers.Abstract
{
    public interface ISweetAlertService
    {
        public string TopEndNotification(string icon, string title);
        public string MiddleNotification(string icon, string title);
    }
}
