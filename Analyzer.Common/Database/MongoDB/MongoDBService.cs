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
            
        }

        public static MongoDBService GetInstance(String serverAdress, String databaseName)
        {
            if (MongoDBService._operator == null)
                MongoDBService._operator = new MongoDBService(serverAdress, databaseName);

            return MongoDBService._operator;
        }

        public bool OpenConnection()
        {
            try
            {
                this.mongoDBClient = new MongoClient(this.serverAddress);
                this.database = mongoDBClient.GetDatabase(this.databaseName);

                this.datawriteQueue = new SortedList<string, BsonDocument>();
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error opening DB connection to address: " + this.serverAddress + "and database: " + this.databaseName, ex);
                return false;
            }

            return true;
        }

        public void AddToWriteQueue(String collectionName, object data)
        {
            this.datawriteQueue.Add(collectionName, data.ToBsonDocument());
        }

        public void AddToWriteQueue(SortedList<String, object> dataQueue)
        {
            foreach(var data in dataQueue)
                this.datawriteQueue.Add(data.Key, data.Value.ToBsonDocument());
        }

        public void WriteToDatabase()
        {
            var keys = this.datawriteQueue.Keys;
            foreach(var key in keys)
            {
                var collection = this.database.GetCollection<BsonDocument>(key);
                var documentsToInsert = from data in this.datawriteQueue where data.Key == key select data.Value;

                //this.datawriteQueue.Where( o => o.Key == key).
                var result = collection.InsertManyAsync(documentsToInsert);


            }


        }

        public bool CloseConnection()
        {
            throw new NotImplementedException("MongoDB Does not need to close connection. No need to use this function. Only interface implementation.");
        }
    }
}
