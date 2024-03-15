using Microsoft.AspNetCore.Mvc;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class SpinnerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
