using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeGapApi.Models;

namespace WeGapApi.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }


}

