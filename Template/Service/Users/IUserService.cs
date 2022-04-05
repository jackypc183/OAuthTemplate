namespace Template.Service.Users
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetList();
        public bool InsterUser(UserDto userDto);
        public bool UpdateUser(UserDto userDto);
        public bool DeleteUser(UserDto userDto);
    }
}