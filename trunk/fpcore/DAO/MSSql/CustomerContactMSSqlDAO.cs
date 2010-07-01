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

        public List<CustomerContact> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select cid, contact_person, tel, address, email, fax, FPObject.* from Customer_Contact inner join FPObject on Customer_Contact.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<CustomerContact> contacts = getQueryResult(cmd);

            cmd.Dispose();
            return contacts;
        }

        public bool add(CustomerContact contact, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.add(contact,transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Customer_Contact(ObjectId, cid, contact_person, tel, address , email, fax) values (@ObjectId, @cid, @contact_person, @tel, @address ,@email, @fax)";

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, contact.objectId));

            if(contact.customer != null)
                cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.NVarChar, 50, contact.customer.company_code));
            else
                cmd.Parameters.Add(genSqlParameter("cid", SqlDbType.NVarChar, 50, null));
            cmd.Parameters.Add(genSqlParameter("contact_person", SqlDbType.NVarChar, 255, contact.contact_person));
            cmd.Parameters.Add(genSqlParameter("tel", SqlDbType.NVarChar, 50, contact.tel));
            cmd.Parameters.Add(genSqlParameter("address", SqlDbType.NVarChar, 255, contact.address));
            cmd.Parameters.Add(genSqlParameter("email", SqlDbType.NVarChar, 50, contact.email));
            cmd.Parameters.Add(genSqlParameter("fax", SqlDbType.NVarChar, 50, contact.fax));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
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
                    contacts.Add(contact);
                }
            }
            return contacts;
        }
    }
}
