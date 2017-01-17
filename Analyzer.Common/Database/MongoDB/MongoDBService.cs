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

        public MongoDBService()
        {

        }

        public static MongoDBService GetInstance()
        {
            if (MongoDBService._operator == null)
                MongoDBService._operator = new MongoDBService();

            return MongoDBService._operator;
        }

        public bool CloseConnection()
        {
            throw new NotImplementedException();
        }

        public bool OpenConnection()
        {
            throw new NotImplementedException();
        }

        public bool WriteToDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
