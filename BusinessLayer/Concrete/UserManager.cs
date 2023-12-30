using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager:IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            var getUser = _userDal.Get(x => x.Name == user.Name);
            if (getUser != null) 
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            else
            {
                if (checkEquality(user))
                    return new ErrorResult(Messages.UserAlreadyExists);
                else
                {
                    _userDal.Add(user);
                    return new SuccessResult(Messages.UserAdded);
                }
            }
        }
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
        public IDataResult<List<User>> GetAll()
        {
            var users = _userDal.GetAll();
            if (users.Any())
                return new SuccessDataResult<List<User>>(users);
            else
                return new ErrorDataResult<List<User>>(Messages.UserNotFound);
        }
        public IDataResult<User> GetById(int id)
        {
            var user=_userDal.Get(x => x.Id == id);
            if (user != null)
                return new SuccessDataResult<User>(user);
            else
                return new ErrorDataResult<User>(Messages.UserNotFound);
        }
        public IDataResult<User> GetByMail(string mail)
        {
            var user = _userDal.Get(x => x.Email == mail);
            if (user != null)
                return new SuccessDataResult<User>(user);
            else
                return new ErrorDataResult<User>(Messages.UserNotFound);
        }
        public IDataResult<UserDto> GetUserDto(int id)
        {
            var userDto = _userDal.GetUserDto(id);
            if (userDto != null)
                return new SuccessDataResult<UserDto>(userDto);
            else
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
        }
        public IDataResult<List<UserDto>> GetUserDtos()
        {
            var userDtos = _userDal.GetUserDtos();
            if (userDtos.Any())
                return new SuccessDataResult<List<UserDto>>(userDtos);
            else
                return new ErrorDataResult<List<UserDto>>(Messages.UserNotFound);
        }
        public IResult ResetUserPassword(string email, string password)
        {
            var getUser = GetByMail(email);
            if (getUser !=null)
            {
                getUser.Data.Password = password;
                return new SuccessResult(Messages.PasswordUpdated);
            }
            else
                return new ErrorResult(Messages.UserNotFound);
        }
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
        public IResult UpdateUserDto(UserForRegisterDto user)
        {
            User currentUser = _userDal.Get(x => x.Id == user.Id);
            if (currentUser == null)
                return new ErrorResult(Messages.UserNotFound);
            else
            {
                if (checkEqualityForUpdateDto(user))
                    return new ErrorResult(Messages.UserAlreadyExists);
            }

            currentUser.UserGroupId = user.GroupId;
            currentUser.Email = user.Email;
            currentUser.Username = user.Username;
            currentUser.Password = user.Password;
            currentUser.Name = user.Name;
            currentUser.Phone = user.Phone;

            if (!string.IsNullOrEmpty(user.Password))
                currentUser.Password = user.Password;

            _userDal.Update(currentUser);
            return new SuccessResult(Messages.UserUpdated);
        }
        private bool checkEquality(User user)
        {
            var users = _userDal.GetAll();
            foreach (var getUser in users)
            {
                if (getUser.Email.Equals(user.Email) || getUser.Username.Equals(user.Username))
                    return true;
                else
                    return false;
            }
            return true;
        }
        private bool checkEqualityForUpdateDto(UserForRegisterDto user)
        {
            var users = _userDal.GetUserDtos();
            foreach (var getUser in users)
            {
                if (getUser.Email.Equals(user.Email) || getUser.Username.Equals(user.Username))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
