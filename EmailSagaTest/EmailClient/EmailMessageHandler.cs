using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using TimerMessages;
using System.Messaging;






namespace EmailClient
{
    public class EmailMessageHandler : IHandleMessages<EmailMessage>, IHandleMessages<SendTime>
    {

        public IBus Bus { get; set; }

        public void Handle(EmailMessage message)
        {

            /**
             * Get a Status of MSMQ's
             * **/
            // read all the queues
            StringBuilder sendMessage = new StringBuilder();
            string machineToRead = System.Environment.MachineName;
            var queues = MessageQueue.GetPrivateQueuesByMachine(machineToRead);
            foreach (MessageQueue queue in queues)
            {
                sendMessage.AppendLine(queue.QueueName);
            }



            /***
             * 
             * the Mail Message
             * 
             * ****/
 

            MailMessage nMail = new MailMessage();
            nMail.To.Add(message.toAddress);
            nMail.From = new MailAddress(message.fromAddress);
            nMail.Subject = "Operational Message -- MSMQ Queues";
            nMail.Body = sendMessage.ToString();
            SmtpClient sc = new SmtpClient(message.emailServer);
            sc.Send(nMail);

            ResponseCommand command = new ResponseCommand();
            command.RequestId = message.RequestId;

            command.state = StateCodes.SentEmail;
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
