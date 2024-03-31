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
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<JobType> JobType { get; set; }
        public DbSet<JobSkill> JobSkill { get; set; }
        //public DbSet<OTPRecord> OTPRecord { get; set; }
        public DbSet<JobJobSkill> JobJobSkill { get; set; }
        public DbSet<Skill> Skill { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
          


            modelBuilder.Entity<Job>()
                .HasOne(j => j.JobType)   // Job has one JobType
                .WithMany()               // JobType can be associated with many Jobs
                .HasForeignKey(j => j.JobTypeId);  // Foreign key

            modelBuilder.Entity<JobJobSkill>()
                .HasKey(jjs => new { jjs.JobId, jjs.JobSkillId });  //

            modelBuilder.Entity<JobJobSkill>()
                .HasOne(jjs => jjs.Job)
                .WithMany(j => j.JobJobSkill)
                .HasForeignKey(jjs => jjs.JobId);

            modelBuilder.Entity<JobJobSkill>()
               .HasOne(jjs => jjs.JobSkill)
               .WithMany()
               .HasForeignKey(jjs => jjs.JobSkillId);


        }

    }
}

