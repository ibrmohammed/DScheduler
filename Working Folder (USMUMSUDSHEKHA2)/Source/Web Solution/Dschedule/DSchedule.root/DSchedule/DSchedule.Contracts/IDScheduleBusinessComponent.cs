using DSchedule.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSchedule.Contracts
{
    public interface IDScheduleBusinessComponent
    {
        List<NodesModel> GetNodes();
    }
}
