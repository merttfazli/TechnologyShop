using Microsoft.AspNetCore.Mvc;

namespace UserUI.ViewComponents
{
    public class Header: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
