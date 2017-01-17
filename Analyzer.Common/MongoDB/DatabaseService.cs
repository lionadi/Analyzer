using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.MongoDB
{

    public class DatabaseService
    {
        private static DatabaseService _operator = null;

        public DatabaseService()
        {
            
        }

        public static DatabaseService GetInstance()
        {
            if (DatabaseService._operator == null)
                DatabaseService._operator = new DatabaseService();

            return DatabaseService._operator;
        }

        //public void AddToWriteQueue()
    }
}
