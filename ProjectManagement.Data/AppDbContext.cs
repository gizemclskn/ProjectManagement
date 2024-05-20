using Microsoft.EntityFrameworkCore;
using ProjectManangment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        { 
        }  

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectNote> ProjectNotes { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);
            modelBuilder.Entity<ProjectNote>().HasKey(pn => pn.NoteId);
            modelBuilder.Entity<ProjectFile>().HasKey(pf => pf.FileId);
        }
    }
}
