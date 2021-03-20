using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCRUD.Classes.Utility.DataAccess
{
    static class ConnectionString
    {
        public static string config = ConfigurationManager.ConnectionStrings["SystemDatabaseConnectionTemp"].ConnectionString;
    }
}
