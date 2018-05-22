using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestBin.Common.Models;
using RestBin.WebServer.EF;

namespace RestBin.WebServer.Rest.Migrations
{
    public class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            //config
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            //interception
            DbInterception.Add(new EFInterceptor());
        }

        protected override void Seed(AppDbContext context)
        {
             
        }
    }

}
