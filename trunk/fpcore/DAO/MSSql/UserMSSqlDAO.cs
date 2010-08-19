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
    public class UserMSSqlDAO : BaseDAO, IUserDAO
    {
        public UserAC get(int objId, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<UserAC> users = search("where FPObject.ObjectId = '" + objId + "' and IsDeleted = 0 ", trans);
            if (users != null && users.Count > 0)
            {
                return users[0];
            }
            else
            {
                return null;
            }
        }
        public List<UserAC> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select chi_name, eng_name, user_name, dept,section , user_password ,email,post,status,remark, FPObject.* from UserAC inner join FPObject on UserAC.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<UserAC> users = getQueryResult(cmd);

            cmd.Dispose();
            return users;
        }
        public bool add(UserAC user, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(user, transaction);


            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into UserAC(ObjectId, chi_name, eng_name, user_name, dept, user_password,post,email,remark,status) values " +
                "(@ObjectId, @chi_name, @eng_name, @user_name, @dept, @user_password,@post,@email,@remark,@status)";

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            int? dept=null ;
            if(user.dept !=null )
                dept =user.dept.objectId;
            int? section = null;
            if (user.section != null)
                section = user.section.objectId;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, user.objectId));
            cmd.Parameters.Add(genSqlParameter("chi_name", SqlDbType.NVarChar, 50, user.chi_name));
            cmd.Parameters.Add(genSqlParameter("eng_name", SqlDbType.NVarChar, 50, user.eng_name));
            cmd.Parameters.Add(genSqlParameter("user_name", SqlDbType.NVarChar, 50, user.user_name));
            cmd.Parameters.Add(genSqlParameter("dept",SqlDbType.Int,10,dept));
            cmd.Parameters.Add(genSqlParameter("section", SqlDbType.Int, 10, section));
            cmd.Parameters.Add(genSqlParameter("user_password",SqlDbType.NVarChar,50,user.user_password));

            cmd.Parameters.Add(genSqlParameter("post", SqlDbType.NVarChar, 50, user.post));
            cmd.Parameters.Add(genSqlParameter("email", SqlDbType.NVarChar, 50, user.email));
            cmd.Parameters.Add(genSqlParameter("remark", SqlDbType.NVarChar, 255, user.remark));
            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, user.status));

            cmd.ExecuteNonQuery();

            updateUserRole(user, transaction);

            cmd.Dispose();
            return true;
        }
        public bool delete(UserAC user, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.delete(user, transaction);
            return true;
        }
        public bool update(UserAC user, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.update(user, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update UserAC set chi_name = @chi_name, eng_name = @eng_name, user_name = @user_name, dept = @dept,section = @section, user_password = @user_password,post=@post,email=@email,remark=@remark,status=@status " +
                " where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;


            int? dept = null;
            if (user.dept != null)
                dept = user.dept.objectId;
            int? section = null;
            if (user.section != null)
                section = user.section.objectId;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, user.objectId));
            cmd.Parameters.Add(genSqlParameter("chi_name", SqlDbType.NVarChar, 50, user.chi_name));
            cmd.Parameters.Add(genSqlParameter("eng_name", SqlDbType.NVarChar, 50, user.eng_name));
            cmd.Parameters.Add(genSqlParameter("user_name", SqlDbType.NVarChar, 50, user.user_name));
            cmd.Parameters.Add(genSqlParameter("dept", SqlDbType.Int, 10,dept));
            cmd.Parameters.Add(genSqlParameter("section", SqlDbType.Int, 10, section));
            cmd.Parameters.Add(genSqlParameter("user_password", SqlDbType.NVarChar, 50, user.user_password));


            cmd.Parameters.Add(genSqlParameter("post", SqlDbType.NVarChar, 50, user.post));
            cmd.Parameters.Add(genSqlParameter("email", SqlDbType.NVarChar, 50, user.email));
            cmd.Parameters.Add(genSqlParameter("remark", SqlDbType.NVarChar, 255, user.remark));
            cmd.Parameters.Add(genSqlParameter("status", SqlDbType.NVarChar, 50, user.status));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            updateUserRole(user, transaction);

            return true;
        }

        public bool updateUserRole(UserAC user, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "delete UserRole where usr = " + user.objectId;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.ExecuteNonQuery();

            if (user.roles!=null)
            {
                for (int i = 0; i < user.roles.Count; i++)
                {
                    cmd.CommandText = "insert into UserRole(usr,role) values(" + user.objectId + "," + user.roles[i].objectId + ")";
                    cmd.ExecuteNonQuery();
                }
            }
            cmd.Dispose();
            return true;
        }


        public List<UserAC> getUserNotInRole(string roleID, DbTransaction transaction)
        {
            if (roleID == null)
                return new List<UserAC>();
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "SELECT UserAC.*, status,FPObject.*" +
" FROM         UserAC, FPObject "+
" WHERE UserAC.ObjectId = FPObject.ObjectId and fpobject.isdeleted=0 and (UserAC.ObjectId NOT IN" +
                         " (SELECT     usr "+
                          "  FROM          UserRole "+
                          "  WHERE      (role ='"+int.Parse(roleID)+"'))) ";

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<UserAC> users = getQueryResult(cmd);

            cmd.Dispose();
            return users;
        }

        public List<UserAC> getUserByRole(string roleID, DbTransaction transaction)
        {
            if(roleID == null)
                return new List<UserAC>();

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select UserAC.* , FPObject.* from FPRole ,FPObject, UserAC , UserRole where UserAC.ObjectId = FPObject.ObjectId and FPObject.IsDeleted = 0 and UserAC.ObjectID = UserRole.usr and FPRole.ObjectId = UserRole.role and FPRole.ObjectId='" + int.Parse(roleID) + "'";
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<UserAC> users = getQueryResult(cmd);

            cmd.Dispose();
            return users;
        }

        private List<UserAC> getQueryResult(SqlCommand cmd)
        {
            IFPRoleDAO roleDAO = DAOFactory.getInstance().createFPRoleDAO();
            ISectionDAO sectionDAO = DAOFactory.getInstance().createSectionDAO();
            IDepartmentDAO deptDAO = DAOFactory.getInstance().createDepartmentDAO();
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<UserAC> users = new List<UserAC>();
            UserAC user = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user = new UserAC();
                    user.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    user.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    user.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    user.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    user.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    user.chi_name = getString(dt.Rows[i]["chi_name"]);
                    user.eng_name = getString(dt.Rows[i]["eng_name"]);
                    user.user_name = getString(dt.Rows[i]["user_name"]);
                    user.user_password = getString(dt.Rows[i]["user_password"]);
                    user.dept = deptDAO.get(getInt(dt.Rows[i]["dept"]), cmd.Transaction);
                    user.section = sectionDAO.get(getInt(dt.Rows[i]["section"]), cmd.Transaction);

                    user.post= getString(dt.Rows[i]["post"]);
                    user.email = getString(dt.Rows[i]["email"]);
                    user.remark = getString(dt.Rows[i]["remark"]);
                    user.status = getString(dt.Rows[i]["status"]);

                    user.roles = roleDAO.getRoleByUser(user, cmd.Transaction);

                    users.Add(user);
                }
            }
            return users;
        }
    }
}
