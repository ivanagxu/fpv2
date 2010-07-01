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
    public class DepartmentMSSqlDAO:BaseDAO , IDepartmentDAO
    {
        public bool add(Department dept, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(dept, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Department(ObjectId, name, other) values " +
                "(@ObjectId, @name, @other)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, dept.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, dept.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, dept.other));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool delete(Department dept, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(dept, transaction);
        }

        public bool update(Department dept, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(dept, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Department set name=@name, other=@other where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, dept.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, dept.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, dept.other));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public Department get(int objId, DbTransaction transaction)
        {
            List<Department> departments = search("where FPObject.ObjectId = " + objId + " and IsDeleted = 0", transaction);
            if (departments.Count > 0)
                return departments[0];
            else
                return null;
        }

        public List<Department> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select name, other , FPObject.* from Department inner join FPObject on Department.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<Department> departments = getQueryResult(cmd);

            cmd.Dispose();
            return departments;
        }

        private List<Department> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<Department> departments = new List<Department>();
            Department department = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    department = new Department();
                    department.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    department.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    department.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    department.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    department.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    department.name = getString(dt.Rows[i]["name"]);
                    department.other = getString(dt.Rows[i]["other"]);
                    departments.Add(department);
                }
            }
            return departments;
        }
    }
}
