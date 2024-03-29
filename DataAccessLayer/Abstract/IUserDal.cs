﻿using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        UserDto GetUserDto(int id);
        List<UserDto> GetUserDtos();
    }
}
