using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using fpcore.Model;
using System.Data.Common;
using System.Data;

namespace fpcore.DAO.MSSql
{
    public class AssetConsumptionMSSqlDAO : BaseDAO, IAssetConsumptionDAO
    {
        public bool add(AssetConsumption assetConsumption, DbTransaction transaction)
        {

            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.add(assetConsumption, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into AssetConsumption(ObjectId,jobid,qty,size,unit,purpose,cost,product) values " +
                "(@ObjectId,@jobid, @qty, @size, @unit,@purpose, @cost,@product)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = trans.Connection;
            cmd.Transaction = trans;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, assetConsumption.objectId));
            cmd.Parameters.Add(genSqlParameter("jobid", SqlDbType.NVarChar, 255, assetConsumption.jobid));
            cmd.Parameters.Add(genSqlParameter("qty", SqlDbType.NVarChar, 255, assetConsumption.qty));
            cmd.Parameters.Add(genSqlParameter("size", SqlDbType.NVarChar, 255, assetConsumption.size));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 255, assetConsumption.unit));
            cmd.Parameters.Add(genSqlParameter("purpose", SqlDbType.Int, 10, assetConsumption.purpose));
            cmd.Parameters.Add(genSqlParameter("cost", SqlDbType.NVarChar, 50, assetConsumption.cost));

            if(assetConsumption.product != null)
                cmd.Parameters.Add(genSqlParameter("product", SqlDbType.Int, 10, assetConsumption.product.objectId));
            else
                cmd.Parameters.Add(genSqlParameter("product", SqlDbType.Int, 10, null));

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }
        public bool delete(AssetConsumption assetConsumption, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.delete(assetConsumption, transaction);
            return true;
        }

        public bool update(AssetConsumption assetConsumption, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.update(assetConsumption, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update AssetConsumption set jobid = @jobid,qty = @qty, size = @size, unit = @unit, purpose = @purpose, cost = @cost, product = @product where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, assetConsumption.objectId));
            cmd.Parameters.Add(genSqlParameter("jobid", SqlDbType.NVarChar, 255, assetConsumption.jobid));
            cmd.Parameters.Add(genSqlParameter("qty", SqlDbType.NVarChar, 255, assetConsumption.qty));
            cmd.Parameters.Add(genSqlParameter("size", SqlDbType.NVarChar, 255, assetConsumption.size));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 255, assetConsumption.unit));
            cmd.Parameters.Add(genSqlParameter("purpose", SqlDbType.Int, 10, assetConsumption.purpose));
            cmd.Parameters.Add(genSqlParameter("cost", SqlDbType.NVarChar, 50, assetConsumption.cost));

            if (assetConsumption.product != null)
                cmd.Parameters.Add(genSqlParameter("product", SqlDbType.Int, 10, assetConsumption.product.objectId));
            else
                cmd.Parameters.Add(genSqlParameter("product", SqlDbType.Int, 10, null));

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }

        public List<AssetConsumption> get(string jobid, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<AssetConsumption> jobDetails = search("where jobid = '" + jobid + "' and IsDeleted = 0 ", transaction);
            return jobDetails;
        }

        public AssetConsumption get(int objId, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<AssetConsumption> jobDetails = search("where FPObject.ObjectId = '" + objId + "'", transaction);
            if (jobDetails != null && jobDetails.Count > 0)
            {
                return jobDetails[0];
            }
            else
            {
                return null;
            }
        }

        public List<AssetConsumption> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select AssetConsumption.jobid, AssetConsumption.qty, AssetConsumption.size, AssetConsumption.unit, AssetConsumption.purpose ,AssetConsumption.cost,AssetConsumption.product, FPObject.* from AssetConsumption inner join FPObject on AssetConsumption.ObjectId = FPObject.ObjectId " + query;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<AssetConsumption> jobDetails = getQueryResult(cmd);

            cmd.Dispose();

            return jobDetails;
        }
        private List<AssetConsumption> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<AssetConsumption> assetConsumptions = new List<AssetConsumption>();
            AssetConsumption assetConsumption = null;
            IInventoryDAO inventoryDAO = DAOFactory.getInstance().createInventoryDAO();

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    assetConsumption = new AssetConsumption();

                    assetConsumption.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    assetConsumption.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    assetConsumption.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    assetConsumption.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    assetConsumption.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    assetConsumption.jobid = getString(dt.Rows[i]["jobid"]);
                    assetConsumption.qty = getString(dt.Rows[i]["qty"]);
                    assetConsumption.size = getString(dt.Rows[i]["size"]);
                    assetConsumption.unit = getString(dt.Rows[i]["unit"]);
                    assetConsumption.purpose = getInt(dt.Rows[i]["purpose"]);
                    assetConsumption.cost = getString(dt.Rows[i]["cost"]);
                    assetConsumption.product = inventoryDAO.Get(getInt(dt.Rows[i]["product"]),cmd.Transaction);
                    assetConsumptions.Add(assetConsumption);
                }
            }
            return assetConsumptions;
        }
    }
}
