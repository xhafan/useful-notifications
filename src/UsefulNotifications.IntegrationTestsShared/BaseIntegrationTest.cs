using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using UsefulNotifications.Infrastructure;

namespace UsefulNotifications.IntegrationTestsShared
{
    public abstract class BaseIntegrationTest
    {
        protected NhibernateUnitOfWork UnitOfWork;
        private static readonly NhibernateConfigurator NhibernateConfigurator = new NhibernateConfigurator();

        [SetUp]
        public void BaseSetUp()
        {
            UnitOfWork = new NhibernateUnitOfWork(NhibernateConfigurator);
            UnitOfWork.BeginTransaction();
        }

        [TearDown]
        public void BaseTearDown()
        {
            UnitOfWork.Commit();
        }
    }
}