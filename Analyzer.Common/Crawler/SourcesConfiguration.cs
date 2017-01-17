using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Common.IO;

namespace Analyzer.Common.Crawler
{
    /// <summary>
    /// This is used to 
    /// </summary>
    public class SourcesConfiguration
    {
        public static List<Source> GetConfiguredSources(String configurationFileAbsolutePath)
        {
            List<Source> sources = null;
            try
            {
                if (!File.Exists(configurationFileAbsolutePath))
                {
                    String fileContent = File.ReadAllText(configurationFileAbsolutePath);

                    sources = (List<Source>)XMLObjectSerializer.ObjectToXML(fileContent, new List<Source>().GetType());
                }
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteLog("Error in reading the configuration file: ", ex);
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
                File.WriteAllText(configurationFileAbsolutePath, XMLObjectSerializer.GetXMLFromObject(configuredSources));
                
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteLog("Error in writing the configuration file: ", ex);
            }

            return operationStatus;
        }
    }
}
