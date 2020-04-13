using FeatureLibrary.Models.Entities;
using HotChocolate.Language;
using HotChocolate.Subscriptions;

namespace API.GraphQL.Types.Events
{
    /// <summary>
    /// Coding Skill Event
    /// </summary>
    public class CodingSkillEvent : EventMessage
    {
        #region constructors
        /*
        * Constructors
        */
        public CodingSkillEvent(string eventName, CodingSkillLevel level, CodingSkillEntity skill)    
            : base(CreateEventDescription(eventName, level), skill) {  }

        public CodingSkillEvent(string eventName, string name, CodingSkillEntity skill)    
            : base(CreateEventDescription(eventName, name), skill) {  }

        public CodingSkillEvent(string eventName, long userId, CodingSkillEntity skill)    
            : base(CreateEventDescription(eventName, userId), skill) {  }


        #endregion

        #region event descriptions
        /*
        * Event descriptions
        */        
        private static EventDescription CreateEventDescription(string eventName, CodingSkillLevel level)
        {
            return new EventDescription(eventName,
                new ArgumentNode(nameof(level),
                    new EnumValueNode(level)));
        }
        private static EventDescription CreateEventDescription(string eventName, string name)
        {
            return new EventDescription(eventName,
                new ArgumentNode(nameof(name),
                    new StringValueNode(name)));
        }
        private static EventDescription CreateEventDescription(string eventName, long userId)
        {
            return new EventDescription(eventName,
                new ArgumentNode(nameof(userId),
                    new IntValueNode(userId)));
        }
        #endregion
    }
}
