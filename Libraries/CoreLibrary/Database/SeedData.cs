using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Models;
using static CoreLibrary.Database.MockData;

namespace CoreLibrary.Database
{
    public static class SeedData
    {   
        public static readonly List<User> Users = GetUsers(2).ToList();
    }
}
