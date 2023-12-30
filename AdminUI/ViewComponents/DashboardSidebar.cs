using AdminUI.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.ViewComponents
{
    public class DashboardSidebar:ViewComponent
    {
        IModuleService _moduleService;
        public DashboardSidebar(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        public IViewComponentResult Invoke()
        {
            List<MenuViewModel> menu = new List<MenuViewModel>();
            var modules = _moduleService.GetAllByRoot(0);
            foreach (var item in modules)
            {
                menu.Add(new MenuViewModel { Module = item, Modules = _moduleService.GetAllByRoot(item.Id) });

            }
            return View(menu);
        }

    }
}
