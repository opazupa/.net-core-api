using HotChocolate.Subscriptions;

namespace API.GraphQL.Types.Events
{
    /// <summary>
    /// User Event
    /// </summary>
    public class UserEvent : EventMessage
    {
        public UserEvent(string eventName, string userName)    
            : base(new EventDescription(eventName), userName) {  }
    }
}
