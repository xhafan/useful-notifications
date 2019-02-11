using System.Reflection;
using CoreDdd.Nhibernate.Configurations;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Dtos.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Infrastructure
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
                typeof(Location).Assembly,
                typeof(LocationFilmDto).Assembly,
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