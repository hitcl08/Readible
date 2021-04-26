using System.Runtime.Serialization;

namespace Readible.ServiceModel.Dtos
{
    [DataContract(Name = "user")]
    public class UserDto
    {
        [DataMember(Name = "id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "username", Order = 2)]
        public string Username { get; set; }
        [DataMember(Name = "password", Order = 3)]
        public string Password { get; set; }
        [DataMember(Name = "subscriptionId", Order = 4)]
        public int SubscriptionId { get; set; }
    }
}
