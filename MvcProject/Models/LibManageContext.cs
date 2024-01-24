using Microsoft.EntityFrameworkCore;
using MvcProject.Models;

namespace MvcProject.Models
{
    public class LibManageContext : DbContext
    {
        public LibManageContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<LibManage> LibManages { get; set; }
        public DbSet<MvcProject.Models.TLib> TLib { get; set; } = default!;

    }
}
