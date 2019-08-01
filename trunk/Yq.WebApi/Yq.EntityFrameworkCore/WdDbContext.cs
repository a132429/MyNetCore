using Yq.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Yq.EntityFrameworkCore
{
    public class WdDbContext: DbContext
    {
        public WdDbContext(DbContextOptions<WdDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
