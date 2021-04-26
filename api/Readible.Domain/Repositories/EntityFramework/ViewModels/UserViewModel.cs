namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SubscriptionViewModel Subscription { get; set; }
    }
}
