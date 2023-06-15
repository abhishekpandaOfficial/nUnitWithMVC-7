using System;
using DempApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DempApp.Web.Data
{
	public interface IApplicationDbContext
	{
        DbSet<Employee> Employees { get; set; }
        int SaveChanges();
        // Add other members from ApplicationDbContext if needed
    }
}

