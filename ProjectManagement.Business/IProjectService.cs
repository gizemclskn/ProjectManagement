using ProjectManagement.Core.UnitOfWorks;
using ProjectManangment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Business
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(int id);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
    }

    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _unitOfWork.Projects.GetAll();
        }

        public Project GetProject(int id)
        {
            return _unitOfWork.Projects.Get(id);
        }

        public void CreateProject(Project project)
        {
            _unitOfWork.Projects.Add(project);
            _unitOfWork.Complete();
        }

        public void UpdateProject(Project project)
        {
            var existingProject = _unitOfWork.Projects.Get(project.ProjectId);
            if (existingProject == null)
                throw new Exception("Project not found");

            existingProject.Name = project.Name;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Status = project.Status;

            _unitOfWork.Complete();
        }

        public void DeleteProject(int id)
        {
            var project = _unitOfWork.Projects.Get(id);
            if (project == null)
                throw new Exception("Project not found");

            _unitOfWork.Projects.Remove(project);
            _unitOfWork.Complete();
        }
    }

}
