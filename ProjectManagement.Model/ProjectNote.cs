using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManangment.Model
{
    public class ProjectNote
    {
        public int NoteId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Project Project { get; set; }
    }
}
