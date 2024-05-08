using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.Core.UnitOfWorks;
using ProjectManangment.Model;

namespace ProjectManagement.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("project-report")]
        public async Task<IActionResult> GetProjectReport()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<ProjectReportRequest>(body);

                if (request == null)
                {
                    return BadRequest("Invalid request data");
                }

                var projects = _unitOfWork.Projects.Find(p =>
                    p.StartDate >= request.StartDate &&
                    p.EndDate <= request.EndDate);

                var reportData = new ProjectReportData
                {
                    Projects = projects.ToList(),
                    TotalProjects = projects.Count()
                };

                return Ok(reportData);
            }
        }
    }
    public class ProjectReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ProjectReportData
    {
        public List<Project> Projects { get; set; }
        public int TotalProjects { get; set; }
    }
}