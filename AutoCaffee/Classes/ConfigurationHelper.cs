using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCaffee
{
    // Singleton
    public class ConfigurationHelper
    {
        private static ConfigurationHelper instance;
        public readonly string conString;
        private ConfigurationHelper()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            conString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder
                .UseSqlServer(conString)
                .Options;
        }
        

        public static ConfigurationHelper getInstance()
        {
            if (instance == null)
                instance = new ConfigurationHelper();
            return instance;
        }
    }

}
