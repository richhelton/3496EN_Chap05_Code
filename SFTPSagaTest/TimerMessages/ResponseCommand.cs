using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace TimerMessages
{
    public class ResponseCommand : ICommand
    {
        public Guid RequestId { get; set; }
        public StateCodes state { get; set; }

    }
}
