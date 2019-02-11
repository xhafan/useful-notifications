using System;
using System.IO;
using CoreDdd.Nhibernate.DatabaseSchemaGenerators;
using UsefulNotifications.Infrastructure;

namespace UsefulNotifications.ToolsApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Choose from the following options and press enter:");
            Console.WriteLine("1 Generate database schema sql file");

            var line = Console.ReadLine();
            if (line == "1")
            {
                var databaseSchemaFileName = "UsefulNotifications_generated_database_schema.sql";

                using (var nhibernateConfigurator = new NhibernateConfigurator(shouldMapDtos: false))
                {
                    new DatabaseSchemaGenerator(databaseSchemaFileName, nhibernateConfigurator).Generate();
                    Console.WriteLine($"Database schema sql file has been generated into {databaseSchemaFileName}");
                }

                _AddSemicolonsToPostgreSqlScript(databaseSchemaFileName);
            }
        }

        private static void _AddSemicolonsToPostgreSqlScript(string databaseSchemaFileName)
        {
            var sqlScript = File.ReadAllText(databaseSchemaFileName);
            sqlScript = sqlScript.Replace("\r\n", ";\r\n");
            File.WriteAllText(databaseSchemaFileName, sqlScript);
        }
    }
}
