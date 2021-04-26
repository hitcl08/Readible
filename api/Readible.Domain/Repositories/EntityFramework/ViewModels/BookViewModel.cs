using System.Collections.Generic;

namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ICollection<SubscriptionBookViewModel> SubscriptionBooks { get; set; }
    }
}
