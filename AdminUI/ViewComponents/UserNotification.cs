using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.ViewComponents
{
    public class UserNotification:ViewComponent
    {
        IUserService _userService;
        public UserNotification(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            //var usermail = User.Identity.Name;
            //var user = _userService.GetByMail(usermail);
            //var values = _userService.GetDetail(user.Id);
            return View(/*values*/);

        }
    }
}
