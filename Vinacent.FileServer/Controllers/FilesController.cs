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

        [HttpPost("/api/root-project")]
        public async Task<RootProject> CreateOrUpdateRootProject(RootProject input)
        {
            return await _fileProcessAppService.SyncRootProject(input);
        }

        [HttpGet("/files/{id:guid}")]
        public async Task<IActionResult> PublicDownload(Guid id)
        {
            try
            {
                var fileItem = await _fileProcessAppService.GetFileItem(id);
                await SaveLog(id);
                try
                {
                    return File(System.IO.File.OpenRead(fileItem.PhysicalServerPath), "application/octet-stream", fileItem.FileName);
                } catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                return NotFound();
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> PrivateUpload([FromForm] FileUploadDto dto)
        {
            try
            {
                var result = await _fileProcessAppService.Upload(dto);
                if (result != null && result.FileItemId != Guid.Empty)
                {
                    await SaveLog(result!.FileItemId!.Value);
                    return new JsonResult(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                Response.StatusCode = 500;
                return new JsonResult(ex.Message);
            }

            Response.StatusCode = 500;
            return new EmptyResult();
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
                _logger.LogError(ex.StackTrace, ex);
                return new FileDownloadDto();
            }
        }

        [HttpDelete("remove/{id:guid}")]
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
                _logger.LogError(ex.StackTrace, ex);
                return new FileDownloadDto();
            }
        }

    }
}