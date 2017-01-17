using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{
    interface IDatabaseConnection : INoSQLDatabaseOperations
    {
        bool OpenConnection();
        void WriteToDatabase();
        bool CloseConnection();
        
    }
}
