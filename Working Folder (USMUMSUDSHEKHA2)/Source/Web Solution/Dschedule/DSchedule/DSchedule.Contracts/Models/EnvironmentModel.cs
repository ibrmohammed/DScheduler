using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class EnvironmentModel
    {
        public int EnvironmentID { get; set; }
        public int NodeID { get; set; }
        public string EnvironmentName { get; set; }
        public string EnvironmentPath { get; set; }
        public System.DateTime CreatedDateTime { get; set; }

        public List<Environment> EnvironmentList { get; set; }
        public List<Node> NodesList { get; set; }
    }
}
