using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Saga;
using TimerMessages;



namespace TimerSaga
{
    public class TimerSagaData : IContainSagaData
    {
        /*** 
         * Gets/sets the Id of the process. Do NOT generate this value in your code.
         * The value of the Id will be generated automatically to provide the
         * best performance for saving in a database.
        * ***/
        public virtual Guid Id { get; set; }  // Required
        /***
         * Contains the return address of the endpoint that caused the process to run.
         * ***/
        public virtual string Originator { get; set; }  //Required
        /***
         * Contains the Id of the message which caused the saga to start.
           This is needed so that when we reply to the Originator, any
           registered callbacks will be fired correctly.
         * ***/
        public virtual string OriginalMessageId { get; set; }  //Required

        [Unique]
        public virtual Guid RequestId { get; set; }  // Unique ID to lookup Request message
        /**
          * Email Info
          * */
        public virtual string emailServer { get; set; }
        public virtual string toAddress { get; set; }
        public virtual string fromAddress { get; set; }
        /**
          * Timer Info
          * */
        public string HrTxt { get; set; }
        public string MinTxt { get; set; }
        public string SecTxt { get; set; }
        public string AmPmTxt { get; set; }

     }
}
