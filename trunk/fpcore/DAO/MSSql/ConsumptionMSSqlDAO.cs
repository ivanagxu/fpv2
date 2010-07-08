using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace fpcore.DAO.MSSql
{
    public class ConsumptionMSSqlDAO : BaseDAO, IConsumptionDAO
    {
        #region IConsumptionDAO 成员

        public Consumption get(int objId, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<Consumption> consumption = search("where FPObject.ObjectId = '" + objId + "' and IsDeleted = 0 ", trans);
            if (consumption != null && consumption.Count > 0)
            {
                return consumption[0];
            }
            else
            {
                return null;
            }
        }

        public List<fpcore.Model.Consumption> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select total,totalunit,store,storeunit,used,usedunit,inventoryid,asdate,subtotal,substore,subused, FPObject.* from Consumption inner join FPObject on Consumption.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<Consumption> consumptions = getQueryResult(cmd);

            cmd.Dispose();
            return consumptions;
        }

        private List<Consumption> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();


            //  IPrintItemDAO printItemDAO = DAOFactory.getInstance().createPrintJobDAO();
            IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
            IInventoryDAO inventoryDAO = DAOFactory.getInstance().createInventoryDAO();

            List<Consumption> consumptions = new List<Consumption>();
            Consumption consumption = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    consumption = new Consumption();

                    consumption.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    consumption.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    consumption.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    consumption.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    consumption.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    consumption.inventory = inventoryDAO.Get(getInt(dt.Rows[i]["inventoryid"]), cmd.Transaction);
                    consumption.store = getDecimal(dt.Rows[i]["store"]);
                    consumption.storeunit = getString(dt.Rows[i]["storeunit"]);
                    consumption.total = getDecimal(dt.Rows[i]["total"]);
                    consumption.totalunit = getString(dt.Rows[i]["totalunit"]);
                    consumption.used = getDecimal(dt.Rows[i]["used"]);
                    consumption.usedunit = getString(dt.Rows[i]["usedunit"]);
                    consumption.asdate = getDateTime(dt.Rows[i]["asdate"]);

                    consumption.subtotal = getDecimal(dt.Rows[i]["subtotal"]);
                    consumption.subused = getDecimal(dt.Rows[i]["subused"]);
                    consumption.substore = getDecimal(dt.Rows[i]["substore"]);

                    consumptions.Add(consumption);
                }
            }

            return consumptions;
        }

        public bool add(fpcore.Model.Consumption consumption, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(consumption, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Consumption(ObjectId,total,totalunit,store,storeunit,used,usedunit,inventoryid,asdate,subtotal,substore,subused) values " +
                "(@ObjectId,@total,@totalunit,@store,@storeunit,@used,@usedunit,@inventoryid,@asdate,@subtotal,@substore,@subused)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            int? reid = null;

            if (consumption.inventory != null)
                reid = consumption.inventory.objectId;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, consumption.objectId));
            cmd.Parameters.Add(genSqlParameter("total", SqlDbType.Decimal, 18, consumption.total));
            cmd.Parameters.Add(genSqlParameter("totalunit", SqlDbType.VarChar, 50, consumption.totalunit));
            cmd.Parameters.Add(genSqlParameter("store", SqlDbType.VarChar, 18, consumption.store));
            cmd.Parameters.Add(genSqlParameter("storeunit", SqlDbType.VarChar, 50, consumption.storeunit));
            cmd.Parameters.Add(genSqlParameter("used", SqlDbType.Decimal, 18, consumption.used));
            cmd.Parameters.Add(genSqlParameter("usedunit", SqlDbType.VarChar, 50, consumption.usedunit));
            cmd.Parameters.Add(genSqlParameter("inventoryid", SqlDbType.Int, 10, reid));
            cmd.Parameters.Add(genSqlParameter("asdate", SqlDbType.DateTime, 0, consumption.asdate));

            cmd.Parameters.Add(genSqlParameter("subtotal", SqlDbType.Decimal, 18, consumption.subtotal));
            cmd.Parameters.Add(genSqlParameter("substore", SqlDbType.Decimal, 18, consumption.substore));
            cmd.Parameters.Add(genSqlParameter("subused", SqlDbType.Decimal, 18, consumption.subused));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }

        public bool delete(fpcore.Model.Consumption consumption, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.delete(consumption, transaction);
            return true;
        }

        public bool update(fpcore.Model.Consumption consumption, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(consumption, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Consumption set subtotal=@subtotal,substore=@substore,subused=@subused,asdate=@asdate,total=@total,totalunit=@totalunit,store=@store,storeunit=@storeunit,used=@used,usedunit=@usedunit,inventoryid=@inventoryid  " +
                " where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            int? reid = null;

            if (consumption.inventory != null)
                reid = consumption.inventory.objectId;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, consumption.objectId));
            cmd.Parameters.Add(genSqlParameter("total", SqlDbType.Decimal, 18, consumption.total));
            cmd.Parameters.Add(genSqlParameter("totalunit", SqlDbType.VarChar, 50, consumption.totalunit));
            cmd.Parameters.Add(genSqlParameter("store", SqlDbType.VarChar, 18, consumption.store));
            cmd.Parameters.Add(genSqlParameter("storeunit", SqlDbType.VarChar, 50, consumption.storeunit));
            cmd.Parameters.Add(genSqlParameter("used", SqlDbType.Decimal, 18, consumption.used));
            cmd.Parameters.Add(genSqlParameter("usedunit", SqlDbType.VarChar, 50, consumption.usedunit));
            cmd.Parameters.Add(genSqlParameter("inventoryid", SqlDbType.Int, 10, reid));
            cmd.Parameters.Add(genSqlParameter("asdate", SqlDbType.DateTime, 0, consumption.asdate));

            cmd.Parameters.Add(genSqlParameter("subtotal", SqlDbType.Decimal, 18, consumption.subtotal));
            cmd.Parameters.Add(genSqlParameter("substore", SqlDbType.Decimal, 18, consumption.substore));
            cmd.Parameters.Add(genSqlParameter("subused", SqlDbType.Decimal, 18, consumption.subused));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public decimal cateStoredCount(int inventoryid, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "SELECT     SUM(store) AS sum " +
                        "FROM         Consumption " +
                            " WHERE     (inventoryid = '" + inventoryid + "')";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            decimal count = getDecimal(dt.Rows[0]["sum"]);
            cmd.Dispose();

            return count;
        }

        #endregion
    }
}
