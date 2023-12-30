using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserGroupManager : IUserGroupService
    {
        private readonly IUserGroupDal _userGroupDal;
        public UserGroupManager(IUserGroupDal userGroupDal)
        {
            _userGroupDal = userGroupDal;
        }
        public IResult Add(UserGroup userGroup)
        {
            var getUserGroup = _userGroupDal.Get(x => x.Name == userGroup.Name);
            if (getUserGroup != null)
                return new ErrorResult(Messages.UserGroupNotFound);
            else
            {
                if (checkEquality(userGroup))
                    return new ErrorResult(Messages.UserGroupAlreadyExists);
                else
                {
                    _userGroupDal.Add(userGroup);
                    return new SuccessResult(Messages.UserGroupAdded);
                }
            }
        }
        public IResult Delete(UserGroup userGroup)
        {
            _userGroupDal.Delete(userGroup);
            return new SuccessResult(Messages.UserGroupDeleted);
        }
        public IDataResult<List<UserGroup>> GetAll()
        {
            var userGroups = _userGroupDal.GetAll();
            if (userGroups.Any())
                return new SuccessDataResult<List<UserGroup>>(userGroups);
            else
                return new ErrorDataResult<List<UserGroup>>(Messages.UserGroupNotFound);
        }
        public IDataResult<UserGroup> GetById(int id)
        {
            var userGroup = _userGroupDal.Get(x => x.Id == id);
            if (userGroup != null)
                return new SuccessDataResult<UserGroup>(userGroup);
            else
                return new ErrorDataResult<UserGroup>(Messages.UserGroupNotFound);
        }
        public IResult Update(UserGroup userGroup)
        {
            var getuserGroup = _userGroupDal.Get(x => x.Name == userGroup.Name && x.Id != userGroup.Id);
            if (getuserGroup != null)
                return new ErrorResult(Messages.UserGroupUpdated);
            else
            {
                _userGroupDal.Update(userGroup);
                return new SuccessResult(Messages.UserGroupUpdated);
            }
        }
        private bool checkEquality(UserGroup userGroup)
        {
            var userGroups = _userGroupDal.GetAll();
            foreach (var getUserGroup in userGroups)
            {
                if (getUserGroup.Name.Equals(userGroup.Name))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
