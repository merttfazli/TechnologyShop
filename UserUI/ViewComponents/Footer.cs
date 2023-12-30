using Microsoft.AspNetCore.Mvc;

namespace UserUI.ViewComponents
{
    public class Footer:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
