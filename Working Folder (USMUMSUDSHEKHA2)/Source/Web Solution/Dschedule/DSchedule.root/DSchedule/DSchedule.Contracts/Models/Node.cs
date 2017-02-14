using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class Node
    {

        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public string NodePath { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
