using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSchedule.Web.Models
{
    public class NodesModel
    {
        public int NodeID { get; set; }

        public string NodeName { get; set; }

        public string Path { get; set; }

        public DateTime AddedDate { get; set; }

        public List<TNode> NodesList { get; set; }
    }
}