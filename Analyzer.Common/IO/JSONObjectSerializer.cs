using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.IO
{
    public class JSONObjectSerializer
    {
        public static String SerializeObjectToJSON(object data)
        {
            MemoryStream st = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(data.GetType());

            ser.WriteObject(st, data);
            StreamReader sr = new StreamReader(st);

            return sr.ReadToEnd();
        }
    }
}
