using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database.MongoDB
{
    public class MongoDBService : Database.IDatabaseConnection
    {
        private static MongoDBService _operator = null;
        private MongoClient mongoDBClient = null;
        private IMongoDatabase database = null;
        private String serverAddress { get; set; }
        private String databaseName { get; set; }
        private System.Collections.Generic.SortedList<String, BsonDocument> datawriteQueue;

        public MongoDBService(String serverAddress, String databaseName)
        {
            this.serverAddress = serverAddress;
            this.databaseName = databaseName;
            this.OpenConnection();
        }

        private bool DoesCollectionExist(String collectionName)
        {
            bool collectionExists = false;

            foreach (var item in database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            { 
                var name = item.Elements.FirstOrDefault().Value.AsString;
                if (name.Contains(collectionName))
                {
                    collectionExists = true;
                    break;
                }
            }

            return collectionExists;
        }

        public static MongoDBService GetInstance(String serverAdress, String databaseName)
        {
            if (MongoDBService._operator == null)
                MongoDBService._operator = new MongoDBService(serverAdress, databaseName);

            return MongoDBService._operator;
        }

        public void OpenConnection()
        {
            try
            {
                this.mongoDBClient = new MongoClient(this.serverAddress);
                this.database = mongoDBClient.GetDatabase(this.databaseName);

                this.datawriteQueue = new SortedList<string, BsonDocument>();
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error opening DB connection to address: " + this.serverAddress + "and database: " + this.databaseName, ex);
                this.mongoDBClient = null;
                this.database = null;
                //return false;
            }

            if (this.mongoDBClient == null || this.database == null)
                throw new Exception("ERROR: Failed to establish MongoDB connection. Check your configurations!");


            //return true;
        }

        public void AddToWriteQueue<T>(String collectionName, T data)
        {
            this.datawriteQueue.Add(collectionName, data.ToBsonDocument());
        }

        public void AddToWriteQueue<T>(SortedList<String, T> dataQueue)
        {
            foreach(var data in dataQueue)
                this.datawriteQueue.Add(data.Key, data.Value.ToBsonDocument());
        }

        public async Task<bool> WriteToDatabaseAsync()
        {
            bool databaseOperationStatus = false;
            try
            {
                Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Starting to process queue at queue size: " + this.datawriteQueue.Count);
                var keys = this.datawriteQueue.Keys;
                int dataInsertionCount = 0;
                foreach (var key in keys)
                {
                    if(!this.DoesCollectionExist(key))
                    {
                        Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Collection does not exist in database. Creating collection: " + key);
                        await this.database.CreateCollectionAsync(key);
                    }
                    var collection = this.database.GetCollection<BsonDocument>(key);
                    var documentsToInsert = from data in this.datawriteQueue where data.Key == key select data.Value;

                    await collection.InsertManyAsync(documentsToInsert);

                    Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Data written successfully to database. Documents queue size was: " + documentsToInsert.Count() + " Updated collection was: " + key);
                    dataInsertionCount += documentsToInsert.Count();
                    Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Data processed: " + dataInsertionCount + "/" + this.datawriteQueue.Count);
                }

                Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Queue was processed and written to the database. Clearing queue at size: " + this.datawriteQueue.Count);
                this.datawriteQueue.Clear();
                databaseOperationStatus = true;
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error writing queue to database. Queue size: " + this.datawriteQueue.Count, ex);
            }

            return databaseOperationStatus;
        }

        public bool CloseConnection()
        {
            throw new NotImplementedException("MongoDB Does not need to close connection. No need to use this function. Only interface implementation.");
        }

        public int GetQueueSize()
        {
            return this.datawriteQueue.Count;
        }
    }
}
