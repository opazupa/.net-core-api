using HotChocolate.Types;

namespace API.GraphQL.Types.Inputs
{
   /// <summary>
   /// Authentication input type
   /// </summary>
   public class AuthenticationInputType : InputObjectType
   {
        protected override void Configure(IInputObjectTypeDescriptor descriptor)
       {
           Name = "AuthenticationInput";

           descriptor.Field("UserName")
                .Type<NonNullType<StringType>>()
                .Description("UseNname");
                
            descriptor.Field("Password")
                .Type<NonNullType<StringType>>()
                .Description("Password");
       }
   }
}
