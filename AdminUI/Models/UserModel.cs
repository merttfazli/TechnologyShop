using EntityLayer.Concrete;
using EntityLayer.Concrete.Dtos;

namespace AdminUI.Models
{
    public class UserModel
    {
        public User User { get; set; }
        public List<UserDto> Users { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public ResultModel Result { get; set; }

    }
}
