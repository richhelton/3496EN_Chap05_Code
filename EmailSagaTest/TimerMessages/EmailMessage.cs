using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace TimerMessages
{
    public class EmailMessage : ICommand
    {

        public Guid RequestId { get; set; }   // Must be Unique

        /**
          * Email Info
          * */
        public string emailServer { get; set; }
        public string toAddress { get; set; }
        public string fromAddress { get; set; }


    }
}
