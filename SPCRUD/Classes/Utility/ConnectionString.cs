using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCRUD.Classes.Utility
{
    static class ConnectionString
    {
        //change to "SystemDatabaseConnection" (the final connectionString) when app is ready to be deployed
        //see connection strings at App.config file
        public static readonly string config = ConfigurationManager.ConnectionStrings["SystemDatabaseConnectionTemp"].ConnectionString;
    }
}
