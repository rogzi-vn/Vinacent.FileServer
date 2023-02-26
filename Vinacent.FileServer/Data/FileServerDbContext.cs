using Microsoft.EntityFrameworkCore;
using Vinacent.FileServer.Data.Models;

namespace Vinacent.FileServer.Data
{
    public class FileServerDbContext : DbContext
    {
        public FileServerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RootProject> RootProjects { get; set; }
        public DbSet<FileItem> FileItems { get; set; }
        public DbSet<FileItemLog> FileItemLogs { get; set; }
    }
}
