using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace TimerMessages
{
    public class SendTime : ICommand
    {
        /**
         * Timer Info
         * */
        public string HrTxt { get; set; }
        public string MinTxt { get; set; }
        public string SecTxt { get; set; }
        public string AmPmTxt { get; set; }
        public Guid RequestId { get; set; }   // Must be Unique

         /**
         * SFTP Info
         * */
        public string sftpServer { get; set; }
        public string fileName { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
