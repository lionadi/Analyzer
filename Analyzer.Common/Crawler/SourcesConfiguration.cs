using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Common.IO;

namespace Analyzer.Common.Crawler
{
    public class SourcesConfiguration
    {
        public static List<Source> GetConfiguredSources(String configurationFileAbsolutePath)
        {
            List<Source> sources = new List<Source>();

            if (!File.Exists(configurationFileAbsolutePath))
            {
                String fileContent = File.ReadAllText(configurationFileAbsolutePath);

                sources = (List<Source>)XMLObjectSerializer.ObjectToXML(fileContent, sources.GetType());
            }

            return sources;
        }

        public static bool WriteConfiguredSources(List<Source> configuredSources, String configurationFileAbsolutePath)
        {
            bool operationStatus = false;

            try
            {
                if (File.Exists(configurationFileAbsolutePath))
                {
                    File.Delete(configurationFileAbsolutePath);
                }
                File.WriteAllText(path, createText);
                String fileContent = File.ReadAllText(configurationFileAbsolutePath);

                sources = (List<Source>)XMLObjectSerializer.ObjectToXML(fileContent, sources.GetType());
            } catch(Exception ex)
            {

            }

            return operationStatus;
        }
    }
}
