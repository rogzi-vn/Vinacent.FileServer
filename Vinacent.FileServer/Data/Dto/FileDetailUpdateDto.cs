namespace Vinacent.FileServer.Data.Dto
{
    public class FileDetailUpdateDto
    {
        public Guid FileItemId { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
    }
}
