using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    [Authorize(Policy = "ElevatedRights", Roles = "Admin,User")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IUserGroupService _userGroupService;

        public UserController(IUserService userService, IUserGroupService userGroupService)
        {
            _userService = userService;
            _userGroupService = userGroupService;
        }

        [Route("users")]
        public IActionResult Index()
        {
            var userDtos = _userService.GetUserDtos();
            var userGroups= _userGroupService.GetAll();
            return View(new UserModel
            {
                User=new User(),
                Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                UserGroups=userGroups.Success ? userGroups.Data: new List<UserGroup>(),
                Result = new ResultModel()
            });
        }
        [Route("users/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    var data=_userService.Add(user);
                    var userDtos = _userService.GetUserDtos();
                    var userGroups = _userGroupService.GetAll();
                    ModelState.Clear();
                    return View("Index", new UserModel
                    {
                        User = new User(),
                        Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                        UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                        Result = new ResultModel() { Error=data.Success,Message=data.Message}
                    });
                }
                else
                {
                    var data = _userService.Update(user);
                    var userDtos = _userService.GetUserDtos();
                    var userGroups = _userGroupService.GetAll();
                    ModelState.Clear();
                    return View("Index", new UserModel
                    {
                        User = new User(),
                        Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                        UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                        Result = new ResultModel() { Error = data.Success, Message = data.Message }
                    });
                }
            }
            else
            {
                var userDtos = _userService.GetUserDtos();
                var userGroups = _userGroupService.GetAll();
                ModelState.Clear();
                ViewBag.Message = "Model Hatalı";
                return View("Index", new UserModel
                {
                    User = new User(),
                    Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel()
                });

            }
        }

        [Route("users/{id}")]
        public IActionResult Index(int id)
        {
            var user = _userService.GetById(id);
            var userDtos = _userService.GetUserDtos();
            var userGroups = _userGroupService.GetAll();
            if (user == null)
            {
                return Redirect("/users");
            }
            return View(new UserModel
            {
                User = user.Success ? user.Data : new User(),
                Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                Result = new ResultModel()
            });
        }

        [Route("users/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var userForDelete = _userService.GetById(id);
            var user = _userService.GetById(id);

            if (user.Success)
            {
                var data = _userService.Delete(userForDelete.Data);
                var userGroups = _userGroupService.GetAll();
                var userDtos = _userService.GetUserDtos();

                UserModel model = new UserModel
                {
                    Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                    User = new User(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel() { Error = data.Success, Message = data.Message }
                };
                return View("Index", model);
            }
            else
            {
                var userDtos = _userService.GetUserDtos();
                var userGroups = _userGroupService.GetAll();

                return View("Index", new UserModel
                {
                    Users = userDtos.Success ? userDtos.Data : new List<UserDto>(),
                    User = new User(),
                    UserGroups = userGroups.Success ? userGroups.Data : new List<UserGroup>(),
                    Result = new ResultModel() { Error = user.Success, Message = user.Message }
                });
            }
        }
    }
}
