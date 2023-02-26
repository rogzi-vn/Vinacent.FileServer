namespace Vinacent.FileServer.Data.Dto
{
    public class FileUploadDto
    {
        public Guid RootProjectId { get; set; }

        public string FileName => File.FileName;
        public string? Description { get; set; }
        public string? CreatorId { get; set; }
        public string? LastUpdaterId { get; set; }
        public string? ExtensionJsonData { get; set; }
        public IFormFile File { get; set; }

    }
}
