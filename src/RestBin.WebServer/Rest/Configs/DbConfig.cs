using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestBin.WebServer.EF;
using RestBin.WebServer.Rest.Migrations;

namespace RestBin.WebServer.Rest.Configs
{
    public static class DbConfig
    {
        public static void Migrate()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }
    }
}
