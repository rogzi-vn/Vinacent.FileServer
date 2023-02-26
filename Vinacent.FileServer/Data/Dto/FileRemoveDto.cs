namespace Vinacent.FileServer.Data.Dto
{
    public class FileRemoveDto
    {
        public Guid RootProjectId { get; set; }

        public Guid? FileItemId { get; set; }

    }
}
