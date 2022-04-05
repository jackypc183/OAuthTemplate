using Template.Dal;

namespace Template.Service.Users
{
    public class UserService: IUserService
    {
        IUserRepository _userRep;
        public UserService(IUserRepository userRep)
        {
            _userRep = userRep;
        }

        public bool DeleteUser(UserDto userDto)
        {
            return _userRep.DeleteUser(userDto);
        }

        public IEnumerable<UserDto> GetList()
        {
            return _userRep.GetList();
        }

        public bool InsterUser(UserDto userDto)
        {
            return _userRep.InsterUser(userDto);
        }

        public bool UpdateUser(UserDto userDto)
        {
            return _userRep.UpdateUser(userDto);
        }
    }
}
