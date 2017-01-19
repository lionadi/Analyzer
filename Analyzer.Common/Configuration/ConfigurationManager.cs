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

            public static int DatabaseWriteQueueSize
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultWriteQueueSize;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.DatabaseWriteQueueSize], out value) ? value : Analyzer.Common.Constants.Database.DefaultWriteQueueSize;
                }
            }

            public static int MaxAnalyzerRuntimeHours
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultMaxAnalyzerRuntimeHours;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.MaxAnalyzerRuntimeHours], out value) ? value : Analyzer.Common.Constants.Database.DefaultMaxAnalyzerRuntimeHours;
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

            public static String WebScrapingConfigurationFileLocation
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.WebScrapingConfigurationFileLocation];
                }
            }

            private static List<String> _userAgentOptions;
            public static List<String> UserAgentOptions
            {
                get
                {
                    _userAgentOptions = System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.UserAgentOptions].ToString().Split('#').ToList();
                    return _userAgentOptions;
                }
            }

            

            public static int RSSFeedHistoryRangeForScrapingInDays
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultRSSFeedHistoryRangeForScrapingInDays;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.RSSFeedHistoryRangeForScrapingInDays], out value) ? value : Analyzer.Common.Constants.Database.DefaultRSSFeedHistoryRangeForScrapingInDays;
                }
            }

            public static int WebScrapingTimeIntervalsInMinutes
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultWebScrapingTimeIntervalsInMinutes;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.WebScrapingTimeIntervalsInMinutes], out value) ? value : Analyzer.Common.Constants.Database.DefaultWebScrapingTimeIntervalsInMinutes;
                }
            }

            public static int RSSFeedReaderWaitTimeInMilliseconds
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultRSSFeedReaderWaitTimeInMilliseconds;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.RSSFeedReaderWaitTimeInMilliseconds], out value) ? value : Analyzer.Common.Constants.Database.DefaultRSSFeedReaderWaitTimeInMilliseconds;
                }
            }

            public static int WebDataReaderWaitTimeInMilliseconds
            {
                get
                {
                    int value = Analyzer.Common.Constants.Database.DefaultWebDataReaderWaitTimeInMilliseconds;
                    return Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[Analyzer.Common.Constants.AppSettingsKeys.WebDataReaderWaitTimeInMilliseconds], out value) ? value : Analyzer.Common.Constants.Database.DefaultWebDataReaderWaitTimeInMilliseconds;
                }
            }
        }

        public static List<T> GetConfiguredSources<T>(String configurationFileAbsolutePath)
        {
            List<T> sources = null;
            try
            {
                if (File.Exists(configurationFileAbsolutePath))
                {
                    String fileContent = File.ReadAllText(configurationFileAbsolutePath);

                    sources = (List<T>)XMLObjectSerializer.ObjectToXML(fileContent, new List<T>().GetType());
                }
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error in reading the configuration file: ", ex);
            }

            return sources;
        }

        public static bool WriteConfiguredSources<T>(List<T> configuredSources, String configurationFileAbsolutePath)
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
