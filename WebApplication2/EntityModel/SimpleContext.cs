using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.EntityModel
{
    public abstract class SimpleContext:IDisposable
    {
        protected SqlConnection _sqlConnection { get; set; }
        public SimpleContext(string connectionStringName)
        {
          string connection=  ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            _sqlConnection = new SqlConnection(connection);
            _sqlConnection.Open();
        }

        public abstract void Initializer();

        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }

}