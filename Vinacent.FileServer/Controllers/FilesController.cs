using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vinacent.FileServer.Data;
using Vinacent.FileServer.Data.Dto;
using Vinacent.FileServer.Data.Models;
using Vinacent.FileServer.Interfaces;

namespace Vinacent.FileServer.Controllers
{
    [ApiController]
    [Route("/api/files")]
    public class FilesController : ControllerBase
    {

        private readonly ILogger<FilesController> _logger;
        private readonly IFileProcessAppService _fileProcessAppService;
        private readonly FileServerDbContext _dbContext;

        public FilesController(ILogger<FilesController> logger, IFileProcessAppService fileProcessAppService, FileServerDbContext dbContext)
        {
            _logger = logger;
            _fileProcessAppService = fileProcessAppService;
            _dbContext = dbContext;
        }

        private async Task SaveLog(Guid fileItemId)
        {
            var fileItemLog = new FileItemLog
            {
                FileItemId = fileItemId,
                Action = Request.RouteValues["action"]?.ToString() ?? "",
                Url = Request.Path,
                IpAddress = (Request.HttpContext.Connection.RemoteIpAddress ?? Request.HttpContext.Connection.LocalIpAddress)?.ToString() ?? "::1",
                BrowserInfo = JsonConvert.SerializeObject(Request.HttpContext.Request.Headers),
                CreationTime = DateTime.Now

            };
            _dbContext.FileItemLogs.Add(fileItemLog);
            await _dbContext.SaveChangesAsync();
        }

        [HttpPost("/root-project")]
        public async Task<RootProject> CreateOrUpdateRootProject(RootProject input)
        {
            if (input.Id == Guid.Empty)
            {
                input = await _fileProcessAppService.CreateRootProject(input);
            }
            else
            {
                input = await _fileProcessAppService.UpdateRootProject(input);
            }

            return input;
        }

        [HttpGet("/files/{id:guid}")]
        public async Task<IActionResult> PublicDownload(Guid id)
        {
            try
            {
                var fileItem = await _fileProcessAppService.GetFileItem(id);
                await SaveLog(id);
                return File(System.IO.File.OpenRead(fileItem.PhysicalServerPath), "application/octet-stream", fileItem.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(PublicDownload), ex);
                return NotFound();
            }
        }

        [HttpPost("upload")]
        public async Task<FileDownloadDto> PrivateUpload([FromForm] FileUploadDto dto)
        {
            try
            {
                var result = await _fileProcessAppService.Upload(dto);
                if (result != null && result.FileItemId != Guid.Empty)
                {
                    await SaveLog(result!.FileItemId!.Value);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(PublicDownload), ex);
            }
            return new FileDownloadDto();
        }

        [HttpPost("update-detail")]
        public async Task<FileDownloadDto> PrivateUpdateDetial(FileDetailUpdateDto dto)
        {
            try
            {
                var result = await _fileProcessAppService.UpdateDetail(dto);
                await SaveLog(dto.FileItemId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(PublicDownload), ex);
                return new FileDownloadDto();
            }
        }

        [HttpDelete("remove")]
        public async Task<FileDownloadDto> PrivateRemove(Guid id)
        {
            try
            {
                var result = await _fileProcessAppService.FileRemove(id);
                await SaveLog(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(PublicDownload), ex);
                return new FileDownloadDto();
            }
        }

    }
}