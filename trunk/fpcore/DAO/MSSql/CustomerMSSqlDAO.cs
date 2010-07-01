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
    public class CustomerMSSqlDAO : BaseDAO, ICustomerDAO
    {
        public Customer get(String company_code, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<Customer> customers = search(" where company_code = '" + company_code + "' and IsDeleted = 0 ", 1, 1, "", false, trans);

            if (customers != null && customers.Count > 0)
            {
                return customers[0];
            }
            else 
            {
                return null;
            }
        }

        public List<Customer> search(string query,int limit, int start, String sort, bool descending, DbTransaction transaction)
        {
            if (sort == "" || sort == null)
                sort = "ObjectId";

            String orderby1 = sort + (descending ? " DESC" : " ASC");
            String orderby2 = sort + (descending ? " ASC" : " DESC");

            String sql =
                " SELECT * FROM ( " +
                    " SELECT TOP " + limit + " * FROM ( " +
                        " SELECT TOP " + (limit + start) + " company_name,company_code,FPObject.* " +
                        " FROM Customer inner join FPObject on Customer.ObjectId = FPObject.ObjectId " +
                            query +
                        " ORDER BY " + orderby1 + ") as foo " +
                    " ORDER by " + orderby2 + ") as bar " +
                    " ORDER by " + orderby1;


            SqlTransaction trans = (SqlTransaction)transaction;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<Customer> customers = getQueryResult(cmd);
            cmd.Dispose();
            return customers;
        }

        public bool add(Customer customer, DbTransaction transaction)
        {

            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(customer,transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Customer(ObjectId, company_name, company_code) values " +
                "(@ObjectId, @cid, @company_name, @company_code)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, customer.objectId));
            cmd.Parameters.Add(genSqlParameter("company_code", SqlDbType.NVarChar, 255, customer.company_code));
            cmd.Parameters.Add(genSqlParameter("company_name", SqlDbType.NVarChar, 255, customer.company_name));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool update(Customer customer, DbTransaction transaction)
        {

            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(customer, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update customer set company_name = @company_name, company_code = @company_code , " +
                " where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, customer.objectId));
            cmd.Parameters.Add(genSqlParameter("company_code", SqlDbType.NVarChar, 255, customer.company_code));
            cmd.Parameters.Add(genSqlParameter("company_name", SqlDbType.NVarChar, 255, customer.company_name));
            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }

        public bool delete(Customer customer, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(customer, transaction);
        }



        private List<Customer> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<Customer> customers = new List<Customer>();
            Customer customer = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    customer = new Customer();
                    customer.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    customer.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    customer.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    customer.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    customer.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    customer.company_code = getString(dt.Rows[i]["company_code"]);
                    customer.company_name = getString(dt.Rows[i]["company_name"]);
                    customers.Add(customer);
                }
            }
            return customers;
        }



    }
}
