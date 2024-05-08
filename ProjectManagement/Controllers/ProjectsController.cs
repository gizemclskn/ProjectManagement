using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> GetProjectDetail()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(body);

                if (data == null || !data.ContainsKey("ProjectId"))
                    return BadRequest("Project ID is required.");

                int projectId = data["ProjectId"];
                var project = _unitOfWork.Projects.Get(projectId);

                if (project == null)
                    return NotFound();

                return Ok(project);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var project = JsonConvert.DeserializeObject<Project>(body);

                if (project == null)
                {
                    return BadRequest("Invalid project data");
                }

                _unitOfWork.Projects.Add(project);
                _unitOfWork.Complete();

                return Created("", project);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProject()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var project = JsonConvert.DeserializeObject<Project>(body);

                if (project == null || project.ProjectId == 0)
                    return BadRequest("Invalid project data.");

                var existingProject = _unitOfWork.Projects.Get(project.ProjectId);

                if (existingProject == null)
                    return NotFound();

                // Güncelleme işlemi
                existingProject.Name = project.Name;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.Status = project.Status;

                _unitOfWork.Complete();

                return NoContent();  // Başarıyla güncellendi
            }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProject()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(body);

                if (data == null || !data.ContainsKey("ProjectId"))
                    return BadRequest("Project ID is required.");

                int projectId = data["ProjectId"];
                var project = _unitOfWork.Projects.Get(projectId);

                if (project == null)
                    return NotFound();

                _unitOfWork.Projects.Remove(project);
                _unitOfWork.Complete();

                return NoContent();  // Başarıyla silindi
            }
        }
    }
}

