using Vinacent.FileServer.Abstracts.Data;
using Vinacent.FileServer.Interfaces.Data;

namespace Vinacent.FileServer.Data.Models
{
    public class FileItemLog : Entity<Guid>, IHasCreationTime
    {
        public Guid FileItemId { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string IpAddress { get; set; }
        public string BrowserInfo { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
