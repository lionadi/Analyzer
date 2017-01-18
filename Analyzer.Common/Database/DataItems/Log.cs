using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database.DataItems
{
    public enum LogType
    {
        Unknown,
        Information,
        Warninig,
        Error,
        WriteOperation,
        ReadOperation,
        WebScraping
    }

    [DataContract]
    public class Log
    {
        [DataMember]
        public DateTime DateAndTimeOfEvent { get; set; }

        [DataMember]
        public String Data { get; set; }

        [DataMember]
        public LogType Type { get; set; }
    }
}
