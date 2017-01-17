using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database.DataItems
{
    [DataContract]
    public class WebData
    {
        [DataMember]
        public String URL { get; set; }
        [DataMember]
        public String Data { get; set; }
    }
}
