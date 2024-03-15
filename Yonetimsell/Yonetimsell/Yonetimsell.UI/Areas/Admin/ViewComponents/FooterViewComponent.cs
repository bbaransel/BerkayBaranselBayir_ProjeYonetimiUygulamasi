using Microsoft.AspNetCore.Mvc;

namespace Yonetimsell.UI.Areas.Admin.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
