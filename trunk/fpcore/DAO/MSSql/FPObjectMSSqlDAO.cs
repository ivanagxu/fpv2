using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using fpcore.Model;

namespace fpcore.DAO.MSSql
{
    public class FPObjectMSSqlDAO : BaseDAO, IFPObjectDAO
    {

        public bool update(fpcore.Model.FPObject obj, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update FPObject set UpdateDate = getDate(), UpdateBy = @UpdateBy , IsDeleted = @IsDeleted where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, obj.objectId));
            cmd.Parameters.Add(genSqlParameter("UpdateBy", SqlDbType.NVarChar, 50, obj.updateBy));
            cmd.Parameters.Add(genSqlParameter("IsDeleted", SqlDbType.Int, 10, obj.isDeleted));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            FPObject objNew = get(obj.objectId, trans);
            obj.objectId = objNew.objectId;
            obj.createDate = objNew.createDate;
            obj.isDeleted = objNew.isDeleted;
            obj.updateBy = objNew.updateBy;
            obj.updateDate = objNew.updateDate;
            return true;
        }

        public bool delete(fpcore.Model.FPObject obj, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update FPObject set UpdateDate = getDate(), UpdateBy = @UpdateBy , IsDeleted = 1 where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, obj.objectId));
            cmd.Parameters.Add(genSqlParameter("UpdateBy", SqlDbType.NVarChar, 50, obj.updateBy));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            FPObject objNew = get(obj.objectId, trans);
            obj.objectId = objNew.objectId;
            obj.createDate = objNew.createDate;
            obj.isDeleted = objNew.isDeleted;
            obj.updateBy = objNew.updateBy;
            obj.updateDate = objNew.updateDate;
            return true;
        }

        public FPObject get(int objectId, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "Select ObjectId, CreateDate, UpdateDate , UpdateBy, IsDeleted from FPObject where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, objectId));
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            rdr.Close();

            FPObject obj = null;
            if (dt.Rows.Count > 0)
            {
                obj = new FPObject();
                obj.objectId = getInt(dt.Rows[0]["ObjectId"]);
                obj.createDate = getDateTime(dt.Rows[0]["CreateDate"]);
                obj.updateDate = getDateTime(dt.Rows[0]["UpdateDate"]);
                obj.updateBy = getString(dt.Rows[0]["UpdateBy"]);
                obj.isDeleted = getInt(dt.Rows[0]["IsDeleted"]) == 1;
            }
            else
            {

            }
            cmd.Dispose();

            return obj;
        }

        public bool add(fpcore.Model.FPObject obj, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into FPObject(ObjectId, CreateDate, UpdateDate, UpdateBy, IsDeleted) values (@ObjectId, getDate(), getDate(), @UpdateBy, @IsDeleted)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, obj.objectId));
            cmd.Parameters.Add(genSqlParameter("UpdateBy", SqlDbType.NVarChar, 50, obj.updateBy));
            cmd.Parameters.Add(genSqlParameter("IsDeleted", SqlDbType.Int, 10, obj.isDeleted));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            FPObject objNew = get(obj.objectId, trans);
            obj.objectId = objNew.objectId;
            obj.createDate = objNew.createDate;
            obj.isDeleted = objNew.isDeleted;
            obj.updateBy = objNew.updateBy;
            obj.updateDate = objNew.updateDate;
            return true;
        }

        public int nextObjectId(System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "Select max(ObjectId) + 1 as ObjectId from FPObject";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            rdr.Close();

            int objectId = -1;
            if (dt.Rows.Count > 0)
            {
                objectId = getInt(dt.Rows[0]["ObjectId"]);
            }
            else
            {
                throw new Exception("Get next Object ID failed");
            }
            cmd.Dispose();

            return objectId;
        }
    }
}
