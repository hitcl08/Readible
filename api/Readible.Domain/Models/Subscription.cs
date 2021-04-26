using System.Collections.Generic;

namespace Readible.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}
