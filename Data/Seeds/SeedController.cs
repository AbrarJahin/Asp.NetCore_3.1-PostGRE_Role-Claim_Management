using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Seeds
{
    public class SeedController:IDisposable
    {
        private readonly ModelBuilder ModelBuilder;
        public SeedController(ModelBuilder builder)
        {
            ModelBuilder = builder;
        }
        public void Dispose()
        {
            //Need to do anything for disposing the variables, currently it is not needed
        }

        internal void Execute()
        {
            ICollection<Guid> superAdminIdList = UserSeeder.Execute(ModelBuilder);
            IList<Guid> roleIdList = RoleSeeder.Execute(ModelBuilder, superAdminIdList);
            ClaimSeeder.Execute(ModelBuilder, superAdminIdList); //ClaimSeeder is unnecessery as it is never used in the project
            RoleClaimSeeder.Execute(ModelBuilder, roleIdList);
        }
    }
}
