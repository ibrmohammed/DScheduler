using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class Sessions
    {
        public int SessionId { get; set; }
        public int EnvironmentID { get; set; }
        public int AccountTypeID { get; set; }
        public string Session { get; set; }
        public int UprocId { get; set; }
        public string Uproc { get; set; }
        public string Environment { get; set; }
        public string Node { get; set; }
        public string User { get; set; }
        public List<UprocsModel> UprocList { get; set; }
    }
}
