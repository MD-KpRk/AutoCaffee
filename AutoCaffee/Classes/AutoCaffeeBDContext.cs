using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoCaffee
{
    public partial class AutoCaffeeBDContext : DbContext
    {
        public AutoCaffeeBDContext()
        {
        }

        public AutoCaffeeBDContext(DbContextOptions<AutoCaffeeBDContext> options )
            : base(options)
        {
        }

        public DbSet<Check> Checks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Dolg> Dolgs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderstatus> Orderstatuses { get; set; }
        public DbSet<Orderstring> Orderstrings { get; set; }
        public DbSet<Personal> Personals { get; set; }
    }
}
