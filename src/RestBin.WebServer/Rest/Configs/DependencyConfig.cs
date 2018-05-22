using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.Unity;
using RestBin.Common.Models;
using RestBin.WebServer.EF;
using RestBin.WebServer.Rest.IOC;

namespace RestBin.WebServer.Rest.Configs
{
    public static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            //repos
            container.RegisterType<IEntityRepository<HeaderModel>, EntityRepository<HeaderModel>>(new HierarchicalLifetimeManager());
            container.RegisterType<IEntityRepository<TradeRecordModel>, EntityRepository<TradeRecordModel>>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);
        }

    }
}
