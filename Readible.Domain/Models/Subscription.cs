using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Book> Books { get; set; }
    }
}
