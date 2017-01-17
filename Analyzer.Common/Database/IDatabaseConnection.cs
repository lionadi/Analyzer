using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common.Database
{
    interface IDatabaseConnection
    {
        bool OpenConnection();
        bool WriteToDatabase();
        bool CloseConnection(); 
    }
}
