using System.Collections.Generic;
using Bogus;
using Kpi.YourDomain.ClientTests.Model.Domain.Login;

namespace Kpi.YourDomain.ClientTests.TestsData.Storage
{
    public static class UsersStorage
    {
        public static Dictionary<string, UserInformation> Users => 
            new Dictionary<string, UserInformation>
            {
                { "ExistingUser", ExistingUser }
            };

        private static UserInformation ExistingUser => 
            new Faker<UserInformation>()
                .RuleFor(u => u.Email, "dummy@rambler.ru")
                .RuleFor(u => u.Password, "dummy");
    }
}
