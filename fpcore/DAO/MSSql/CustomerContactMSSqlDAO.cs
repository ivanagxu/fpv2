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
    public class CustomerContactMSSqlDAO : BaseDAO, ICustomerContactDAO
    {
        public CustomerContact get(int objId, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<CustomerContact> contacts = search("where FPObject.ObjectId = '" + objId + "' and IsDeleted = 0 ", trans);
            if (contacts != null && contacts.Count > 0)
            {
                return contacts[0];
            }
            else
            {
                return null;
            }
        }

        public CustomerContact getCustomerContactByCode(string customerCode,string ctype,DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<CustomerContact> contacts = search("where Customer_Contact.cid = '" + customerCode.Trim() + "' and IsDeleted = 0 and ctype='" + ctype + "' ", trans);
            if (contacts != null && contacts.Count > 0)
            {
                return contacts[0];
            }
            else
            {
                return null;
            }
        }

        public bool add(CustomerContact cc, DbTransaction transaction)
        {

            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(cc, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Customer_Contact(ObjectId, cid, contact_person,tel,address,email,fax,ctype) values " +
                "(@ObjectId, @cid,@contact_person,@tel,@address,@email,@fax,@ctype)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, cc.objectId));
            cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.NVarChar, 50, cc.customer.company_code));
            cmd.Parameters.Add(genSqlParameter("contact_person", SqlDbType.NVarChar, 255, cc.contact_person));
            cmd.Parameters.Add(genSqlParameter("tel", SqlDbType.NVarChar, 50, cc.tel));
            cmd.Parameters.Add(genSqlParameter("address", SqlDbType.NVarChar,50,cc.address));
            cmd.Parameters.Add(genSqlParameter("email", SqlDbType.NVarChar, 50,cc.email));
            cmd.Parameters.Add(genSqlParameter("fax", SqlDbType.NVarChar, 50,cc.fax));
            cmd.Parameters.Add(genSqlParameter("ctype", SqlDbType.NVarChar, 50, cc.ctype));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }


        public bool delete(CustomerContact cc, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(cc, transaction);
        }

        public bool update(CustomerContact cc, DbTransaction transaction)
        {

            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(cc, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Customer_Contact set cid=@cid, contact_person=@contact_person,tel=@tel,address=@address,email=@email,fax=@fax,ctype=@ctype " +
                " where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, cc.objectId));
            cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.NVarChar, 50, cc.customer.company_code));
            cmd.Parameters.Add(genSqlParameter("contact_person", SqlDbType.NVarChar, 255, cc.contact_person));
            cmd.Parameters.Add(genSqlParameter("tel", SqlDbType.NVarChar, 50, cc.tel));
            cmd.Parameters.Add(genSqlParameter("address", SqlDbType.NVarChar, 50, cc.address));
            cmd.Parameters.Add(genSqlParameter("email", SqlDbType.NVarChar, 50, cc.email));
            cmd.Parameters.Add(genSqlParameter("fax", SqlDbType.NVarChar, 50, cc.fax));
            cmd.Parameters.Add(genSqlParameter("ctype", SqlDbType.NVarChar, 50, cc.ctype));
            cmd.ExecuteNonQuery();

            cmd.Dispose();

            return true;
        }

        public int count(String condition, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select count(*) as total from Customer_Contact inner join FPObject on Customer_Contact.ObjectId = FPObject.ObjectId " + condition;
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


        public List<CustomerContact> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select cid, contact_person, tel, address, email, fax,ctype, FPObject.* from Customer_Contact inner join FPObject on Customer_Contact.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<CustomerContact> contacts = getQueryResult(cmd);

            cmd.Dispose();
            return contacts;
        }

       

        private List<CustomerContact> getQueryResult(SqlCommand cmd)
        {

            ICustomerDAO customerDAO = DAOFactory.getInstance().createCustomerDAO();
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<CustomerContact> contacts = new List<CustomerContact>();
            CustomerContact contact = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    contact = new CustomerContact();

                    contact.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    contact.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    contact.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    contact.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    contact.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);

                    contact.customer = customerDAO.get(getString(dt.Rows[i]["cid"]),cmd.Transaction);
                    contact.contact_person = getString(dt.Rows[i]["contact_person"]);
                    contact.tel = getString(dt.Rows[i]["tel"]);
                    contact.address = getString(dt.Rows[i]["address"]);
                    contact.email = getString(dt.Rows[i]["email"]);
                    contact.fax = getString(dt.Rows[i]["fax"]);
                    contact.ctype = getString(dt.Rows[i]["ctype"]);
                    contacts.Add(contact);
                }
            }
            return contacts;
        }
    }
}
