using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Shared.Messages
{
    public class ReportFinishedEvent
    {
        public string Location { get; set; }
        public DateTime StaredAt { get; set; }
        public DateTime FinishedAd { get; set; }
        public int LocationCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
