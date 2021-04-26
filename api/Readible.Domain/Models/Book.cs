using System.Collections.Generic;

namespace Readible.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
