using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Logger
{
    public class ExceptionLoggingService
    {
        // </ Singleton code
        // private fields
        private readonly FileStream _fileStream;
        private readonly StreamWriter _streamWriter;
        private static ExceptionLoggingService _instance;
        // public property
        public static ExceptionLoggingService Instance
        {
            get
            {
                return _instance ?? (_instance = new ExceptionLoggingService());
            }
        }
        //private constructor
        private ExceptionLoggingService()
        {
            _fileStream = File.OpenWrite(GetExecutionFolder() + "\\EasyAsFallingOffA.log");
            _streamWriter = new StreamWriter(_fileStream);
        }
        // <!-- Singleton code

        public void WriteLog(string message)
        {

            StringBuilder formattedMessage = new StringBuilder();
            formattedMessage.AppendLine("Date: " + DateTime.Now.ToString());
            formattedMessage.AppendLine("Message: " + message);
            _streamWriter.WriteLine(formattedMessage.ToString());
            _streamWriter.Flush();
        }

        public void WriteLog(string message, Exception ex)
        {
            String msgInnerExAndStackTrace = String.Format("{0}; Inner Ex: {1}; Stack Trace: { 2}", ex.Message, ex.InnerException, ex.StackTrace);
            StringBuilder formattedMessage = new StringBuilder();
            formattedMessage.AppendLine("Date: " + DateTime.Now.ToString());
            formattedMessage.AppendLine("Message: " + message + msgInnerExAndStackTrace);
            _streamWriter.WriteLine(formattedMessage.ToString());
            _streamWriter.Flush();
        }

        


        private string GetExecutionFolder()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
