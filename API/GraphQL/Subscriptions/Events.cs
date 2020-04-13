using FeatureLibrary.Models.Entities;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace API.GraphQL.Subscriptions
{
    /// <summary>
    /// Events
    /// </summary>
    public class Events
    {
        public readonly static string 
            ON_USER_REGISTER = "onUserRegister",
            ON_USER_LOGIN = "onUserLogin",
            ON_SKILL_BY_LEVEL = "onSkillByLevel",
            ON_SKILL_BY_NAME = "onSkillByName",
            ON_SKILL_BY_USER = "onSkillByUser";

        /// <summary>
        /// Event on user login
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string OnUserLogin(EventMessage message)
        {
            return (string)message.Payload;
        }

        /// <summary>
        /// Event on user register
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string OnUserRegister(EventMessage message)
        {
            return (string)message.Payload;
        }

        /// <summary>
        /// Event on skills by level
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CodingSkillEntity OnSkillByLevel(CodingSkillLevel level, EventMessage message)
        {
            return (CodingSkillEntity)message.Payload;
        }

        /// <summary>
        /// Event on skills by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CodingSkillEntity OnSkillByName(string name, EventMessage message)
        {
            return (CodingSkillEntity)message.Payload;
        }

        /// <summary>
        /// Event on skills by user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CodingSkillEntity OnSkillByUser(long userId, EventMessage message)
        {
            return (CodingSkillEntity)message.Payload;
        }
    }
}
