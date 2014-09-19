using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using TimerMessages;
using System.Messaging;
using Tamir.SharpSsh;



namespace SFTPClient
{
    public class SFTPMessageHandler : IHandleMessages<SFTPMessage>, IHandleMessages<SendTime>
    {

        public IBus Bus { get; set; }

        public void Handle(SFTPMessage message)
        {

             /**
             * Connect to the SFTP
             * and upload file
             * */
            Sftp Sftpclient = new Sftp(message.sftpServer,message.username,message.password);
            Sftpclient.Connect();
            string newFileName = string.Format("text-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt",
                    DateTime.Now);
              Sftpclient.Put(message.fileName, newFileName);

            ResponseCommand command = new ResponseCommand();
            command.RequestId = message.RequestId;

            command.state = StateCodes.SentSFTPFile;
            Bus.Send(command);

        }

        public void Handle(SendTime message)
        {

            ResponseCommand command = new ResponseCommand();
            command.RequestId = message.RequestId;
            command.state = StateCodes.ReceivedFromSaga;
            Bus.Send(command);

        }


    }
}
