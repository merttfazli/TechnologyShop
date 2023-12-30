using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class UserGroupController : Controller
    {
        private IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [Route("user-groups")]
        public IActionResult Index()
        {
            var userGroups = _userGroupService.GetAll();
            return View(new UserGroupModel
            {
                UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                Result= new ResultModel(),
                UserGroup = new UserGroup()
            });
        }

        [Route("users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                if (userGroup.Id == 0)
                {
                    var data=_userGroupService.Add(userGroup);
                    var userGroups = _userGroupService.GetAll();
                    ModelState.Clear();
                    return View("Index", new UserGroupModel
                    {
                        UserGroup = new UserGroup(),
                        UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                        Result = new ResultModel() { Error=data.Success,Message=data.Message},
                    });
                }
                else
                {
                    var data = _userGroupService.Update(userGroup);
                    var userGroups = _userGroupService.GetAll();
                    ModelState.Clear();
                    return View("Index", new UserGroupModel
                    {
                        UserGroup = new UserGroup(),
                        UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message },
                    });
                }
            }
            else
            {
                var userGroups = _userGroupService.GetAll();
                ModelState.Clear();
                ViewBag.Message = "Model Hatalı";
                return View("Index", new UserGroupModel
                {
                    UserGroup = new UserGroup(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel(),
                });
            }
        }

        [Route("user-groups/{id}")]
        public IActionResult Index(int id)
        {
            var userGroup = _userGroupService.GetById(id);
            var userGroups = _userGroupService.GetAll();
            if (userGroup == null)
            {
                return Redirect("/user-groups");
            }

            return View(new UserGroupModel
            {
                UserGroup = userGroup.Success ? userGroup.Data: new UserGroup(),
                UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                Result = new ResultModel(),
            });
        }

        [Route("user-groups/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var userGroup = _userGroupService.GetById(id);

            if (userGroup != null)
            {
                var data = _userGroupService.Delete(userGroup.Data);
                var userGroups = _userGroupService.GetAll();

                UserGroupModel model = new UserGroupModel
                {
                    UserGroup = new UserGroup(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message},
                };
                return View("Index", model);
            }
            else
            {
                ViewBag.Message = "User Group Bulunamadı ";
                var userGroups = _userGroupService.GetAll();

                return View("Index", new UserGroupModel
                {
                    UserGroup = new UserGroup(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel() { Error = userGroup.Success, Message = userGroup.Message }
                });
            }
        }
    }
}
