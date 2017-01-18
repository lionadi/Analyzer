using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{
    interface INoSQLDatabaseOperations
    {
        void AddToWriteQueue<T>(String collectionName, T data);
        void AddToWriteQueue<T>(SortedList<String, T> dataQueue);
        int GetQueueSize();
    }
}
