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
    public class FPRoleMSSqlDAO : BaseDAO , IFPRoleDAO
    {
        public bool add(FPRole role, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(role, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into FPRole(ObjectId, name, other) values " +
                "(@ObjectId, @name, @other)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, role.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, role.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, role.other));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool update(FPRole role, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(role, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update FPRole set name=@name, other=@other where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, role.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, role.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, role.other));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool delete(FPRole role, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(role, transaction);
        }

        public FPRole get(int objId, DbTransaction transaction)
        {
            List<FPRole> roles = search("where FPObject.ObjectId = " + objId + " and IsDeleted = 0", transaction);
            if (roles.Count > 0)
                return roles[0];
            else
                return null;
        }

        public List<FPRole> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select name, other , FPObject.* from FPRole inner join FPObject on FPRole.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<FPRole> roles = getQueryResult(cmd);

            cmd.Dispose();
            return roles;
        }

        public List<FPRole> getRoleByUser(UserAC user, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select name, other , FPObject.* from FPRole ,FPObject, UserAC , UserRole where FPRole.ObjectId = FPObject.ObjectId and FPObject.IsDeleted = 0 and UserAC.ObjectID = UserRole.usr and FPRole.ObjectId = UserRole.role and UserAC.ObjectId='"+user.objectId +"'";
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<FPRole> roles = getQueryResult(cmd);

            cmd.Dispose();
            return roles;
        }

        private List<FPRole> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            IUserDAO userDao = DAOFactory.getInstance().createUserDAO();

            List<FPRole> roles = new List<FPRole>();
            FPRole role = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    role = new FPRole();
                    role.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    role.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    role.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    role.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    role.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    role.name = getString(dt.Rows[i]["name"]);
                    role.other = getString(dt.Rows[i]["other"]);
                 //   role.userList = userDao.getUserByRole(role, cmd.Transaction);
                    roles.Add(role);
                }
            }
            return roles;
        }



        


    }
}
