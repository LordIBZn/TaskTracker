using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // todo, in_progress, done
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
    }
}
