using System.Collections.Generic;
using MMW.Website.Api.MsGraphApi.Core;

namespace MMW.Website.Api.MsGraphApi.Users
{
    public partial class User
    {
        public static User ById(string id)
        {
            var ret =
                ApiConnector.Get<User>($"v1.0/users/{id}");
            return ret.Content;
        }

        public static List<User> All()
        {
            var ret =
                ApiConnector.Get<List<User>>("v1.0/users", "value");
            return ret.Content;
        }
    }
}