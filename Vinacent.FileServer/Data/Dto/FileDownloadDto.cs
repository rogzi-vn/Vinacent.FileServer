namespace Vinacent.FileServer.Data.Dto
{
    public class FileDownloadDto
    {
        public Guid RootProjectId { get; set; }

        public Guid? FileItemId { get; set; }

    }
}
