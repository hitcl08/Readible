using Microsoft.EntityFrameworkCore;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System.Data.Entity.Infrastructure;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class ReadibleContext : DbContext
    {
        public ReadibleContext(DbContextOptions<ReadibleContext> options) : base(options) { }

        public DbQuery<UserViewModel> UserViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserViewModel>(entity => {
                entity.ToTable("User").HasKey(x => x.UserId);

                entity.Property(x => x.UserId);

                entity.Property(x => x.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(x => x.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(x => x.SubscriptionId);
            });
        }
    }
}
