using Template.Models.LineNotifyToken;
using Template.Models.Db;

namespace Template.Repository.Dal.LineNotifyToken
{
    public class LineNotifyTokenRepository : ILineNotifyTokenRepository
    {
        private IUnitOfWork _unitOfWork;
        public LineNotifyTokenRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete(Guid id)
        {
            var Repo = _unitOfWork.Repository<Template.Models.Db.LineNotifyToken>();
            try
            {
                var data = Repo.Reads().Where(x => x.id == id).FirstOrDefault();
                if (data != null)
                {
                    Repo.Delete(data);
                    Repo.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<LineNotifyTokenDto> GetList()
        {
            var Repo = _unitOfWork.Repository<Template.Models.Db.LineNotifyToken>();
            var data = from r in Repo.Reads()
                       select new LineNotifyTokenDto()
                       {
                           id = r.id,
                           lineUserId = r.lineUserId,
                           lineNotifyToken = r.lineNotifyToken
                       };
            return data.ToList();
        }

        public bool Inster(LineNotifyTokenDto lineNotifyTokenDto)
        {
            LineNotifyTokenDto r = lineNotifyTokenDto;
            var Repo = _unitOfWork.Repository<Template.Models.Db.LineNotifyToken>();
            try
            {
                Template.Models.Db.LineNotifyToken users = new Template.Models.Db.LineNotifyToken()
                {
                    id = r.id,
                    lineUserId = r.lineUserId,
                    lineNotifyToken = r.lineNotifyToken
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

        public bool Update(LineNotifyTokenDto lineNotifyTokenDto)
        {
            LineNotifyTokenDto r = lineNotifyTokenDto;
            var Repo = _unitOfWork.Repository<Template.Models.Db.LineNotifyToken>();
            try
            {
                var data = Repo.Reads().Where(x => x.id == lineNotifyTokenDto.id).FirstOrDefault();
                data.lineUserId = r.lineUserId;
                data.lineNotifyToken = r.lineNotifyToken;
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
