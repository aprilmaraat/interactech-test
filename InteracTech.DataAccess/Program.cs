using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InteracTech.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public class AuthContextFactory : IDesignTimeDbContextFactory<pocoContext>
        {
            private const string migrationOptionsFileName = "MigrationOptions.json";
            /// <summary>
            /// Method to create MigrationOptions.json file (if not created) 
            /// </summary>
            /// <param name="args"></param>
            /// <returns></returns>
            public pocoContext CreateDbContext(string[] args)
            {
                while (!File.Exists(migrationOptionsFileName))
                {
                    Console.WriteLine("Please type in a connection string to your poco Database:");

                    string inputString = Console.ReadLine();

                    File.WriteAllText(migrationOptionsFileName,
                        $@"{{
  ""ConnectionStrings"": {{
    ""Database"": ""{inputString.Replace("\\", "\\\\").Replace("\"", "\\\"")}""
  }}
}}");
                }

                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile(migrationOptionsFileName);

                IConfigurationRoot configRoot = configurationBuilder.Build();

                args = new string[] { configRoot.GetConnectionString("Database") };

                if (args == null || args.Length < 1)
                    throw new ArgumentNullException(nameof(args)
                        , "No arguments passed, the first argument must be the connectionString for the database");

                string connectionString = args[0];
                var optionsBuilder = new DbContextOptionsBuilder<pocoContext>();
                Console.WriteLine($"Using connection string: {connectionString}");
                optionsBuilder.UseSqlServer(connectionString);

                return new pocoContext(optionsBuilder.Options);
            }
        }
    }
}
