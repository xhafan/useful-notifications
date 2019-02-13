using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;
using UsefulNotifications.Infrastructure;

namespace UsefulNotifications.IntegrationTests
{
    public abstract class BaseIntegrationTest
    {
        protected NhibernateUnitOfWork UnitOfWork;

        [SetUp]
        public void SetUp()
        {
            UnitOfWork = new NhibernateUnitOfWork(new NhibernateConfigurator());
            UnitOfWork.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            UnitOfWork.Rollback();
        }
    }
}