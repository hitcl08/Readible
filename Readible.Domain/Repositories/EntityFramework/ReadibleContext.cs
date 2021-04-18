using Microsoft.EntityFrameworkCore;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System.Data.Entity.Infrastructure;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class ReadibleContext : DbContext
    {
        public ReadibleContext(DbContextOptions<ReadibleContext> options) : base(options) { }

        public DbSet<UserViewModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(entity => {
            //    entity.ToTable("User").HasKey(x => x.Id);

            //    entity.Property(x => x.Id);

            //    entity.Property(x => x.Username)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(x => x.Password)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(x => x.SubscriptionId);
            //});
        }
    }
}
