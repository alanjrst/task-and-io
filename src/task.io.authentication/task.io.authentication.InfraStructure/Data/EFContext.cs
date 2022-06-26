using System;
using Microsoft.EntityFrameworkCore;
using task.io.authentication.Domain.Entities.Users;

namespace task.io.authentication.InfraStructure.Data
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => p.DeletedDate == new DateTime(0001, 01, 01, 0, 0, 0));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if(!optionsBuilder.IsConfigured){
                var mysql = "Server=127.0.0.1;Database=Authentication;Uid=root;Pwd=@T9x1p4e5";
                optionsBuilder.UseMySql(mysql,ServerVersion.AutoDetect(mysql));
            }
        }
    }
}