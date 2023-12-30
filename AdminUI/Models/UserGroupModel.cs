using EntityLayer.Concrete;

namespace AdminUI.Models
{
    public class UserGroupModel
    {
        public List<UserGroup> UserGroups { get; set; }
        public UserGroup UserGroup { get; set; }
        public ResultModel Result { get; set; }

    }
}
