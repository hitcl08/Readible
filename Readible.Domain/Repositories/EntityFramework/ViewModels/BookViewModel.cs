using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int? SubscriptionId { get; set; }
    }
}
