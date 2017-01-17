using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{

    public class DatabaseService
    {
        private static DatabaseService _operator = null;
        private IDatabaseConnection databaseConnection;

        public DatabaseService()
        {
            if (Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBSerice)
                this.databaseConnection = new MongoDB.MongoDBService(Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBServerAddress, Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBDatabaseName);
        }

        public static DatabaseService GetInstance()
        {
            if (DatabaseService._operator == null)
                DatabaseService._operator = new DatabaseService();

            return DatabaseService._operator;
        }

        public void AddtoWriteQueue(String collectionName, object data)
        {
            this.databaseConnection.AddToWriteQueue(collectionName, data);
        }

        public void AddtoWriteQueue(SortedList<String, object> dataQueue)
        {
            this.databaseConnection.AddToWriteQueue(dataQueue);
        }

        //public void AddToWriteQueue()
    }
}
