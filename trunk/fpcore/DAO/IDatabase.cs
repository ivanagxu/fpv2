using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IDatabase
    {
        bool setConnectionString(string connStr);
        DbConnection getConnection();
        DbTransaction beginTransaction(DbConnection conn);
    }
}
