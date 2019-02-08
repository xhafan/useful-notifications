using System;
using System.IO;
using System.Reflection;
using DatabaseBuilder;
using Npgsql;
using NUnit.Framework;

namespace UsefulNotifications.Domain.IntegrationTests
{
    [SetUpFixture]
    public class RunOncePerTestRun
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            _CreateDatabase();
        }

        private void _CreateDatabase()
        {
            using (var nhibernateConfigurator = new NhibernateConfigurator())
            {
                var configuration = nhibernateConfigurator.GetConfiguration();
                var connectionString = configuration.Properties["connection.connection_string"];
                var scriptsDirectoryPath = Path.Combine(_GetAssemblyCodeBaseLocation(), "DatabaseScripts");

                var builderOfDatabase = new BuilderOfDatabase(() => new NpgsqlConnection(connectionString));
                builderOfDatabase.BuildDatabase(scriptsDirectoryPath);
            }
        }

        // https://stackoverflow.com/a/283917/379279
        private string _GetAssemblyCodeBaseLocation()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}