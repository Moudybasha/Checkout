using System;
using System.Web;
using Elmah;
using log4net.Appender;
using log4net.Core;

namespace Checkout.CrossCutting.Framework.Logging.Appenders
{
    public class ElmahAppender : AppenderSkeleton
    {
        private static readonly Type DeclaringType = typeof (ElmahAppender);
        private ErrorLog _errorLog;
        private string _hostName;

        static ElmahAppender()
        {
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            _hostName = Environment.MachineName;
            try
            {
                _errorLog = ErrorLog.GetDefault(HttpContext.Current);
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("Could not create default ELMAH error log", ex);
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_errorLog == null)
                return;

            var error = loggingEvent.ExceptionObject == null ? new Error() : new Error(loggingEvent.ExceptionObject);
            error.Time = DateTime.Now;
            if (loggingEvent.MessageObject != null)
                error.Message = loggingEvent.MessageObject.ToString();
            error.Detail = RenderLoggingEvent(loggingEvent);
            error.HostName = _hostName;
            error.User = loggingEvent.UserName.Split('\\')[1];//loggingEvent.Identity;
            error.Type = loggingEvent.Level.ToString();
            
            _errorLog.Log(error);
        }
    }
}