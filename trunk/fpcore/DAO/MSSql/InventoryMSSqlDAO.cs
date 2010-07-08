using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace fpcore.DAO.MSSql
{
    public class InventoryMSSqlDAO:BaseDAO, IInventoryDAO
    {
        #region IInventoryDAO 成员

        public Inventory Get(int objId, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<Inventory> inventories = List("where FPObject.ObjectId = '" + objId + "' and IsDeleted = 0 ", 1, 1, string.Empty, false, trans);
            if (inventories != null && inventories.Count > 0)
            {
                return inventories[0];
            }
            else
            {
                return null;
            }
        }

        public bool Add(Inventory inventory, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(inventory, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into inventory(ObjectId, orderno, assetno,category,receiveddate,receivedby,orderdeadline,remark,contactperson,tel,productno,productnameen,productnamecn,description,dimension,unit,unitcost,quantity,stored,status) values " +
                "(@ObjectId, @orderno,@assetno,@category,@receiveddate,@receivedby,@orderdeadline,@remark,@contactperson,@tel,@productno,@productnameen,@productnamecn,@description,@dimension,@unit,@unitcost,@quantity,@stored,@status)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            int? reid = null;

            if (inventory.receivedby != null)
                reid = inventory.receivedby.objectId;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, inventory.objectId));
            cmd.Parameters.Add(genSqlParameter("orderno", SqlDbType.NVarChar, 50, inventory.orderno));
            cmd.Parameters.Add(genSqlParameter("assetno", SqlDbType.NVarChar, 50, inventory.assetno));
            cmd.Parameters.Add(genSqlParameter("category", SqlDbType.VarChar, 50, inventory.category));
            cmd.Parameters.Add(genSqlParameter("receiveddate", SqlDbType.DateTime, 50, inventory.receiveddate));
            cmd.Parameters.Add(genSqlParameter("receivedby", SqlDbType.Int, 10, reid));
            cmd.Parameters.Add(genSqlParameter("orderdeadline", SqlDbType.VarChar, 50, inventory.orderdeadline));
            cmd.Parameters.Add(genSqlParameter("remark", SqlDbType.NVarChar, 255, inventory.remark));
            cmd.Parameters.Add(genSqlParameter("contactperson", SqlDbType.NVarChar, 50, inventory.contactperson));
            cmd.Parameters.Add(genSqlParameter("tel", SqlDbType.NVarChar, 50, inventory.Tel));
            cmd.Parameters.Add(genSqlParameter("productno", SqlDbType.NVarChar, 50, inventory.productno));
            cmd.Parameters.Add(genSqlParameter("productnameen", SqlDbType.NVarChar, 50, inventory.productnameen));
            cmd.Parameters.Add(genSqlParameter("productnamecn", SqlDbType.NVarChar, 10, inventory.productnamecn));
            cmd.Parameters.Add(genSqlParameter("description", SqlDbType.NVarChar, 255, inventory.description));
            cmd.Parameters.Add(genSqlParameter("dimension", SqlDbType.NVarChar, 50, inventory.dimension));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 50, inventory.unit));
            cmd.Parameters.Add(genSqlParameter("unitcost", SqlDbType.NVarChar, 50, inventory.unitcost));
            cmd.Parameters.Add(genSqlParameter("quantity", SqlDbType.NVarChar, 50, inventory.quantity));
            cmd.Parameters.Add(genSqlParameter("stored", SqlDbType.NVarChar, 50, inventory.stored));

            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, inventory.status));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }

        public int count(string condition, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select count(*) as total from Inventory inner join FPObject on Inventory.ObjectId = FPObject.ObjectId " + condition;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            int count = getInt(dt.Rows[0]["total"]);
            cmd.Dispose();

            return count;
        }

        public bool Update(fpcore.Model.Inventory inventory, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(inventory, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Inventory set orderno=@orderno, assetno=@assetno,category=@category,receiveddate=@receiveddate,receivedby=@receivedby,orderdeadline=@orderdeadline,remark=@remark,contactperson=@contactperson,tel=@tel,productno=@productno,productnameen=@productnameen,productnamecn=@productnamecn,description=@description,dimension=@dimension,unit=@unit,unitcost=@unitcost,quantity=@quantity,stored =@stored,status=@status  where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;


            int? reid = null;

            if (inventory.receivedby != null)
                reid = inventory.receivedby.objectId;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, inventory.objectId));
            cmd.Parameters.Add(genSqlParameter("orderno", SqlDbType.NVarChar, 50, inventory.orderno));
            cmd.Parameters.Add(genSqlParameter("assetno", SqlDbType.NVarChar, 50, inventory.assetno));
            cmd.Parameters.Add(genSqlParameter("category", SqlDbType.VarChar, 50, inventory.category));
            cmd.Parameters.Add(genSqlParameter("receiveddate", SqlDbType.DateTime, 50, inventory.receiveddate));
            cmd.Parameters.Add(genSqlParameter("receivedby", SqlDbType.Int, 10, reid));
            cmd.Parameters.Add(genSqlParameter("orderdeadline", SqlDbType.VarChar, 50, inventory.orderdeadline));
            cmd.Parameters.Add(genSqlParameter("remark", SqlDbType.NVarChar, 255, inventory.remark));
            cmd.Parameters.Add(genSqlParameter("contactperson", SqlDbType.NVarChar, 50, inventory.contactperson));
            cmd.Parameters.Add(genSqlParameter("tel", SqlDbType.NVarChar, 50, inventory.Tel));
            cmd.Parameters.Add(genSqlParameter("productno", SqlDbType.NVarChar, 50, inventory.productno));
            cmd.Parameters.Add(genSqlParameter("productnameen", SqlDbType.NVarChar, 50, inventory.productnameen));
            cmd.Parameters.Add(genSqlParameter("productnamecn", SqlDbType.NVarChar, 10, inventory.productnamecn));
            cmd.Parameters.Add(genSqlParameter("description", SqlDbType.NVarChar, 255, inventory.description));
            cmd.Parameters.Add(genSqlParameter("dimension", SqlDbType.NVarChar, 50, inventory.dimension));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 50, inventory.unit));
            cmd.Parameters.Add(genSqlParameter("unitcost", SqlDbType.NVarChar, 50, inventory.unitcost));
            cmd.Parameters.Add(genSqlParameter("quantity", SqlDbType.NVarChar, 50, inventory.quantity));
            cmd.Parameters.Add(genSqlParameter("stored", SqlDbType.NVarChar, 50, inventory.stored));
            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, inventory.status));

            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }

        public bool delete(fpcore.Model.Inventory inventory, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(inventory, transaction);
        }

        public List<Inventory> List(string query, int limit, int start, string sortExpression, bool sortDirection, DbTransaction transaction)
        {
            if (sortExpression == "" || sortExpression == null)
                sortExpression = "UpdateDate";

            String orderby1 = sortExpression + (sortDirection ? " DESC" : " ASC");
            String orderby2 = sortExpression + (sortDirection ? " ASC" : " DESC");
          
            String sql =
                " SELECT * FROM (" +
                    " SELECT TOP " + limit + " * FROM ( " +
                        " SELECT TOP " + (limit + start) + " orderno, assetno,category,receiveddate,receivedby,orderdeadline,remark,contactperson,tel,productno,productnameen,productnamecn,description,dimension,unit,unitcost,quantity,stored,status, FPObject.*  " +
                        " FROM Inventory inner join FPObject on Inventory.ObjectId = FPObject.ObjectId " +
                            query +
                        " ORDER BY " + orderby1 + ") as foo " +
                    " ORDER by " + orderby2 + ") as bar " +
                    " ORDER by " + orderby1;

            SqlTransaction trans = (SqlTransaction)transaction;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<Inventory> inventories = getQueryResult(cmd);

            cmd.Dispose();

            return inventories;
        }

        private List<Inventory> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();


            //  IPrintItemDAO printItemDAO = DAOFactory.getInstance().createPrintJobDAO();
            IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
            IConsumptionDAO  consumptionDAO = DAOFactory.getInstance().createConsumptionDAO();

            List<Inventory> inventories = new List<Inventory>();
            Inventory inventory = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inventory = new Inventory();

                    inventory.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    inventory.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    inventory.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    inventory.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    inventory.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    inventory.assetno = getString(dt.Rows[i]["assetno"]);
                    inventory.category = getString(dt.Rows[i]["category"]);
                    inventory.contactperson = getString(dt.Rows[i]["contactperson"]);
                    inventory.description = getString(dt.Rows[i]["description"]);
                    inventory.dimension = getString(dt.Rows[i]["dimension"]);
                    inventory.orderdeadline = getDateTime(dt.Rows[i]["orderdeadline"]);
                    inventory.orderno = getString(dt.Rows[i]["orderno"]);
                    inventory.productnamecn = getString(dt.Rows[i]["productnamecn"]);
                    inventory.productnameen = getString(dt.Rows[i]["productnameen"]);
                    inventory.productno = getString(dt.Rows[i]["productno"]);
                    inventory.quantity = getString(dt.Rows[i]["quantity"]);
                    inventory.receivedby = userDAO.get(getInt(dt.Rows[i]["receivedby"]), cmd.Transaction);
                    inventory.receiveddate = getDateTime(dt.Rows[i]["receiveddate"]);
                    inventory.remark = getString(dt.Rows[i]["remark"]);
                    inventory.stored = consumptionDAO.cateStoredCount(getInt(dt.Rows[i]["ObjectId"]), cmd.Transaction).ToString();
                    inventory.Tel = getString(dt.Rows[i]["Tel"]);
                    inventory.unit = getString(dt.Rows[i]["unit"]);
                    inventory.unitcost = getString(dt.Rows[i]["unitcost"]);
                    inventory.status = getString(dt.Rows[i]["status"]);
                    inventories.Add(inventory);
                }
            }
            return inventories;
        }

        #endregion
    }
}
