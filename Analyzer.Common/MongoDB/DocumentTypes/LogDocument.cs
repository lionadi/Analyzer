using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.MongoDB.DocumentTypes
{
    [DataContract]
    public class LogItem
    {
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public DateTime DateAndTimeOfEvent { get; set; }

        [DataMember]
        public String Data { get; set; }
    }
}
