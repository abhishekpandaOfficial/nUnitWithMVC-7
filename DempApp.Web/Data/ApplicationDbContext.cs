using System;
using DempApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DempApp.Web.Data
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
       
		public DbSet<Employee> Employees { get; set; }
       

    }
}

