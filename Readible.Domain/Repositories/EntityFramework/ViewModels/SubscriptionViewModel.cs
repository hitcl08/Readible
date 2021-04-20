using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public int UserId { get; internal set; }
        public UserViewModel User { get; set; }
        public ICollection<SubscriptionBookViewModel> SubscriptionBooks { get; set; }
    }
}
