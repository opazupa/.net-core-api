using FeatureLibrary.Models.Entities;
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
            ON_SKILL_UPDATE_BY_LEVEL = "onSkillUpdateByLevel",
            ON_SKILL_UPDATE_BY_NAME = "onSkillUpdateByName";

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
        /// Event on skill add or update
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CodingSkillEntity OnSkillUpdateByLevel(CodingSkillLevel level, EventMessage message)
        {
            return (CodingSkillEntity)message.Payload;
        }

        /// <summary>
        /// Event on skill add or update
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public CodingSkillEntity OnSkillUpdateByName(string name, EventMessage message)
        {
            return (CodingSkillEntity)message.Payload;
        }
    }
}
