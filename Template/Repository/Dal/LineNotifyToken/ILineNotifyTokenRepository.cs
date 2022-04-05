using Template.Models.LineNotifyToken;

namespace Template.Repository.Dal.LineNotifyToken
{
    public interface ILineNotifyTokenRepository
    {
        public IEnumerable<LineNotifyTokenDto> GetList();
        public bool Inster(LineNotifyTokenDto lineNotifyTokenDto);
        public bool Update(LineNotifyTokenDto lineNotifyTokenDto);
        public bool Delete(Guid id);
    }
}
