using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        IResult RegisterUser(UserForRegisterDto userForRegisterDto);
        IDataResult<UserDto> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
    }
}
