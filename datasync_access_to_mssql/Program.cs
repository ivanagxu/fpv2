using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

namespace datasync_access_to_mssql
{
    class Program
    {
        public static String connStrMdb = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=D:\\MyProject\\Fingerprint\\Program\\fingerprintv2\\fingerprintv2\\DB\\fingerprintdb.mdb";
        //String connStrMSSql = "Provider=SQLOLEDB;Data Source=(local)\\SQLEXPRESS;Initial Catalog=fp;Integrated Security=SSPI;";
        public static String connStrMSSql = "server=localhost\\SQLEXPRESS;user id=efiling;password=efiling;initial catalog=fp;Connect Timeout=45";

        public static OleDbConnection accessConn;
        public static SqlConnection msSqlConn;
        public static int ObjectId = 10000;
        public static SqlCommand msSqlCmd2 = new SqlCommand();

        static void Main(string[] args)
        {
            
            try
            {
                FileStream fs = new FileStream("connstr.txt", FileMode.Open);
                byte[] connstr = new byte[fs.Length];
                fs.Read(connstr, 0, (int)fs.Length);
                fs.Close();
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < connstr.Length; i++)
                    sb.Append((char)connstr[i]);


                connStrMdb = sb.ToString().Split('\n')[0].Replace("\r","");
                connStrMSSql = sb.ToString().Split('\n')[1].Replace("\r", "");

                accessConn = new OleDbConnection(connStrMdb);
                msSqlConn = new SqlConnection(connStrMSSql);

                accessConn.Open();
                msSqlConn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }

            OleDbCommand accessCmd = new OleDbCommand();
            accessCmd.Connection = accessConn;
            SqlCommand msSqlCmd = new SqlCommand();
            msSqlCmd.Connection = msSqlConn;
            msSqlCmd2.Connection = msSqlConn;
            msSqlCmd2.CommandText = "delete FPObject";
            msSqlCmd2.ExecuteNonQuery();
            msSqlCmd2.CommandText = "delete Sequences";
            msSqlCmd2.ExecuteNonQuery();

            migrateCustomer(accessCmd, msSqlCmd);
            migratePrint_Job_Category(accessCmd, msSqlCmd);
            migratePrintJob(accessCmd, msSqlCmd);
            migratePrintJob_Detail1(accessCmd, msSqlCmd);
            migratePrintJobLookUp(accessCmd, msSqlCmd);
            migratePrintOrder(accessCmd, msSqlCmd);
            migrateUserAC(accessCmd, msSqlCmd);

            msSqlCmd2.CommandText = "insert into Sequences values('ObjectId'," + ObjectId + ")";
            msSqlCmd2.ExecuteNonQuery();

            accessCmd.Dispose();
            msSqlCmd.Dispose();
            accessConn.Close();
            msSqlConn.Close();
        }

        public static int addObject()
        {
            ObjectId++;
            msSqlCmd2.CommandText = "insert into FPObject values(" + ObjectId + ",getDate(),getDate(),'admin',0)";
            msSqlCmd2.ExecuteNonQuery();
            return ObjectId;
        }

        public static void migrateCustomer(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Customer
            
            accessCmd.CommandText = "select * from Customer";
            msSqlCmd.CommandText = "delete Customer";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Customer(ObjectId,cid,company_name,contact_person,contact_tel,address) values(@ObjectId,@cid,@company_name,@contact_person,@contact_tel,@address)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("@cid", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["cid"];
                msSqlCmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["company_name"];
                msSqlCmd.Parameters.Add("@contact_person", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["contact_person"];
                msSqlCmd.Parameters.Add("@contact_tel", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["contact_tel"];
                msSqlCmd.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["address"];

                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();

        }

        public static void migratePrintJob(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_Job
            accessCmd.CommandText = "select * from Print_Job";
            msSqlCmd.CommandText = "delete Print_Job";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Print_Job values(@ObjectId,@jobid,@pid,@notes,@mac,@pc,@newjob,@em,@ftp,@cddvd,@job_type,@handled_by,@test_job,@register_date,@job_deadline,@print_job,@Fpaper,@Fcolor,@Fdelivery_date,@Fdelivery_address,@job_status,@file_name,@handled_by2,@hold_job,@Gpage,@Gcolor)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);
            
            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("jobid", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["jobid"];
                msSqlCmd.Parameters.Add("pid", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["pid"];
                msSqlCmd.Parameters.Add("notes", SqlDbType.NVarChar, 2000).Value = accessDt.Rows[i]["notes"];
                msSqlCmd.Parameters.Add("mac", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["mac"])? 1 : 0;
                msSqlCmd.Parameters.Add("pc", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["pc"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("newjob", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["newjob"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("em", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["em"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("ftp", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["ftp"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("cddvd", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["cddvd"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("job_type", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["job_type"];
                msSqlCmd.Parameters.Add("handled_by", SqlDbType.Int, 10).Value = accessDt.Rows[i]["handled_by"];
                msSqlCmd.Parameters.Add("test_job", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["test_job"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("register_date", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["register_date"];
                msSqlCmd.Parameters.Add("job_deadline", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["job_deadline"];
                msSqlCmd.Parameters.Add("print_job", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["print_job"];
                msSqlCmd.Parameters.Add("Fpaper", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Fpaper"];
                msSqlCmd.Parameters.Add("Fcolor", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Fcolor"];
                msSqlCmd.Parameters.Add("Fdelivery_date", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Fdelivery_date"];
                msSqlCmd.Parameters.Add("Fdelivery_address", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Fdelivery_address"];
                msSqlCmd.Parameters.Add("job_status", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["job_status"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("file_name", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["file_name"];
                msSqlCmd.Parameters.Add("handled_by2", SqlDbType.Int, 10).Value = accessDt.Rows[i]["handled_by2"];
                msSqlCmd.Parameters.Add("hold_job", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["hold_job"]) ? 1 : 0;
                msSqlCmd.Parameters.Add("Gpage", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Gpage"];
                msSqlCmd.Parameters.Add("Gcolor", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["Gcolor"];
                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }

        public static void migratePrint_Job_Category(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_Job_Category
            accessCmd.CommandText = "select * from Print_Job_Category";
            msSqlCmd.CommandText = "delete Print_Job_Category";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Print_Job_Category values(@ObjectId,@id,@category_name, @category_code)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for(int i = 0 ; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("@id",SqlDbType.NVarChar,50).Value = accessDt.Rows[i]["id"];
                msSqlCmd.Parameters.Add("@category_name",SqlDbType.NVarChar,50).Value = accessDt.Rows[i]["category_name"];
                msSqlCmd.Parameters.Add("@category_code",SqlDbType.NVarChar,50).Value = accessDt.Rows[i]["category_code"];

                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }

        public static void migratePrintJob_Detail1(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_job_detail1
            accessCmd.CommandText = "select * from Print_job_detail1";
            msSqlCmd.CommandText = "delete Print_job_detail1";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Print_job_detail1 values(@ObjectId,@jobid,@qty, @size, @unit,@purpose,@cost)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("@jobid", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["jobid"];
                msSqlCmd.Parameters.Add("@qty", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["qty"];
                msSqlCmd.Parameters.Add("@size", SqlDbType.NVarChar, 255).Value = accessDt.Rows[i]["size"];
                msSqlCmd.Parameters.Add("@unit", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["unit"];
                msSqlCmd.Parameters.Add("@purpose", SqlDbType.Int, 10).Value = 0;
                msSqlCmd.Parameters.Add("@cost", SqlDbType.NVarChar, 50).Value = "";
                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }


        private static String getCategoryName(String val)
        {
            if (val == "A1")
                return "";
            if (val == "A2")
                return "Type";
            if (val == "A3")
                return "藥膜";
            if (val == "A4")
                return "網線";
            if (val == "A5")
                return "顏色";
            if (val == "A6")
                return "打稿";
            if (val == "A7")
                return "Scanning";
            if (val == "A8")
                return "Slide Output";

            if (val == "B1")
                return "用紙";
            if (val == "B2")
                return "廣告架";
            if (val == "B3")
                return "過膠";
            if (val == "B4")
                return "F. B.";
            if (val == "B5")
                return "Forex";
            if (val == "B6")
                return "包邊";
            if (val == "B7")
                return "包裝";

            if (val == "C1")
                return "用紙";
            if (val == "C2")
                return "過膠";
            if (val == "C3")
                return "F. B. ";
            if (val == "C4")
                return "Forex";
            if (val == "C5")
                return "包邊";
            if (val == "C6")
                return "包裝";

            if (val == "D1")
                return "";
            if (val == "D2")
                return "尺寸";
            if (val == "D3")
                return "顏色";
            if (val == "D4")
                return "規格";
            if (val == "D5")
                return "列印形式";
            if (val == "D6")
                return "用紙";
            if (val == "D7")
                return "加工";
            if (val == "D8")
                return "裝訂";
            if (val == "D9")
                return "封面";
            if (val == "D10")
                return "封底";
            if (val == "D11")
                return "過膠";
            if (val == "D12")
                return "F. B.";
            if (val == "D13")
                return "表咭紙";
            if (val == "D14")
                return "CDR包裝";
            if (val == "D15")
                return "包裝";

            if (val == "E1")
                return "機種";
            if (val == "E2")
                return "用紙";
            if (val == "E3")
                return "過膠";
            if (val == "E4")
                return "F. B. ";
            if (val == "E5")
                return "Forex ";
            if (val == "E6")
                return "包邊";
            if (val == "E7")
                return "包裝";
            if (val == "E8")
                return "車袋";
            if (val == "E9")
                return "接邊";
            if (val == "E10")
                return "雞眼";

            if (val == "F1")
                return "Finishing 加工";
            if (val == "F2")
                return "Binding 裝訂";
            if (val == "F3")
                return "Packing 包裝";
            if (val == "F4")
                return "Delivery Address 送貨地點";
            if (val == "F5")
                return "";

            if (val == "G1")
                return "";
            if (val == "G2")
                return "";
            if (val == "G3")
                return "";
            if (val == "G4")
                return "";

            return "";
        }
        public static void migratePrintJobLookUp(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_job_Lookup
            accessCmd.CommandText = "select * from Print_job_Lookup";
            msSqlCmd.CommandText = "delete Print_job_Lookup";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Print_job_Lookup values(@jid,@code_desc, @category,@category_name, @ordering)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@jid", SqlDbType.Int, 10).Value = accessDt.Rows[i]["jid"];
                msSqlCmd.Parameters.Add("@code_desc", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["code_desc"];
                msSqlCmd.Parameters.Add("@category", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["category"];
                msSqlCmd.Parameters.Add("@category_name", SqlDbType.NVarChar, 50).Value = getCategoryName((accessDt.Rows[i]["category"] == null) ? "" : accessDt.Rows[i]["category"].ToString());
                msSqlCmd.Parameters.Add("@ordering", SqlDbType.Int, 10).Value = accessDt.Rows[i]["ordering"];
                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }

        public static void migrateUserAC(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_job_Lookup
            accessCmd.CommandText = "select * from UserAC";
            msSqlCmd.CommandText = "delete UserAC";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into UserAC values(@ObjectId,@uid,@chi_name,@eng_name,@user_name,@dept,@user_password,@permission,@sales,@job,@admin)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("@uid", SqlDbType.Int, 10).Value = (int)(double)accessDt.Rows[i]["uid"];
                msSqlCmd.Parameters.Add("@chi_name", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["chi_name"];
                msSqlCmd.Parameters.Add("@eng_name", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["eng_name"];
                msSqlCmd.Parameters.Add("@user_name", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["user_name"];
                msSqlCmd.Parameters.Add("@dept", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["dept"];
                msSqlCmd.Parameters.Add("@user_password", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["user_password"];
                msSqlCmd.Parameters.Add("@permission", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["permission"];
                msSqlCmd.Parameters.Add("@sales", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["sales"]) ? 1 : 0; 
                msSqlCmd.Parameters.Add("@job", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["job"]) ? 1 : 0; 
                msSqlCmd.Parameters.Add("@admin", SqlDbType.Int, 10).Value = ((bool)accessDt.Rows[i]["admin"]) ? 1 : 0;
                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }

        public static void migratePrintOrder(OleDbCommand accessCmd, SqlCommand msSqlCmd)
        {
            //Print_job_detail1
            accessCmd.CommandText = "select * from Print_Order";
            msSqlCmd.CommandText = "delete Print_Order";
            msSqlCmd.ExecuteNonQuery();
            msSqlCmd.CommandText = "insert into Print_Order values(@ObjectId,@pid,@received_date,@modified_date,@order_deadline,@invoice_no,@cid,@received_by,@sales_person,@remarks,@customer_name,@order_finish,@customer_tel,@customer_contact_person)";
            OleDbDataReader accessRdr = accessCmd.ExecuteReader();
            DataTable accessDt = new DataTable();
            accessDt.Load(accessRdr);

            for (int i = 0; i < accessDt.Rows.Count; i++)
            {
                msSqlCmd.Parameters.Clear();
                msSqlCmd.Parameters.Add("@ObjectId", SqlDbType.Int, 10).Value = addObject();
                msSqlCmd.Parameters.Add("@pid", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["pid"];
                msSqlCmd.Parameters.Add("@received_date", SqlDbType.DateTime, 0).Value = accessDt.Rows[i]["received_date"];
                msSqlCmd.Parameters.Add("@modified_date", SqlDbType.DateTime, 0).Value = accessDt.Rows[i]["modified_date"];
                msSqlCmd.Parameters.Add("@order_deadline", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["order_deadline"];
                msSqlCmd.Parameters.Add("@invoice_no", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["invoice_no"];
                msSqlCmd.Parameters.Add("@cid", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["cid"];
                msSqlCmd.Parameters.Add("@received_by", SqlDbType.Int, 10).Value = accessDt.Rows[i]["received_by"];
                msSqlCmd.Parameters.Add("@sales_person", SqlDbType.Int, 10).Value = accessDt.Rows[i]["sales_person"];
                msSqlCmd.Parameters.Add("@remarks", SqlDbType.NVarChar, 2000).Value = accessDt.Rows[i]["remarks"];
                msSqlCmd.Parameters.Add("@customer_name", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["customer_name"];
                msSqlCmd.Parameters.Add("@order_finish", SqlDbType.Int, 10).Value = (bool)accessDt.Rows[i]["order_finish"] ? 1:0;
                msSqlCmd.Parameters.Add("@customer_tel", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["customer_tel"];
                msSqlCmd.Parameters.Add("@customer_contact_person", SqlDbType.NVarChar, 50).Value = accessDt.Rows[i]["customer_contact_person"];
                try
                {
                    msSqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    String ex = e.Message;
                }
            }
            accessRdr.Close();
            accessDt.Dispose();
        }


        public static void manualImport(SqlCommand msSqlCmd)
        {
            
            String[] sqls = new String[]{
                "delete from print_job_lookup where category = 'D6'",
                "insert into print_job_lookup values('157',N'80gsm','D6',N'用紙',1)",
                "insert into print_job_lookup values('151',N'100gsm','D6',N'用紙',2)",
                "insert into print_job_lookup values ('400',N'120gsm','D6',N'用紙',3)",
                "insert into print_job_lookup values ('401',N'128gsm','D6',N'用紙',4)",
                "insert into print_job_lookup values ('402',N'157gsm','D6',N'用紙',5)",
                "insert into print_job_lookup values('152','N160gsm','D6',N'用紙',5)",
                "insert into print_job_lookup values ('403',N'210gsm','D6',N'用紙',6)",
                "insert into print_job_lookup values('154',N'220gsm','D6',N'用紙',7)",
                "insert into print_job_lookup values('155',N'250gsm','D6',N'用紙',8)",
                "insert into print_job_lookup values('156',N'280gsm','D6',N'用紙',9)",
                "insert into print_job_lookup values ('404',N'300gsm','D6',N'用紙',10)",
                "insert into print_job_lookup values('158',N'80gsm紙色','D6',N'用紙',11)",
                "insert into print_job_lookup values('160',N'書紙面貼紙','D6',N'用紙',12)",
                "insert into print_job_lookup values('161',N'CD 貼紙 (細)','D6',N'用紙',13)",
                "insert into print_job_lookup values('162',N'CD 貼紙 (大)','D6',N'用紙',14)",
                "insert into print_job_lookup values('163',N'透明A4貼紙','D6',N'用紙',15)",
                "insert into print_job_lookup values('164',N'磨沙A4貼紙','D6',N'用紙',16)",
                "insert into print_job_lookup values ('405',N'自來紙','D6',N'用紙',17)",
                "insert into print_job_lookup values ('406',N'光面貼紙','D6',N'用紙',18)",
                "insert into print_job_lookup values ('407',N'光粉紙','D6',N'用紙',19)",
                "insert into print_job_lookup values ('408',N'啞粉紙','D6',N'用紙',20)",
                "insert into print_job_lookup values ('409',N'書紙','D6',N'用紙',21)",
                "insert into print_job_lookup values ('410',N'雙粉咭','D6',N'用紙',22)"
            };

            for (int i = 0; i < sqls.Length; i++)
            {
                msSqlCmd.CommandText = sqls[i];
                msSqlCmd.ExecuteNonQuery();
            }

        }
    }
}
