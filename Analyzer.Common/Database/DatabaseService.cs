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

        public Task<bool> AddtoWriteQueueAsync<T>(String collectionName, T data)
        {
            return Task.Factory.StartNew(() => this.databaseConnection.AddToWriteQueue(collectionName, data)).ContinueWith(task => {
                if (databaseConnection.GetQueueSize() >= MaxQueueSize)
                    return databaseConnection.WriteToDatabase();
                return task.Result;
            });
        }

        public Task<bool> AddtoWriteQueueAsync<T>(SortedList<String, T> dataQueue)
        {
            
                return Task.Factory.StartNew(() => this.databaseConnection.AddToWriteQueue(dataQueue)).ContinueWith( task => {
                    if (databaseConnection.GetQueueSize() >= MaxQueueSize)
                        return databaseConnection.WriteToDatabase();
                    return task.Result;
                });
        }

        /// <summary>
        /// Writes the remaining queued data into the database
        /// </summary>
        public Task<bool> Flush()
        {
            return Task.Factory.StartNew(() => this.databaseConnection.WriteToDatabase());
        }

        //public void AddToWriteQueue()
    }
}
