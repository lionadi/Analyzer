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
        private int MaxQueueSize = Analyzer.Common.Constants.Database.DefaultWriteQueueSize;

        public DatabaseService()
        {
            if (Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBSerice)
                this.databaseConnection = MongoDB.MongoDBService.GetInstance(Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBServerAddress, Analyzer.Common.Configuration.ConfigurationManager.AppSettings.MongoDBDatabaseName);

            this.MaxQueueSize = Analyzer.Common.Configuration.ConfigurationManager.AppSettings.DatabaseWriteQueueSize;
        }

        public void LogDatabaseServiceConfigurations()
        {

        }

        public static DatabaseService GetInstance()
        {
            if (DatabaseService._operator == null)
                DatabaseService._operator = new DatabaseService();

            return DatabaseService._operator;
        }

        public async Task<bool> AddtoWriteQueueAsync<T>(String collectionName, T data)
        {
            this.databaseConnection.AddToWriteQueue(collectionName, data);

            if (this.databaseConnection.GetQueueSize() >= this.MaxQueueSize)
                return await this.databaseConnection.WriteToDatabaseAsync();

            return true;
        }

        public async Task<bool> AddtoWriteQueueAsync<T>(SortedList<String, T> dataQueue)
        {
            this.databaseConnection.AddToWriteQueue(dataQueue);

            if (this.databaseConnection.GetQueueSize() >= this.MaxQueueSize)
                return await this.databaseConnection.WriteToDatabaseAsync();

            return true;
        }

        //public void AddToWriteQueue()
    }
}
