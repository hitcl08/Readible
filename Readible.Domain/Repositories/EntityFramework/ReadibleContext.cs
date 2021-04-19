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
        public DbSet<SubscriptionViewModel> Subscription { get; set; }
        public DbSet<BookViewModel> Book { get; set; }
        public DbSet<BookDetailsViewModel> BookDetails { get; set; }
    }
}
