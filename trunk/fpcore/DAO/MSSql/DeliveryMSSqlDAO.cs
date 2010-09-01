using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace fpcore.DAO.MSSql
{
    public class DeliveryMSSqlDAO:BaseDAO,IDeliveryDAO
    {
        #region IDeliveryDAO 成员

        public fpcore.Model.Delivery Get(int objId, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<Delivery> deliveries = List("where FPObject.ObjectId = '" + objId + "' and IsDeleted = 0 ", 1, 1, string.Empty, false, trans);
            if (deliveries != null && deliveries.Count > 0)
            {
                return deliveries[0];
            }
            else
            {
                return null;
            }
        }

        public bool Add(fpcore.Model.Delivery delivery, System.Data.Common.DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(delivery, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into delivery(ObjectId, number, non_order_num,part_no,length,width,height,weight,delivery_type,requested_by,handled_by,notes,assigned_by,deadline,status,contact,remarks,cid) values " +
                "(@ObjectId,@number, @non_order_num,@part_no,@length,@width,@height,@weight,@delivery_type,@requested_by,@handled_by,@notes,@assigned_by,@deadline,@status,@contact,@remarks,@cid)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, delivery.objectId));
            cmd.Parameters.Add(genSqlParameter("number", SqlDbType.NVarChar, 50, delivery.number));
            cmd.Parameters.Add(genSqlParameter("non_order_num", SqlDbType.NVarChar, 50, delivery.non_order));


            cmd.Parameters.Add(genSqlParameter("part_no", SqlDbType.VarChar, 50, delivery.part_no));
            cmd.Parameters.Add(genSqlParameter("length", SqlDbType.NVarChar, 50, delivery.length));
            cmd.Parameters.Add(genSqlParameter("width", SqlDbType.NVarChar, 50, delivery.width));
            cmd.Parameters.Add(genSqlParameter("remarks", SqlDbType.NVarChar, 1000, delivery.remarks));

            cmd.Parameters.Add(genSqlParameter("height", SqlDbType.VarChar, 50, delivery.height));
            cmd.Parameters.Add(genSqlParameter("weight", SqlDbType.NVarChar, 50, delivery.weight));
            cmd.Parameters.Add(genSqlParameter("delivery_type", SqlDbType.NVarChar, 50, delivery.delivery_type));

            int? requested_by = null;
            if (delivery.requested_by != null)
                requested_by = delivery.requested_by.objectId;
            int? handled_by = null;
            if (delivery.handled_by != null)
                handled_by = delivery.handled_by.objectId;
            int? assigned_by = null;
            if (delivery.assigned_by != null)
                assigned_by = delivery.assigned_by.objectId;
            int? contact = null;
            if (delivery.contact != null)
                contact = delivery.contact.objectId;
            int? cid = null;
            if (delivery.customer != null)
                cid = delivery.customer.objectId;

            cmd.Parameters.Add(genSqlParameter("requested_by", SqlDbType.Int, 10, requested_by));
            cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 50, handled_by));
            cmd.Parameters.Add(genSqlParameter("notes", SqlDbType.NVarChar, 50, delivery.notes));

            cmd.Parameters.Add(genSqlParameter("assigned_by", SqlDbType.Int, 10, assigned_by));
            cmd.Parameters.Add(genSqlParameter("contact", SqlDbType.Int, 10, contact));
            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, delivery.status));
            cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.Int, 10, cid));
            cmd.Parameters.Add(genSqlParameter("deadline", SqlDbType.DateTime, 0, delivery.deadline));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }

        public bool Update(fpcore.Model.Delivery delivery, System.Data.Common.DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(delivery, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Delivery set number=@number,remarks=@remarks, non_order_num=@non_order_num,part_no=@part_no,length=@length,width=@width,height=@height,weight=@weight,delivery_type=@delivery_type,requested_by=@requested_by,handled_by=@handled_by,notes=@notes,assigned_by=@assigned_by,deadline=@deadline,status=@status,contact=@contact,cid=@cid where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, delivery.objectId));
            cmd.Parameters.Add(genSqlParameter("number", SqlDbType.NVarChar, 50, delivery.number));
            cmd.Parameters.Add(genSqlParameter("non_order_num", SqlDbType.NVarChar, 50, delivery.non_order));


            cmd.Parameters.Add(genSqlParameter("part_no", SqlDbType.VarChar, 50, delivery.part_no));
            cmd.Parameters.Add(genSqlParameter("length", SqlDbType.NVarChar, 50, delivery.length));
            cmd.Parameters.Add(genSqlParameter("width", SqlDbType.NVarChar, 50, delivery.width));
            cmd.Parameters.Add(genSqlParameter("remarks", SqlDbType.NVarChar, 1000, delivery.remarks));

            cmd.Parameters.Add(genSqlParameter("height", SqlDbType.VarChar, 50, delivery.height));
            cmd.Parameters.Add(genSqlParameter("weight", SqlDbType.NVarChar, 50, delivery.weight));
            cmd.Parameters.Add(genSqlParameter("delivery_type", SqlDbType.NVarChar, 50, delivery.delivery_type));

            int? requested_by = null;
            if (delivery.requested_by != null)
                requested_by = delivery.requested_by.objectId;
            int? handled_by = null;
            if (delivery.handled_by != null)
                handled_by = delivery.handled_by.objectId;
            int? assigned_by = null;
            if (delivery.assigned_by != null)
                assigned_by = delivery.assigned_by.objectId;
            int? contact = null;
            if (delivery.contact != null)
                contact = delivery.contact.objectId;

            int? cid=null ;
            if(delivery.customer!=null )
                cid=delivery.customer.objectId ;


            cmd.Parameters.Add(genSqlParameter("requested_by", SqlDbType.Int, 10, requested_by));
            cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 50, handled_by));
            cmd.Parameters.Add(genSqlParameter("notes", SqlDbType.NVarChar, 50, delivery.notes));

            cmd.Parameters.Add(genSqlParameter("assigned_by", SqlDbType.Int, 10, assigned_by));
            cmd.Parameters.Add(genSqlParameter("contact", SqlDbType.Int, 10, contact));
            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, delivery.status));

            cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.Int, 10, cid));

            cmd.Parameters.Add(genSqlParameter("deadline", SqlDbType.DateTime, 0, delivery.deadline));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            return true;
        }

        public bool delete(Delivery delivery, System.Data.Common.DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(delivery, transaction);
        }

        public int count(string condition, System.Data.Common.DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select count(*) as total from delivery inner join FPObject on delivery.ObjectId = FPObject.ObjectId " + condition;
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

        public List<fpcore.Model.Delivery> List(string query, int limit, int start, string sortExpression, bool sortDirection, System.Data.Common.DbTransaction transaction)
        {
            if (sortExpression == "" || sortExpression == null)
                sortExpression = "UpdateDate";

            String orderby1 = sortExpression + (sortDirection ? " DESC" : " ASC");
            String orderby2 = sortExpression + (sortDirection ? " ASC" : " DESC");
         //   Delivery delivery = new Delivery();
           
            String sql =
                " SELECT * FROM (" +
                    " SELECT TOP " + limit + " * FROM ( " +
                        " SELECT TOP " + (limit + start) + " delivery.assigned_by ,delivery.contact,delivery.deadline,delivery.delivery_type,delivery.handled_by,delivery.height,delivery.length,delivery.non_order_num,delivery.notes,delivery.number,delivery.part_no,delivery.requested_by,delivery.status,delivery.weight,delivery.width,delivery.remarks,cid, FPObject.*  " +
                        " FROM Delivery inner join FPObject on Delivery.ObjectId = FPObject.ObjectId " +
                            query +
                        " ORDER BY " + orderby1 + ") as foo " +
                    " ORDER by " + orderby2 + ") as bar " +
                    " ORDER by " + orderby1;

            SqlTransaction trans = (SqlTransaction)transaction;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<Delivery> deiliveries = getQueryResult(cmd);

            cmd.Dispose();

            return deiliveries;
        }

        #endregion

        #region private

        private List<Delivery> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

        
          //  IPrintItemDAO printItemDAO = DAOFactory.getInstance().createPrintJobDAO();
            IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
            ICustomerContactDAO contactDAO = DAOFactory.getInstance().createCustomerContactDAO();
            ICustomerDAO customerDAO = DAOFactory.getInstance().createCustomerDAO();
            List<Delivery> deliveries = new List<Delivery>();
            Delivery delivery = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    delivery = new Delivery();

                    delivery.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    delivery.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    delivery.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    delivery.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    delivery.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    delivery.assigned_by = userDAO.get(getInt(dt.Rows[i]["assigned_by"]), cmd.Transaction);
                    delivery.contact = contactDAO.get(getInt(dt.Rows[i]["contact"]), cmd.Transaction);
                    delivery.deadline = getDateTime(dt.Rows[i]["deadline"]);
                    delivery.delivery_type = getString(dt.Rows[i]["delivery_type"]);
                    delivery.handled_by = userDAO.get(getInt(dt.Rows[i]["handled_by"]), cmd.Transaction);
                    delivery.height = getString(dt.Rows[i]["height"]);
                    delivery.length = getString(dt.Rows[i]["length"]);
                    delivery.non_order = getString(dt.Rows[i]["non_order_num"]);
                    delivery.notes = getString(dt.Rows[i]["notes"]);

                    delivery.number = getString(dt.Rows[i]["number"]);
                    delivery.part_no = getString(dt.Rows[i]["part_no"]);
                    delivery.requested_by = userDAO.get(getInt(dt.Rows[i]["requested_by"]), cmd.Transaction);
                    delivery.status = getString(dt.Rows[i]["status"]);

                    delivery.weight = getString(dt.Rows[i]["weight"]);
                    delivery.width = getString(dt.Rows[i]["width"]);
                    delivery.remarks = getString(dt.Rows[i]["remarks"]);

                    delivery.customer = customerDAO.getByID(getInt(dt.Rows[i]["cid"]), cmd.Transaction);
                    deliveries.Add(delivery);
                }
            }
            return deliveries;
        }

        #endregion
    }
}
