using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts.Models
{
    public class ReferenceData
    {
        public int RefId { get; set; }
        public string RefName { get; set; }
        public string RefValue { get; set; }
        public string RefDescription { get; set; }
        public string IsActive { get; set; }
        public string IsVisible { get; set; }
    }
}
