using Microsoft.AspNetCore.Mvc;
using ProjectManangment.Model;
using ProjectManagement.Business;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("all")]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpPost("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _projectService.GetProject(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
                return BadRequest();

            try
            {
                _projectService.UpdateProject(project);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                _projectService.DeleteProject(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
