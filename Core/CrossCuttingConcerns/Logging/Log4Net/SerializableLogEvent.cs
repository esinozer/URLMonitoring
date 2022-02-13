using log4net.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent (LoggingEvent loggingEvent )
        {
            _loggingEvent = loggingEvent;
        }
        //username gibi farklı işlemleri de koyabilirsin 
        public object Message => _loggingEvent.MessageObject;
        public object UserName => _loggingEvent.UserName;
    }
}
