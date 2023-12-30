using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserGroupService
    {
        IDataResult<List<UserGroup>> GetAll();
        IResult Add(UserGroup userGroup);
        IResult Update(UserGroup userGroup);
        IResult Delete(UserGroup userGroup);
        IDataResult<UserGroup> GetById(int id);
    }
}
