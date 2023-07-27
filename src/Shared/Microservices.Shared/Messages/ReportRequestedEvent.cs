using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Shared.Messages
{
    public class ReportRequestedEvent
    {
        public string Location { get; set; }
    }
}
