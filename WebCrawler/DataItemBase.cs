using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.WebCrawler
{
    [DataContract]
    public class DataItemBase
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime Published { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public String Category { get; set; }
    }
}
