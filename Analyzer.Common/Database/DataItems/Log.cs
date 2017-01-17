using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database.DataItems
{
    [DataContract]
    public class Log
    {
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public DateTime DateAndTimeOfEvent { get; set; }

        [DataMember]
        public String Data { get; set; }
    }
}
