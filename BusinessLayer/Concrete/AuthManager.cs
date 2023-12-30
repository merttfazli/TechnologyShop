using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<UserDto> Login(UserForLoginDto userForLoginDto)
        {
            var user = _userService.GetByMail(userForLoginDto.Email).Data;
            if (user == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
            if (user.Email == userForLoginDto.Email && user.Password == userForLoginDto.Password)
            {
                var userDto = _userService.GetUserDto(user.Id).Data;
                return new SuccessDataResult<UserDto>(userDto);
            }
            return new ErrorDataResult<UserDto>(Messages.WrongPassword);
        }

        public IResult RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            if (UserExists(userForRegisterDto.Email).Success)
            {
                var result = _userService.Add(new User
                {
                    UserGroupId = userForRegisterDto.Id,
                    Name = userForRegisterDto.Name,
                    Email = userForRegisterDto.Email,
                    Phone = userForRegisterDto.Phone,
                    Password = userForRegisterDto.Password,
                    Username = userForRegisterDto.Username
                });
                if (result.Success)
                    return new SuccessResult(Messages.UserAdded);
                else
                    return new ErrorResult(Messages.UserAddFailure);
            }
            else
                return new ErrorResult(Messages.EmailAlreadyExists);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
                return new ErrorResult(Messages.UserAlreadyExists);
            else
                return new SuccessResult();
        }
    }
}
