using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class Outage
    {
        public int OutageId { get; set; }
        public string OutageName { get; set; }
        public DateTime? OutageDate { get; set; }
        public TimeSpan? OutageFrom { get; set; }
        public TimeSpan? OutageTo { get; set; }
    }
}
