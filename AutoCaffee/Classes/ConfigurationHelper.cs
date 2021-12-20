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
        public static DbContextOptions<AutoCaffeeBDContext> dbContextOptions = new DbContextOptionsBuilder<AutoCaffeeBDContext>().UseSqlServer(ConfigurationHelper.getInstance().conString).Options;
        private static ConfigurationHelper instance;
        public readonly string conString;
        private ConfigurationHelper()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            conString = config.GetConnectionString("DefaultConnection");
        }

        public static ConfigurationHelper getInstance()
        {
            if (instance == null) instance = new ConfigurationHelper();
            return instance;
        }
    }

}
