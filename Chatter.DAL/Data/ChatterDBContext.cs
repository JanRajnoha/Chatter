using Chatter.DAL.Model;
using Chatter.DAL.Model.ManyToManyTables;
using Microsoft.EntityFrameworkCore;

namespace Chatter.DAL.Data
{
    public class ChatterDBContext : DbContext 
    {
        public ChatterDBContext()
        {

        }

        public ChatterDBContext(DbContextOptions<ChatterDBContext> options) : base(options)
        {

        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = tcp:chatter.database.windows.net, 1433; Initial Catalog = ChatterDB; Persist Security Info = False; User ID = JR; Password =ChatterDB1; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin_Group>().HasKey(ag => new { ag.AdminRefId, ag.GroupRefId });
            modelBuilder.Entity<Admin_Group>().HasOne(ag => ag.Admin).WithMany(a => a.AdminGroups).HasForeignKey(ag => ag.AdminRefId);
            modelBuilder.Entity<Admin_Group>().HasOne(ag => ag.Group).WithMany(g => g.Admins).HasForeignKey(ag => ag.GroupRefId);

            modelBuilder.Entity<Admin_Organization>().HasKey(ao => new { ao.AdminRefId, ao.OrganizationRefId });
            modelBuilder.Entity<Admin_Organization>().HasOne(ao => ao.Admin).WithMany(a => a.AdminOrganizations).HasForeignKey(ag => ag.AdminRefId);
            modelBuilder.Entity<Admin_Organization>().HasOne(ao => ao.Organization).WithMany(o => o.Admins).HasForeignKey(ao => ao.OrganizationRefId);

            modelBuilder.Entity<User_Group>().HasKey(ug => new { ug.UserRefId, ug.GroupRefId });
            modelBuilder.Entity<User_Group>().HasOne(ug => ug.Group).WithMany(g => g.Users).HasForeignKey(au => au.GroupRefId);
            modelBuilder.Entity<User_Group>().HasOne(ug => ug.User).WithMany(u => u.Groups).HasForeignKey(au => au.UserRefId);

            modelBuilder.Entity<User_Organization>().HasKey(uo => new { uo.UserRefId, uo.OrganizationRefId });
            modelBuilder.Entity<User_Organization>().HasOne(uo => uo.Organization).WithMany(o => o.Users).HasForeignKey(ao => ao.OrganizationRefId);
            modelBuilder.Entity<User_Organization>().HasOne(uo => uo.User).WithMany(u => u.Organizations).HasForeignKey(ao => ao.UserRefId);
        }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<SignInLog> SignInLogs { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Admin_Group> Admin_Group { get; set; }

        public DbSet<Admin_Organization> Admin_Organization { get; set; }

        public DbSet<User_Group> User_Group { get; set; }

        public DbSet<User_Organization> User_Organization { get; set; }
    }
}