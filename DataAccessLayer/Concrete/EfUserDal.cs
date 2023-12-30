using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {
        public List<UserDto> GetUserDtos()
        {
            using (Context context = new Context())
            {
                var result = from p in context.Users
                             join c in context.UserGroups
                             on p.UserGroupId equals c.Id
                             select new UserDto { Id = p.Id, Name = p.Name, GroupName = c.Name ,Username=p.Username,Password=p.Password,Email=p.Email,UserGroupId=p.UserGroupId,Phone=p.Phone};
                return result.ToList();
            }
        }

        public UserDto GetUserDto(int id)
        {
            using (Context context = new Context())
            {
                var result = from p in context.Users.Where(x => x.Id == id)
                             join c in context.UserGroups
                             on p.UserGroupId equals c.Id
                             select new UserDto { Id = p.Id, Name = p.Name, GroupName = c.Name, Username = p.Username, Password = p.Password, Email = p.Email, UserGroupId = p.UserGroupId, Phone = p.Phone };
                return result.FirstOrDefault();
            }
        }
    }
}
