using Vinacent.FileServer.Interfaces.Data;

namespace Vinacent.FileServer.Abstracts.Data
{
    public abstract class Entity : IEntity
    {
        public long Id { get; set; }
    }

    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class AuditedEntity : AuditedEntity<long> { 

    }

    public abstract class AuditedEntity<TKey> : IAuditedEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}
