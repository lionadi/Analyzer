using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{
    interface INoSQLDatabaseOperations
    {
        bool AddToWriteQueue<T>(String collectionName, T data);
        bool AddToWriteQueue<T>(SortedList<String, T> dataQueue);
        int GetQueueSize();
    }
}
