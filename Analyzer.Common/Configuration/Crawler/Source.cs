using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Analyzer.Common.Configuration.Crawler
{
    public enum SourceType
    {
        Common,
        WordPress,
        Facebook,
        Twitter,
        Yahoo
    }

    public enum CrawlerType
    {
        RSS,
        Web
    }

    [DataContract]
    public class Source
    {
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String URL { get; set; }

        [DataMember]
        public String CssSelector { get; set; }

        [DataMember]
        public SourceType SourceType { get; set; }

        [DataMember]
        public CrawlerType CrawlerType { get; set; }

        [DataMember]
        public DateTime LastRunTime { get; set; }
    }
}
