using BusinessLayer.Utilities.Results;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByMail(string mail);
        IDataResult<UserDto> GetUserDto(int id);
        IDataResult<List<UserDto>> GetUserDtos();
        IResult UpdateUserDto(UserForRegisterDto user);
        IResult ResetUserPassword(string email, string password);
    }
}
