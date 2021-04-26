namespace Readible.Domain.Repositories.EntityFramework.ViewModels
{
    public class SubscriptionBookViewModel
    {
        public int BookId { get; set; }
        public BookViewModel Book { get; set; }
        public int SubscriptionId { get; set; }
        public SubscriptionViewModel Subscription { get; set; }
    }
}
