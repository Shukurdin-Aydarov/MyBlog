using System.ComponentModel.DataAnnotations;

namespace MyBlog.Location.Api.Infrastructure.Settings
{
    public class EventBusSettings
    {
        [Required]
        public string Host { get; set; }

        [Required]
        public string Queue { get; set; }

        [Required]
        public string Exchange { get; set; }
        
        public int? RetryCount { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
