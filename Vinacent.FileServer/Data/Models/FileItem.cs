using System.ComponentModel.DataAnnotations.Schema;
using Vinacent.FileServer.Abstracts.Data;

namespace Vinacent.FileServer.Data.Models
{
    public class FileItem : AuditedEntity<Guid>
    {
        public Guid RootProjectId { get; set; }

        public string PhysicalServerPath { get; set; }

        public string FileName { get; set; }
        public string? Description { get; set; }
        public string? CreatorId { get; set; }
        public string? LastUpdaterId { get; set; }
        public string? ExtensionJsonData { get; set; }
        public bool? IsDeleted { get; set; } = false;

        [ForeignKey(nameof(RootProjectId))]
        public virtual RootProject RootProject { get; set; }

        public virtual ICollection<FileItem> FileItems { get; set;}

    }
}
