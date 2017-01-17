using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common
{
    public static class Constants
    {
        public static class AppSettingsKeys
        {
            public static String MongoDBService = "MongoDBService";
            public static String MongoDBServerAddress = "MongoDBServerAddress";
            public static String MongoDBDatabaseName = "MongoDBDatabaseName";
        }

        public static class NoSQLDatabaseCollections
        {
            public static String Logs = "Logs";
            public static String Wordpress = "Wordpress";
        }
    }
}
