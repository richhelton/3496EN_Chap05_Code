﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerMessages
{
    public enum StateCodes
    {
        initial,
        SentSFTPFile,
        SentMySaga,
        SentTimerClient,
        ReceivedFromSaga,
        MyWCFClientFail,
        MyWCFClientFailXML,
        Timeout,
        Fail
    }
}