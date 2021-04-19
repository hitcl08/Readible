using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public int? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
