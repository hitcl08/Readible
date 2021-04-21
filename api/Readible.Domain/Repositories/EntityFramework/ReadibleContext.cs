using Microsoft.EntityFrameworkCore;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System.Data.Entity.Infrastructure;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class ReadibleContext : DbContext
    {
        public ReadibleContext(DbContextOptions<ReadibleContext> options) : base(options) { }

        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<SubscriptionViewModel> Subscriptions { get; set; }
        public DbSet<BookViewModel> Books { get; set; }
        public DbSet<SubscriptionBookViewModel> SubscriptionBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionBookViewModel>()
                .HasKey(sb => new { sb.BookId, sb.SubscriptionId });

            modelBuilder.Entity<SubscriptionBookViewModel>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.SubscriptionBooks)
                .HasForeignKey(sb => sb.BookId);

            modelBuilder.Entity<SubscriptionBookViewModel>()
                .HasOne(sb => sb.Subscription)
                .WithMany(s => s.SubscriptionBooks)
                .HasForeignKey(sb => sb.SubscriptionId);

            modelBuilder.Entity<UserViewModel>()
                .HasOne(e => e.Subscription)
                .WithOne(e => e.User)
                .HasForeignKey<SubscriptionViewModel>(s => s.UserId);
        }
    }
}
