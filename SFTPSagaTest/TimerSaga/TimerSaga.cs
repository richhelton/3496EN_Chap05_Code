using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Saga;
using TimerMessages;


namespace TimerSaga
{
    public class TimerSage : NServiceBus.Saga.Saga<TimerSagaData>,
                   IAmStartedByMessages<SendTime>,
                   IHandleMessages<ResponseCommand>,
                   IHandleTimeouts<TimeoutMessage>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<ResponseCommand>(x => x.RequestId).ToSaga(x => x.RequestId);
            ConfigureMapping<TimeoutMessage>(x => x.RequestId).ToSaga(x => x.RequestId);
        }

        /*
         * SendTime Handler
         * */
        public void Handle(SendTime message)
        {

            Data.RequestId = message.RequestId;

            TimeoutMessage tmMessage = new TimeoutMessage();
            tmMessage.RequestId = message.RequestId;

            DateTime toSet = (DateTime)setDateTime(message);

            /**
             * Save SFTP Info
             * */
            Data.sftpServer = message.sftpServer;
            Data.fileName = message.fileName;
            Data.username = message.username;
            Data.password = message.password;
         /**
             * Save Timer Info
             * */
            Data.HrTxt = message.HrTxt;
            Data.MinTxt = message.MinTxt;
            Data.SecTxt = message.SecTxt;
            Data.AmPmTxt = message.AmPmTxt;
 

            RequestTimeout(toSet, tmMessage);
            Bus.Send(message);

        }


        /*****
         * 
         * Return the DateTime of today or tomorrow
         * based on rather it has passed or not
         * 
         * ******/
        public DateTime setDateTime(SendTime message)
        {
            DateTime at = DateTime.Now;
            TimeoutMessage tmMessage = new TimeoutMessage();

            /*****
             * Calculate AM or PM
             * ***/
            int hours = Convert.ToInt32(message.HrTxt);
            if ((message.AmPmTxt == "AM") && (hours == 12))
                hours = 0;
            else if (((message.AmPmTxt == "PM") && (hours < 12)))
                hours += 12;
            int mins = Convert.ToInt32(message.MinTxt);
            int secs = Convert.ToInt32(message.SecTxt);

            var date = DateTime.Now.Date.Add(new TimeSpan(hours, mins, secs));

            // If date has passed
            if (at > date)
            {
                DateTime tomorrow = DateTime.Now.AddDays(1).Add(new TimeSpan(hours, mins, secs));
                return tomorrow;
            }
            else
            {
                return date;
            }
        }

        /*
         * RespondCommand Handler
         * */
        public void Handle(ResponseCommand message)
        {
            // Do nothing, possible future change
            if (message.state == StateCodes.ReceivedFromSaga)
            {
                Console.Out.WriteLine("TimerSaga::ResponseCommand::ReceivedFromSaga");
            }
            else if (message.state == StateCodes.SentSFTPFile)
            {
                Console.Out.WriteLine("TimerSaga::ResponseCommand::SentSFTPFile");
            }            
        }

        /*
         * Timeout
         * */
        public void Timeout(TimeoutMessage message)
        {

            /**
             * Retrieve SFTP Info
             * */

            SFTPMessage sftpMessage = new SFTPMessage();
            sftpMessage.sftpServer = Data.sftpServer;
            sftpMessage.fileName = Data.fileName;
            sftpMessage.username = Data.username;
            sftpMessage.password = Data.password;
            /**
              * Get Timer Info
              * */
            SendTime timeMessage = new SendTime();
            timeMessage.AmPmTxt = Data.AmPmTxt;
            timeMessage.HrTxt = Data.HrTxt;
            timeMessage.MinTxt = Data.MinTxt;
            timeMessage.SecTxt = Data.SecTxt;

            /**
               * 
               * Reset the Timer
               * 
               **/
            TimeoutMessage tmMessage = new TimeoutMessage();
            DateTime toSet = (DateTime)setDateTime(timeMessage);
   
            RequestTimeout(toSet, tmMessage);

            Bus.Send(sftpMessage);


        }
    }

}
