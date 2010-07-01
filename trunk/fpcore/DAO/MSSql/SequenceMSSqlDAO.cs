using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using fpcore.Model;
using System.Data;
using System.Data.Common;

namespace fpcore.DAO.MSSql
{
    public class SequenceMSSqlDAO : BaseDAO , ISequenceDAO
    {
        public int getNextObjectId(DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = "update Sequences set SequenceValue = SequenceValue + 1 where SequenceName = 'ObjectId'";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select SequenceValue from Sequences where SequenceName = 'ObjectId'";
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            rdr.Close();

            cmd.Dispose();
            return getInt(dt.Rows[0]["SequenceValue"]);
        }
    }
}
