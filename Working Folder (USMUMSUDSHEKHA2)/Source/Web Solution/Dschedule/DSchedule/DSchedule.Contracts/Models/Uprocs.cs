using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class Uprocs
    {
        public int UprocID { get; set; }
        public int JobTypeID { get; set; }
        public int EnvironmentID { get; set; }
        public int AccountTypeID { get; set; }
        public string UprocName { get; set; }
        public string JobTypeName { get; set; }
        public string EnvironmentName { get; set; }
        public string AccountName { get; set; }
        public string FolderPath { get; set; }
        public string Command { get; set; }
        public string User { get; set; }
    }
}
