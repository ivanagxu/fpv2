using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace fpcore.DAO.MSSql
{
    public class MSSqlDatabase : IDatabase
    {
        private string connStr = null;
        public bool setConnectionString(string connStr)
        {
            this.connStr = connStr;
            return true;
        }

        public DbConnection getConnection()
        {
            if (connStr == null)
                throw new Exception("Connection string was not set");

            DbConnection conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public DbTransaction beginTransaction(DbConnection conn)
        {
            SqlConnection connection = (SqlConnection)conn;
            return connection.BeginTransaction();
        }
    }
}
