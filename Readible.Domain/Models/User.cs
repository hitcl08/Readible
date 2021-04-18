using System.ComponentModel.DataAnnotations;

namespace Readible.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? SubscriptionId { get; set; }
    }
}
