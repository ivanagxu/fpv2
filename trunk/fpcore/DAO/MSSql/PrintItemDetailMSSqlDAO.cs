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
    public class PrintItemDetailMSSqlDAO : BaseDAO , IPrintItemDetailDAO
    {
        public List<PrintItemDetail> search(String query, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select jid, code_desc, category_name,category ,ordering from Print_Item_Detail " + query;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;
            List<PrintItemDetail> items = getQueryResult(cmd);
            cmd.Dispose();
            return items;
        }
        public List<String> getItemNamesByCategoryId(String id, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select distinct convert(	int,substring (		category, 2 , len(category)	)) as category from Print_Item_Detail where category like '" + id + "%' order by convert(	int,	substring (		category, 2 , len(category)	)) asc ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;
            cmd.Connection = trans.Connection;


            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(reader);
            reader.Close();

            List<String> items = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(id + getInt(dt.Rows[i]["category"]));
            }

            cmd.Dispose();
            return items;
        }
        public PrintItemDetail get(string id, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<PrintItemDetail> lookups = search(" where jid = '" + id + "'", trans);
            if (lookups != null && lookups.Count > 0)
                return lookups[0];
            else
                return null;
        }
        private List<PrintItemDetail> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            List<PrintItemDetail> items = new List<PrintItemDetail>();
            PrintItemDetail item = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new PrintItemDetail();
                    item.category = getString(dt.Rows[i]["category"]);
                    item.code_desc = getString(dt.Rows[i]["code_desc"]);
                    item.jid = getInt(dt.Rows[i]["jid"]);
                    item.ordering = "" + getInt(dt.Rows[i]["ordering"]);
                    item.category_name = getString(dt.Rows[i]["category_name"]);
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
