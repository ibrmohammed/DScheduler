using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSchedule.Contracts.Models
{
    public class UprocsModel
    {
        public UprocsModel()
        {
            UprocsList = new List<Uprocs>();
            JobTypeReferenceData = new List<ReferenceData>();
            EnvironmentReferenceData = new List<ReferenceData>();
            AccountTypeReferenceData = new List<ReferenceData>();
        }
        public int UprocID { get; set; }
        [Required]
        public int JobTypeID { get; set; }
        [Required]
        public int EnvironmentID { get; set; }
        [Required]
        public int AccountTypeID { get; set; }
        [Required]
        public string UprocName { get; set; }
        public int JobType { get; set; }
        public string EnvironmentName { get; set; }
        public int TypeOfAccount { get; set; }
        [Required]
        public string FolderPath { get; set; }
        [Required]
        public string Command { get; set; }
        public List<Uprocs> UprocsList { get; set; }
        public List<ReferenceData> JobTypeReferenceData { get; set; }
        public List<ReferenceData> EnvironmentReferenceData { get; set; }
        public List<ReferenceData> AccountTypeReferenceData { get; set; }
    }
}
