using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using fpcore.Model;
using System.Data.SqlClient;
using System.Data;

namespace fpcore.DAO.MSSql
{
    public class PrintJobCategoryMSSqlDAO : BaseDAO, IPrintJobCategoryDAO
    {
        public PrintJobCategory get(String id, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<PrintJobCategory> categorys = search(" where id = '" + id + "' and IsDeleted = 0", trans);
            if (categorys != null && categorys.Count > 0)
                return categorys[0];
            else
                return null;
        }
        public List<PrintJobCategory> search(String query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select id, category_name, category_code, FPObject.* from Print_Job_Category inner join FPObject on Print_Job_Category.ObjectId = FPObject.ObjectId " + query;
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<PrintJobCategory> categorys = getQueryResult(cmd);
            cmd.Dispose();
            return categorys;
        }

        private List<PrintJobCategory> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<PrintJobCategory> categorys = new List<PrintJobCategory>();
            PrintJobCategory category = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    category = new PrintJobCategory();
                    category.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    category.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    category.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    category.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    category.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    category.category_code = getString(dt.Rows[i]["category_code"]);
                    category.category_name = getString(dt.Rows[i]["category_name"]);
                    category.id = getString(dt.Rows[i]["id"]);
                    categorys.Add(category);
                }
            }
            return categorys;
        }
    }
}
