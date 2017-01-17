using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Common.IO;

namespace Analyzer.Common.Configuration
{
    /// <summary>
    /// This is used to 
    /// </summary>
    public static class ConfigurationManager
    {
        public static class AppSettings
        {
            public static bool MongoDBSerice
            {
                get
                {
                    bool value = false;
                    return Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.MongoDBService], out value) ? value : false;
                }
            }

            public static String MongoDBDatabaseName
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.MongoDBDatabaseName];
                }
            }

            public static String MongoDBServerAddress
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.MongoDBServerAddress];
                }
            }
        }

        public static List<object> GetConfiguredSources(String configurationFileAbsolutePath)
        {
            List<object> sources = null;
            try
            {
                if (!File.Exists(configurationFileAbsolutePath))
                {
                    String fileContent = File.ReadAllText(configurationFileAbsolutePath);

                    sources = (List<object>)XMLObjectSerializer.ObjectToXML(fileContent, new List<object>().GetType());
                }
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error in reading the configuration file: ", ex);
            }

            return sources;
        }

        public static bool WriteConfiguredSources(List<object> configuredSources, String configurationFileAbsolutePath)
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
                Logger.ExceptionLoggingService.Instance.WriteError("Error in writing the configuration file: ", ex);
            }

            return operationStatus;
        }
    }
}
