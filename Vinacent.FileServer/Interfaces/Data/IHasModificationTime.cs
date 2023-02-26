namespace Vinacent.FileServer.Interfaces.Data
{
    public interface IHasModificationTime
    {
        DateTime? ModificationTime { get; set; }
    }
}
