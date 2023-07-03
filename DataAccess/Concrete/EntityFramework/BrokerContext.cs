using System;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
	public class BrokerContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=mssql-109799-0.cloudclusters.net,18159;User ID=fatihhazir;Password=Samsungsony2000;Database=BrokerProject;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<ContactUsForm> ContactUsForms { get; set; }


    }
}

