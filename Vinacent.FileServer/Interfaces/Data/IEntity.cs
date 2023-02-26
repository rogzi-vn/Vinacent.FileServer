namespace Vinacent.FileServer.Interfaces.Data
{
    public interface IEntity : IEntity<long>
    {
    }

    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IAuditedEntity : IAuditedEntity<long> { 

    }

    public interface IAuditedEntity<TKey> : IEntity<TKey>, IHasCreationTime, IHasModificationTime
    {

    }
}
