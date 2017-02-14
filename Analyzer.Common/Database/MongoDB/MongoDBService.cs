using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Common;

namespace Analyzer.Common.Database.MongoDB
{
    public class MongoDBService : Database.IDatabaseConnection
    {
        private static MongoDBService _operator = null;
        private MongoClient mongoDBClient = null;
        private IMongoDatabase database = null;
        private String serverAddress { get; set; }
        private String databaseName { get; set; }

        public bool IsDatabaseWriteInProgress
        {
            get
            {
                return isDatabaseWriteInProgress;
            }
        }

        private bool isDatabaseWriteInProgress = false;

        /// <summary>
        /// Add here items to make them wait for their turn to be processed
        /// </summary>
        private System.Collections.Generic.Dictionary<String, List<BsonDocument>> dataWriteQueue;

        /// <summary>
        /// When the database write operation is envoked the queue is emptied and the items are transfered here. While this holds data no other write operations are to be performed. New Items should go to the queue to wait for their turn again.
        /// </summary>
        //private System.Collections.Generic.Dictionary<String, List<BsonDocument>> processingQueue;

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

                this.dataWriteQueue = new System.Collections.Generic.Dictionary<String, List<BsonDocument>>();
            } catch(Exception ex)
            {
                throw new Exception("ERROR: Failed to establish MongoDB connection. Check your configurations!");
                System.Environment.Exit(2);
                this.mongoDBClient = null;
                this.database = null;
                //return false;
            }

            if (this.mongoDBClient == null || this.database == null)
            {
                throw new Exception("ERROR: Failed to establish MongoDB connection. Check your configurations!");
                System.Environment.Exit(2);
            }


            //return true;
        }

        /// <summary>
        /// This function will insert the data into the right location based on the key value. Data is stored as a two dimensional array.
        /// The first dimension is the collection name or index, the second represents that collections data.
        /// </summary>
        /// <param name="key">This represents the MongoDB collection name where to add the document</param>
        /// <param name="value">The data to be inserted</param>
        private bool AddToInner(String key, BsonDocument value)
        {
            List<BsonDocument> values = null;
            if (!this.dataWriteQueue.ContainsKey(key))
            {
                values = new List<BsonDocument>();
                dataWriteQueue[key] = values;
            } else
            {
                values = this.dataWriteQueue[key];
            }

            values.Add(value);

            return true;
        }

        public bool AddToWriteQueue<T>(String collectionName, T data)
        {
            return this.AddToInner(collectionName, data.ToBsonDocument());
            //this.datawriteQueue.add(data.ToBsonDocument());
        }

        public bool  AddToWriteQueue<T>(SortedList<String, T> dataQueue)
        {
            bool result = true;
            foreach(var data in dataQueue)
                result &= this.AddToInner(data.Key, data.ToBsonDocument());
            //this.datawriteQueue.Enqueue(data.Value.ToBsonDocument());

            return result;
        }

        /// <summary>
        /// Transfers the queue and all its child element into the processing queue for processing and empties the main queue.
        /// </summary>
        private Dictionary<string, List<BsonDocument>> TransferQueueToDatabaseProcessingQueue()
        {
            var tempQueueStorage = this.dataWriteQueue;
            this.dataWriteQueue = new Dictionary<string, List<BsonDocument>>();

            return tempQueueStorage;
        }

        public bool WriteToDatabase()
        {
            bool databaseOperationStatus = false;
            this.isDatabaseWriteInProgress = true;
            int queueSize = 0;
            try
            {

                var processingQueue = this.TransferQueueToDatabaseProcessingQueue();
                queueSize = processingQueue.Count;
                Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Starting to process queue at queue size: " + processingQueue.Count);
                var keys = processingQueue.Keys;
                int dataInsertionCount = 0;
                foreach (var key in keys)
                {
                    if(!this.DoesCollectionExist(key))
                    {
                        Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Collection does not exist in database. Creating collection: " + key);
                        this.database.CreateCollectionAsync(key);
                    }
                    var collection = this.database.GetCollection<BsonDocument>(key);
                    var documentsToInsert = from data in processingQueue where data.Key == key select data.Value;

                    collection.InsertManyAsync(documentsToInsert.FirstOrDefault());

                    Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Data written successfully to database. Documents queue size was: " + documentsToInsert.Count() + " Updated collection was: " + key);
                    dataInsertionCount += documentsToInsert.Count();
                    Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Data processed: " + dataInsertionCount + "/" + processingQueue.Count);
                }

                Logger.ExceptionLoggingService.Instance.WriteDBWriteOperation("Queue was processed and written to the database. Clearing queue at size: " + processingQueue.Count);
                processingQueue.Clear();
                databaseOperationStatus = true;
            } catch(Exception ex)
            {
                Logger.ExceptionLoggingService.Instance.WriteError("Error writing queue to database. Queue size: " + queueSize, ex);
            }
            finally
            {
                this.isDatabaseWriteInProgress = false;
            }

            return databaseOperationStatus;
        }

        public bool CloseConnection()
        {
            throw new NotImplementedException("MongoDB Does not need to close connection. No need to use this function. Only interface implementation.");
        }

        /// <summary>
        /// Returns the queue size if there is no database write operation in progress.
        /// </summary>
        /// <returns>Returns zero if there is a database operation in progress. </returns>
        public int GetQueueSize()
        {

            if (this.IsDatabaseWriteInProgress)
                return 0;

            int queueSize = 0;
            foreach (var keyData in this.dataWriteQueue)
                queueSize += keyData.Value.Count;

            return queueSize;
        }
    }
}
