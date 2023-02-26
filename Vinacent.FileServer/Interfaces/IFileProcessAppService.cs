using Vinacent.FileServer.Data.Dto;
using Vinacent.FileServer.Data.Models;

namespace Vinacent.FileServer.Interfaces
{
    public interface IFileProcessAppService
    {
        Task<RootProject> CreateRootProject(RootProject input);
        Task<RootProject> UpdateRootProject(RootProject input);
        Task<FileDownloadDto> Upload(FileUploadDto input);
        Task<FileDownloadDto> UpdateDetail(FileDetailUpdateDto input);
        Task<FileItem> GetFileItem(Guid fileId);
        Task<FileDownloadDto> FileRemove(Guid fileId);
    }
}
