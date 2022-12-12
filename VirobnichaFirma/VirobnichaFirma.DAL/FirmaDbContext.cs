using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirobnichaFirma.DAL
{
    public enum dbase
    {
        Countries,
        Cities,
        Affiliates,
        Customers,
        Jobtitles,
        Orders,
        OrderLists,
        Products,
        Employees
    }
    public class FirmaDbContext:DbContext
    {
        public FirmaDbContext() 
            : base()
        {
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=dima5324;database=production_company1", new MySqlServerVersion(new Version(8, 0, 29)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderList>()
                .HasKey(o => new { o.OrderId, o.ProductId });
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Jobtitle> Jobtitles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
