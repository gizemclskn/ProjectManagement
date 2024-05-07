using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Repositories;
using ProjectManagement.Core.UnitOfWorks;
using ProjectManagement.Data.GenericRepository;
using ProjectManangment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Project> Projects { get; }
        public IGenericRepository<ProjectNote> ProjectNotes { get; }
        public IGenericRepository<ProjectFile> ProjectFiles { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new GenericRepository<Customer>(_context);
            Projects = new GenericRepository<Project>(_context);
            ProjectNotes = new GenericRepository<ProjectNote>(_context);
            ProjectFiles = new GenericRepository<ProjectFile>(_context);
        } 
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
