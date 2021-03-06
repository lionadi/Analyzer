﻿using System;
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
    public class Log : DataItem
    {
        public void DataItem()
        {
            this.LogEntryDate = DateTime.Now;
        }

        [DataMember]
        public DateTime DateAndTimeOfEvent { get; set; }

        [DataMember]
        public String Data { get; set; }

        [DataMember]
        public String Type { get; set; }

        [DataMember]
        public DateTime LogEntryDate { get; set; }
    }
}
