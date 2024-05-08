using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UnitOfWorks;
using ProjectManangment.Model;

namespace ProjectManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("list")]
        public IActionResult ListProjects()
        {
            var projects = _unitOfWork.Projects.GetAll();
            return Ok(projects);
        }
        [HttpPost("detail")]
        public IActionResult GetProject([FromBody] int projectId)
        {
            var project = _unitOfWork.Projects.Get(projectId);
            return project != null ? Ok(project) : NotFound();
        }

        [HttpPost("create")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            _unitOfWork.Projects.Add(project);
            _unitOfWork.Complete();
            return Created("", project);
        }
        [HttpPost("update")]
        public IActionResult UpdateProject([FromBody] Project project)
        {
            var existingProject = _unitOfWork.Projects.Get(project.ProjectId);
            if (existingProject == null)
                return NotFound();

            existingProject.Name = project.Name;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.Status = project.Status;

            _unitOfWork.Complete();
            return NoContent();
        }
        [HttpPost("delete")]
        public IActionResult DeleteProject([FromBody] int projectId)
        {
            var project = _unitOfWork.Projects.Get(projectId);
            if (project == null)
                return NotFound();

            _unitOfWork.Projects.Remove(project);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
