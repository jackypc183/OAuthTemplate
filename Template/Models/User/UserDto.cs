using System.ComponentModel.DataAnnotations;

namespace Template
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public string? lineUserId { get; set; }
        public string? lineToken { get; set; }
        public string? lineNotifyToken { get; set; }

    }
}