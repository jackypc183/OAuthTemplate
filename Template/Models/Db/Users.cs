using System.ComponentModel.DataAnnotations;

namespace Template.Models.Db
{
    public class Users
    {
        [Key]
        public Guid id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public string? lineUserId { get; set; }
        public string? lineToken { get; set; }

    }
}