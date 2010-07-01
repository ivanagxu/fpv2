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
    public class PrintOrderMSSqlDAO : BaseDAO, IPrintOrderDAO
    {
        public bool add(PrintOrder order, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(order, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Print_Order(ObjectId, pid, received_date, order_deadline, invoice_no, contact_id, received_by, sales_person, remarks, status) values " +
                "(@ObjectId, @pid, @received_date, @order_deadline, @invoice_no, @contact_id, @received_by, @sales_person, @remarks, @status)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = trans.Connection;
            cmd.Transaction = trans;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, order.objectId));
            cmd.Parameters.Add(genSqlParameter("pid", SqlDbType.NVarChar, 50, order.pid));
            cmd.Parameters.Add(genSqlParameter("received_date", SqlDbType.DateTime, 0, order.received_date));
            cmd.Parameters.Add(genSqlParameter("order_deadline", SqlDbType.NVarChar, 50, order.order_deadline));
            cmd.Parameters.Add(genSqlParameter("invoice_no", SqlDbType.NVarChar, 50, order.invoice_no));

            if(order.received_by == null)
                cmd.Parameters.Add(genSqlParameter("received_by", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("received_by", SqlDbType.Int, 10, order.received_by.objectId));
            if(order.sales_person == null)
                cmd.Parameters.Add(genSqlParameter("sales_person", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("sales_person", SqlDbType.Int, 10, order.sales_person.objectId));

            cmd.Parameters.Add(genSqlParameter("remarks", SqlDbType.NVarChar, 2000, order.remarks));

            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, order.status));

            if(order.customer_contact != null)
                cmd.Parameters.Add(genSqlParameter("contact_id", SqlDbType.Int, 10, order.customer_contact.objectId));
            else
                cmd.Parameters.Add(genSqlParameter("contact_id", SqlDbType.Int, 10, null));
            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }


        public bool update(PrintOrder order, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(order, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Print_Order set received_date = @received_date, order_deadline = @order_deadline, " +
                "invoice_no = @invoice_no, contact_id = @contact_id , received_by = @received_by, sales_person = @sales_person, remarks = @remarks , status = @status where pid = @pid";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = trans.Connection;
            cmd.Transaction = trans;
            cmd.Parameters.Add(genSqlParameter("pid", SqlDbType.NVarChar, 50, order.pid));
            cmd.Parameters.Add(genSqlParameter("received_date", SqlDbType.DateTime, 0, order.received_date));
            cmd.Parameters.Add(genSqlParameter("order_deadline", SqlDbType.NVarChar, 50, order.order_deadline));
            cmd.Parameters.Add(genSqlParameter("invoice_no", SqlDbType.NVarChar, 50, order.invoice_no));
            if (order.received_by == null)
                cmd.Parameters.Add(genSqlParameter("received_by", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("received_by", SqlDbType.Int, 10, order.received_by.objectId));
            if (order.sales_person == null)
                cmd.Parameters.Add(genSqlParameter("sales_person", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("sales_person", SqlDbType.Int, 10, order.sales_person.objectId));

            cmd.Parameters.Add(genSqlParameter("remarks", SqlDbType.NVarChar, 2000, order.remarks));

            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, order.status));

            if (order.customer_contact != null)
                cmd.Parameters.Add(genSqlParameter("contact_id", SqlDbType.Int, 10, order.customer_contact.objectId));
            else
                cmd.Parameters.Add(genSqlParameter("contact_id", SqlDbType.Int, 10, null));

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }

        public bool delete(PrintOrder order, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(order, transaction);
        }

        public PrintOrder get(string id, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<PrintOrder> orders = search(" where pid = '" + id + "' and IsDeleted = 0 ",1,1, "" , false , trans);
            if (orders != null && orders.Count > 0)
                return orders[0];
            else
                return null;
        }

        public List<PrintOrder> search(string query, int limit, int start, String sort, bool descending, DbTransaction transaction)
        {
            if (sort == "" || sort == null)
                sort = "pid";

            String orderby1 = sort + (descending ? " DESC" : " ASC");
            String orderby2 = sort + (descending ? " ASC" : " DESC");

            String sql =
                " SELECT * FROM (" +
                    " SELECT TOP " + limit + " * FROM ( " +
                        " SELECT TOP " + (limit + start) + " pid, received_date, order_deadline, invoice_no, contact_id, received_by, sales_person, remarks, status, FPObject.*  " +
                        " FROM Print_Order inner join FPObject on Print_Order.ObjectId = FPObject.ObjectId " +
                            query +
                        " ORDER BY " + orderby1 + ") as foo " +
                    " ORDER by " + orderby2 + ") as bar " +
                    " ORDER by " + orderby1;

            SqlTransaction trans = (SqlTransaction)transaction;
            //String sql = "select pid, received_date, modified_date, order_deadline, invoice_no, cid, received_by, sales_person, remarks, customer_name, order_finish, customer_tel, customer_contact_person from Print_Order " + query;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<PrintOrder> orders = getQueryResult(cmd);

            cmd.Dispose();

            return orders;
        }

        public int count(String condition, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select count(*) as total from Print_Order inner join FPObject on Print_Order.ObjectId = FPObject.ObjectId " + condition;
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

        private List<PrintOrder> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            IPrintItemDAO printItemDAO = DAOFactory.getInstance().createPrintJobDAO();
            IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
            ICustomerContactDAO contactDAO = DAOFactory.getInstance().createCustomerContactDAO();

            List<PrintOrder> orders = new List<PrintOrder>();
            PrintOrder order = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    order = new PrintOrder();

                    order.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    order.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    order.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    order.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    order.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    order.invoice_no = getString(dt.Rows[i]["invoice_no"]);
                    order.order_deadline = getString(dt.Rows[i]["order_deadline"]);
                    order.pid = getString(dt.Rows[i]["pid"]);
                    order.received_by = userDAO.get(getInt(dt.Rows[i]["received_by"]),cmd.Transaction);
                    order.received_date = getDateTime(dt.Rows[i]["received_date"]);
                    order.remarks = getString(dt.Rows[i]["remarks"]);
                    order.sales_person = userDAO.get(getInt(dt.Rows[i]["sales_person"]),cmd.Transaction);
                    order.status = getString(dt.Rows[i]["status"]);
                    order.print_job_list = printItemDAO.search(" where pid = '" + order.pid + "'", 1000, 0, "", false, cmd.Transaction);
                    order.customer_contact = contactDAO.get(getInt(dt.Rows[i]["contact_id"]), cmd.Transaction);
                    orders.Add(order);
                }
            }
            return orders;
        }
    }
}
