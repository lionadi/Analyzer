using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{
    interface INoSQLDatabaseOperations
    {
        void AddToWriteQueue(String collectionName, object data);
        void AddToWriteQueue(SortedList<String, object> dataQueue);
    }
}
