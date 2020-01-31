using System.Collections.Generic;
using SimpleEvenBus.Abstractions.Events;

using MyBlog.Location.Api.Models;

namespace MyBlog.Location.Api.Infrastructure.Events
{
    public class UserLocationUpdatedEvent : Event
    {
        public string UserId { get; set; }
        public IEnumerable<UserLocationDetails> Locations { get; set; }

        public UserLocationUpdatedEvent() { }

        public UserLocationUpdatedEvent(string userId, IEnumerable<UserLocationDetails> locations)
        {
            UserId = userId;
            Locations = locations;
        }
    }
}
