using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class SessionsModel
    {
        public SessionsModel()
        {
            this.SessionsList = new List<Sessions>();
            this.UprocsReferenceData = new List<ReferenceData>();
            this.EnvironmentReferenceData = new List<ReferenceData>();
            this.UserReferenceData = new List<ReferenceData>();
            this.SessionsReferenceData = new List<ReferenceData>();
            this.DependentUprocsReferenceData = new List<ReferenceData>();
        }
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public int EnvironmentID { get; set; }
        public string EnvironmentName { get; set; }
        public int AccountTypeID { get; set; }
        public int UprocID { get; set; }
        public string Node { get; set; }
        public string User { get; set; }
        public string DependentSession { get; set; }
        public string DependentUproc { get; set; }
        public List<Sessions> SessionsList { get; set; }
        public List<ReferenceData> UprocsReferenceData { get; set; }
        public List<ReferenceData> EnvironmentReferenceData { get; set; }
        public List<ReferenceData> UserReferenceData { get; set; }
        public List<ReferenceData> SessionsReferenceData { get; set; }
        public List<ReferenceData> DependentUprocsReferenceData { get; set; }
    }
}
