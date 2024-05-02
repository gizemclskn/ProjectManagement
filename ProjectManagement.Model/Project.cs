using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManangment.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } 

        public Customer Customer { get; set; }
        public ICollection<ProjectNote> ProjectNotes { get; set; } 
        public ICollection<ProjectFile> ProjectFiles { get; set; }
    }
}
