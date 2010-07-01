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
    public class SectionMSSqlDAO : BaseDAO, ISectionDAO
    {
        public bool add(Section section, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(section, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Section(ObjectId, name, other, dept) values " +
                "(@ObjectId, @name, @other, @dept)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, section.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, section.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, section.other));

            if(section == null)
                cmd.Parameters.Add(genSqlParameter("other", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("other", SqlDbType.Int, 10, section.dept.objectId));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool delete(Section section, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            return fpObjectDAO.delete(section, transaction);
        }

        public bool update(Section section, DbTransaction transaction)
        {
            IFPObjectDAO fpObjectDAO = DAOFactory.getInstance().createFPObjectDAO();
            fpObjectDAO.add(section, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Section set name=@name, other=@other, dept=@dept where ObjectId = @ObjectId";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, section.objectId));
            cmd.Parameters.Add(genSqlParameter("name", SqlDbType.NVarChar, 255, section.name));
            cmd.Parameters.Add(genSqlParameter("other", SqlDbType.NVarChar, 255, section.other));

            if (section == null)
                cmd.Parameters.Add(genSqlParameter("other", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("other", SqlDbType.Int, 10, section.dept.objectId));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public Section get(int objId, DbTransaction transaction)
        {
            List<Section> sections = search("where FPObject.ObjectId = " + objId + " and IsDeleted = 0", transaction);
            if (sections.Count > 0)
                return sections[0];
            else
                return null;
        }

        public List<Section> search(string query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select name, other , dept,  FPObject.* from Section inner join FPObject on Section.ObjectId = FPObject.ObjectId " + query;

            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<Section> sections = getQueryResult(cmd);

            cmd.Dispose();
            return sections;
        }

        private List<Section> getQueryResult(SqlCommand cmd)
        {

            IDepartmentDAO deptDAO = DAOFactory.getInstance().createDepartmentDAO();
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<Section> sections = new List<Section>();
            Section section = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    section = new Section();
                    section.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    section.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    section.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    section.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    section.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    section.name = getString(dt.Rows[i]["name"]);
                    section.other = getString(dt.Rows[i]["other"]);
                    section.dept = deptDAO.get(getInt(dt.Rows[i]["dept"]), cmd.Transaction);

                    sections.Add(section);
                }
            }
            return sections;
        }
    }
}
