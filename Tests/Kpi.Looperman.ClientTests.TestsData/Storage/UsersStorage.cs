using System;
using System.Collections.Generic;
using Bogus;
using Kpi.Looperman.ClientTests.Model.Domain.Login;

namespace Kpi.Looperman.ClientTests.TestsData.Storage
{
    public static class UsersStorage
    {
        public static Dictionary<string, UserInformation> Users => 
            new Dictionary<string, UserInformation>
            {
                { "ExistingUser", ExistingUser },
                { "InvalidUser", InvalidUser }
            };

        private static UserInformation ExistingUser => 
            new Faker<UserInformation>()
                .RuleFor(u => u.Email, "test5222@ukr.net")
                .RuleFor(u => u.Password, "qwertyuiop12");

        private static UserInformation InvalidUser =>
            new Faker<UserInformation>()
                .RuleFor(u => u.Email, Guid.NewGuid().ToString)
                .RuleFor(u => u.Password, Guid.NewGuid().ToString);
    }
}
