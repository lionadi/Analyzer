using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Analyzer.WebCrawler.RSS
{
    [DataContract]

    public class FeedItem

    {

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime Published { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int NumComments { get; set; }

    }
}
