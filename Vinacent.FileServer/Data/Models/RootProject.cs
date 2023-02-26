using Vinacent.FileServer.Abstracts.Data;

namespace Vinacent.FileServer.Data.Models
{
    public class RootProject: AuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
