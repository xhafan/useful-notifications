using System.Reflection;
using CoreDdd.Nhibernate.Configurations;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Domain
{
    public class NhibernateConfigurator : BaseNhibernateConfigurator
    {
        public NhibernateConfigurator(
            bool shouldMapDtos = true,
            string configurationFileName = null
        )
            : base(shouldMapDtos, configurationFileName)
        {
#if DEBUG || REPOLINKS_DEBUG
            NHibernateProfiler.Initialize();
#endif
        }

        protected override Assembly[] GetAssembliesToMap()
        {
            return new[]
            {
                typeof(Location).Assembly
            };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
#if DEBUG || REPOLINKS_DEBUG
                NHibernateProfiler.Shutdown();
#endif
            }
            base.Dispose(disposing);
        }
    }

}