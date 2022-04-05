using System.ComponentModel.DataAnnotations;

namespace Template.Models.Db
{
    public class LineNotifyToken
    {
        [Key]
        public Guid id { get; set; }
        public string lineUserId { get; set; }
        public string lineNotifyToken { get; set; }
    }
}
