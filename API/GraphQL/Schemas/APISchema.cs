﻿using GraphQL;
using GraphQL.Types;

namespace API.GraphQL
{
    /// <summary>
    /// GraphQL schema for the API
    /// </summary>
    public class APISchema : Schema
    {
        public APISchema(IDependencyResolver resolver) : base (resolver)
        {
            Query = resolver.Resolve<APIQuery>();
            //Mutation = resolver.Resolve<APIMutation>();
        }
    }
}
    