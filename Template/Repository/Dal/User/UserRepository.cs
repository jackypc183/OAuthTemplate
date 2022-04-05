using Template.Models.Db;
using Template.Repository;

namespace Template.Dal
{
    public class UserRepository : IUserRepository
    {
        private IUnitOfWork _unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool DeleteUser(UserDto userDto)
        {
            UserDto r = userDto;
            var Repo = _unitOfWork.Repository<Users>();
            try
            {
                Repo.Delete(new Users
                {
                    id = r.id,
                    account = r.account,
                    password = r.password,
                    userName = r.userName,
                    lineToken = r.lineToken,
                    lineUserId = r.lineUserId
                });
                Repo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<UserDto> GetList()
        {
            var Repo = _unitOfWork.Repository<Users>();
            var data = from r in Repo.Reads()
                       select new UserDto()
                       {
                           id = r.id,
                           account = r.account,
                           password = r.password,
                           userName = r.userName,
                           lineToken = r.lineToken,
                           lineUserId = r.lineUserId
                       };
            return data.ToList();
        }

        public bool InsterUser(UserDto userDto)
        {
            UserDto r =  userDto;
            var Repo = _unitOfWork.Repository<Users>();
            try
            {
                Users users = new Users()
                {
                    id = r.id,
                    account = r.account,
                    password = r.password,
                    userName = r.userName,
                    lineToken = r.lineToken,
                    lineUserId = r.lineUserId,
                };
                Repo.Create(users);
                Repo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUser(UserDto userDto)
        {
            UserDto r = userDto;
            var Repo = _unitOfWork.Repository<Users>();
            try
            {
                var data = Repo.Reads().Where(x => x.id == userDto.id).FirstOrDefault();
                data.account = r.account;
                data.password = r.password;
                data.userName = r.userName;
                data.lineToken = r.lineToken;
                data.lineUserId = r.lineUserId;
                Repo.Update(data);
                Repo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
