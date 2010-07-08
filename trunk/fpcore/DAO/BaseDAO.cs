using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace fpcore.DAO
{
    public abstract class BaseDAO
    {
        protected IDatabase database = null;
        protected DbParameter genParameter(String paramName, OleDbType type, int size, Object value)
        {
            if (value == null)
                value = DBNull.Value;

            DbParameter param = null;

            if (type == OleDbType.DBTimeStamp)
            {
                if (value == DBNull.Value)
                {
                    param = new OleDbParameter(paramName, value);
                }
                else
                {
                    param = new OleDbParameter(paramName, ((Nullable<DateTime>)value).Value);
                }
            }
            else
            {
                param = new OleDbParameter(paramName, type, size);
                param.Value = value;
            }

            return param;
        }

        protected SqlParameter genSqlParameter(String paramName, SqlDbType type, int size, Object value)
        {
            if (value == null)
                value = DBNull.Value;

            SqlParameter param = null;

            if (type == SqlDbType.DateTime)
            {
                if (value == DBNull.Value)
                {
                    param = new SqlParameter(paramName, value);
                }
                else
                {
                    param = new SqlParameter(paramName, ((Nullable<DateTime>)value).Value);
                }
            }
            else
            {
                param = new SqlParameter(paramName, type, size);
                param.Value = value;
            }

            return param;
        }

        public double getDouble(Object val)
        {
            if (val == DBNull.Value)
                return 0;
            else
                return (double)val;
        }

        public decimal getDecimal(Object val)
        {
            if (val == DBNull.Value)
                return 0;
            else
                return (decimal)val;
        }

        public int getInt(Object val)
        {
            if (val == DBNull.Value)
                return 0;
            else
                return (int)val;
        }

        public bool getBool(Object val)
        {
            if (val == DBNull.Value)
                return false;
            else
                return (bool)val;
        }

        public Nullable<DateTime> getDateTime(Object val)
        {
            if (val == DBNull.Value)
                return null;
            else
                return (DateTime)val;
        }

        public String getString(Object val)
        {
            if (val == DBNull.Value)
                return null;
            else
                return (String)val;
        }
    }
}
