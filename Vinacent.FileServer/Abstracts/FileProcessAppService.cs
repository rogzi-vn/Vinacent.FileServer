using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vinacent.FileServer.Controllers;
using Vinacent.FileServer.Data;
using Vinacent.FileServer.Data.Dto;
using Vinacent.FileServer.Data.Models;
using Vinacent.FileServer.Interfaces;

namespace Vinacent.FileServer.Abstracts
{
    public class FileProcessAppService : IFileProcessAppService
    {
        private readonly FileServerDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileProcessAppService> _logger;

        public FileProcessAppService(FileServerDbContext dbContext, IWebHostEnvironment webHostEnvironment, ILogger<FileProcessAppService> logger)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        private string GetTodayFileFolder()
        {
            var storagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "DataLakeOfFiles", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            try
            {
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Try to create [{storagePath}] faild!", ex);
                throw;
            }

            return storagePath;
        }

        private async Task<FileItem> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("Id null");
            }

            var fileItem = await _dbContext.FileItems.FirstOrDefaultAsync(x => x.Id == id);

            if (fileItem == null)
            {
                throw new Exception("Not found");
            }

            return fileItem;
        }

        private async Task<FileDownloadDto> GetFileDownload(Guid fileId)
        {
            var fileItem = await Get(fileId);

            return GetFileDownload(fileItem);
        }

        private FileDownloadDto GetFileDownload(FileItem input)
        {
            return new FileDownloadDto
            {
                FileItemId = input.Id,
                RootProjectId = input.RootProjectId
            };
        }

        public async Task<RootProject> SyncRootProject(RootProject input)
        {
            if (input.Id == Guid.Empty)
            {
                throw new Exception("Please give unique Id!!");
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                throw new Exception("Please give project name!!");
            }

            var entity = await _dbContext.RootProjects.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                // Create
                var previous = await _dbContext.RootProjects.FirstOrDefaultAsync(x => x.Name.ToLower() == input.Name.ToLower());
                if (previous != null)
                {
                    throw new Exception("Project with that name was existed!");
                }

                input = new RootProject
                {
                    Id = input.Id,
                    Name = input.Name,
                    Description = input.Description,
                    CreationTime = DateTime.Now
                };

                _dbContext.RootProjects.Add(input);
            }
            else
            {
                // Update
                var previous = await _dbContext.RootProjects.FirstOrDefaultAsync(x => x.Id != entity.Id && x.Name.ToLower() == entity.Name.ToLower());
                if (previous != null)
                {
                    throw new Exception("Project with that name was existed!");
                }

                entity.Name = input.Name;
                entity.Description = input.Description;
                entity.ModificationTime = DateTime.Now;

                _dbContext.RootProjects.Update(entity);
            }

            await _dbContext.SaveChangesAsync();
            return input;
        }


        public async Task<FileDownloadDto> FileRemove(Guid fileId)
        {

            var item = await Get(fileId);
            var dto = GetFileDownload(item);

            _dbContext.FileItems.Remove(item);
            await _dbContext.SaveChangesAsync();

            return dto;

        }

        public async Task<FileItem> GetFileItem(Guid fileId)
        {
            var result = await Get(fileId);
            if (File.Exists(result.PhysicalServerPath))
            {
                return result;
            }

            await FileRemove(fileId);
            throw new Exception("File not found!");
        }

        public async Task<FileDownloadDto> UpdateDetail(FileDetailUpdateDto input)
        {
            var fi = await Get(input.FileItemId);
            fi.FileName = input.FileName;
            fi.Description = input.Description;

            _dbContext.FileItems.Update(fi);
            await _dbContext.SaveChangesAsync();

            return GetFileDownload(fi);
        }

        public async Task<FileDownloadDto> Upload(FileUploadDto input)
        {
            var targetFolder = GetTodayFileFolder();
            var serverFilePath = Path.Combine(targetFolder, Guid.NewGuid().ToString());
            var fileItem = new FileItem
            {
                RootProjectId = input.RootProjectId,
                PhysicalServerPath = serverFilePath,
                FileName = input.FileName,
                Description = input.Description,
                CreatorId = input.CreatorId,
                ExtensionJsonData = input.ExtensionJsonData,
                CreationTime = DateTime.Now,
            };

            try
            {
                using var fs = File.Create(serverFilePath);
                await input.File.CopyToAsync(fs);
                fs.Close();

                _dbContext.FileItems.Add(fileItem);
                await _dbContext.SaveChangesAsync();
            } catch(Exception ex)
            {
                _logger.LogError($"Try to save file [{serverFilePath}]", ex);
                throw;
            }

            return GetFileDownload(fileItem);
        }

    }
}
