using ProjectManagement.Core.Repositories;
using ProjectManangment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectManagement.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Project> Projects { get; }
        IGenericRepository<ProjectNote> ProjectNotes { get; }
        IGenericRepository<ProjectFile> ProjectFiles { get; }

        int Complete();  // Değişiklikleri kaydetmek için
    }
}
