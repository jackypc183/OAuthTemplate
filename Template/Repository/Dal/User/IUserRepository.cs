namespace Template.Dal
{
    public interface IUserRepository
    {
        public List<UserDto> GetList();
        public bool InsterUser(UserDto userDto);
        public bool UpdateUser(UserDto userDto);
        public bool DeleteUser(UserDto userDto);
    }
}
