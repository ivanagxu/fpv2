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
    public class PrintItemMSSqlDAO : BaseDAO, IPrintItemDAO
    {

        public PrintItem get(string jobid, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            List<PrintItem> printJobs = search(" where jobid = '" + jobid + "' and IsDeleted = 0", 1, 1, "", false, trans);

            if (printJobs != null && printJobs.Count > 0)
                return printJobs[0];
            else
                return null;
        }


        public bool add(PrintItem printItem, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.add(printItem, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "insert into Print_Item(ObjectId, jobid, pid, notes, mac, pc, newjob, em, ftp, cddvd, job_type, handled_by, test_job, register_date, job_deadline, print_job, Fpaper, Fcolor, Fdelivery_date, Fdelivery_address, job_status, file_name, hold_job, Gpage, Gcolor,qty,size,unit) values" +
                "(@ObjectId, @jobid, @pid, @notes, @mac, @pc, @newjob, @em, @ftp, @cddvd, @job_type, @handled_by, @test_job, @register_date, @job_deadline, @print_job, @Fpaper, @Fcolor, @Fdelivery_date, @Fdelivery_address, @job_status, @file_name, @hold_job, @Gpage, @Gcolor, @qty, @size, @unit)";
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            cmd.Parameters.Add(genSqlParameter("ObjectId", SqlDbType.Int, 10, printItem.objectId));
            cmd.Parameters.Add(genSqlParameter("jobid", SqlDbType.NVarChar, 50, printItem.jobid));
            cmd.Parameters.Add(genSqlParameter("pid", SqlDbType.NVarChar, 50, printItem.pid));
            cmd.Parameters.Add(genSqlParameter("notes", SqlDbType.NVarChar, 2000, printItem.notes));
            cmd.Parameters.Add(genSqlParameter("mac", SqlDbType.Int, 10, printItem.mac ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("pc", SqlDbType.Int, 10, printItem.pc ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("newjob", SqlDbType.Int, 10, printItem.newjob ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("em", SqlDbType.Int, 10, printItem.em ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("cddvd", SqlDbType.Int, 10, printItem.cddvd ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("job_type", SqlDbType.NVarChar, 50, printItem.job_type == null ? "" : printItem.job_type.id));

            if (printItem.handled_by == null)
                cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 10, printItem.handled_by.objectId));

            cmd.Parameters.Add(genSqlParameter("test_job", SqlDbType.Int, 10, printItem.test_job ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("register_date", SqlDbType.NVarChar, 50, printItem.register_date));
            cmd.Parameters.Add(genSqlParameter("job_deadline", SqlDbType.NVarChar, 50, printItem.job_deadline));
            cmd.Parameters.Add(genSqlParameter("Fpaper", SqlDbType.NVarChar, 50, printItem.Fpaper));
            cmd.Parameters.Add(genSqlParameter("Fcolor", SqlDbType.NVarChar, 50, printItem.Fcolor));
            cmd.Parameters.Add(genSqlParameter("Fdelivery_date", SqlDbType.NVarChar, 50, printItem.Fdelivery_date));
            cmd.Parameters.Add(genSqlParameter("Fdelivery_address", SqlDbType.NVarChar, 50, printItem.Fdelivery_address));
            cmd.Parameters.Add(genSqlParameter("job_status", SqlDbType.NVarChar, 50, printItem.job_status));
            cmd.Parameters.Add(genSqlParameter("file_name", SqlDbType.NVarChar, 50, printItem.file_name));

            cmd.Parameters.Add(genSqlParameter("print_job", SqlDbType.NVarChar, 255, printItem.item_detail));


            cmd.Parameters.Add(genSqlParameter("hold_job", SqlDbType.Int, 10, printItem.hold_job ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("Gpage", SqlDbType.NVarChar, 50, printItem.Gpage));
            cmd.Parameters.Add(genSqlParameter("Gcolor", SqlDbType.NVarChar, 50, printItem.Gcolor));
            cmd.Parameters.Add(genSqlParameter("ftp", SqlDbType.Int, 10, printItem.ftp ? 1 : 0));

            cmd.Parameters.Add(genSqlParameter("qty", SqlDbType.NVarChar, 50, printItem.qty));
            cmd.Parameters.Add(genSqlParameter("size", SqlDbType.NVarChar, 50, printItem.size));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 50, printItem.unit));


            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool update(PrintItem printJob, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            objDAO.update(printJob, transaction);

            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "update Print_Item set " +
                "pid = @pid, notes = @notes, mac = @mac, pc = @pc, newjob = @newjob, em = @em, ftp = @ftp, cddvd = @cddvd, " +
                "job_type = @job_type, handled_by = @handled_by, test_job = @test_job, register_date = @register_date, " +
                "job_deadline = @job_deadline, print_job = @print_job, Fpaper = @Fpaper, Fcolor = @Fcolor, Fdelivery_date = @Fdelivery_date, "+
                "Fdelivery_address = @Fdelivery_address, job_status = @job_status, file_name = @file_name, hold_job = @hold_job, "+
                "Gpage = @Gpage, Gcolor = @Gcolor, qty=@qty, size=@size, unit=@unit where jobid = @jobid";
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            cmd.Parameters.Add(genSqlParameter("jobid", SqlDbType.NVarChar, 50, printJob.jobid));
            cmd.Parameters.Add(genSqlParameter("pid", SqlDbType.NVarChar, 50, printJob.pid));
            cmd.Parameters.Add(genSqlParameter("notes", SqlDbType.NVarChar, 2000, printJob.notes));
            cmd.Parameters.Add(genSqlParameter("mac", SqlDbType.Int, 10, printJob.mac ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("pc", SqlDbType.Int, 10, printJob.pc ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("newjob", SqlDbType.Int, 10, printJob.newjob ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("em", SqlDbType.Int, 10, printJob.em ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("cddvd", SqlDbType.Int, 10, printJob.cddvd ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("job_type", SqlDbType.NVarChar, 50, printJob.job_type == null ? "" : printJob.job_type.id));

            if (printJob.handled_by == null)
                cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 10, null));
            else
                cmd.Parameters.Add(genSqlParameter("handled_by", SqlDbType.Int, 10, printJob.handled_by.objectId));

            cmd.Parameters.Add(genSqlParameter("test_job", SqlDbType.Int, 10, printJob.test_job ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("register_date", SqlDbType.NVarChar, 50, printJob.register_date));
            cmd.Parameters.Add(genSqlParameter("job_deadline", SqlDbType.NVarChar, 50, printJob.job_deadline));

            cmd.Parameters.Add(genSqlParameter("Fpaper", SqlDbType.NVarChar, 50, printJob.Fpaper));
            cmd.Parameters.Add(genSqlParameter("Fcolor", SqlDbType.NVarChar, 50, printJob.Fcolor));
            cmd.Parameters.Add(genSqlParameter("Fdelivery_date", SqlDbType.NVarChar, 50, printJob.Fdelivery_date));
            cmd.Parameters.Add(genSqlParameter("Fdelivery_address", SqlDbType.NVarChar, 50, printJob.Fdelivery_address));
            cmd.Parameters.Add(genSqlParameter("job_status", SqlDbType.NVarChar, 50, printJob.job_status));
            cmd.Parameters.Add(genSqlParameter("file_name", SqlDbType.NVarChar, 50, printJob.file_name));

            cmd.Parameters.Add(genSqlParameter("print_job", SqlDbType.NVarChar, 255, printJob.item_detail));

            cmd.Parameters.Add(genSqlParameter("hold_job", SqlDbType.Int, 10, printJob.hold_job ? 1 : 0));
            cmd.Parameters.Add(genSqlParameter("Gpage", SqlDbType.NVarChar, 50, printJob.Gpage));
            cmd.Parameters.Add(genSqlParameter("Gcolor", SqlDbType.NVarChar, 50, printJob.Gcolor));
            cmd.Parameters.Add(genSqlParameter("ftp", SqlDbType.Int, 10, printJob.ftp ? 1 : 0));

            cmd.Parameters.Add(genSqlParameter("qty", SqlDbType.NVarChar, 50, printJob.qty));
            cmd.Parameters.Add(genSqlParameter("size", SqlDbType.NVarChar, 50, printJob.size));
            cmd.Parameters.Add(genSqlParameter("unit", SqlDbType.NVarChar, 50, printJob.unit));


            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }

        public bool delete(PrintItem printJob, DbTransaction transaction)
        {
            IFPObjectDAO objDAO = DAOFactory.getInstance().createFPObjectDAO();
            return objDAO.delete(printJob, transaction);
        }

        public List<PrintItem> search(string query, int limit, int start, String sort, bool descending, DbTransaction transaction)
        {
            if (sort == "" || sort == null)
                sort = "jobid";

            String orderby1 = sort + (descending ? " DESC" : " ASC");
            String orderby2 = sort + (descending ? " ASC" : " DESC");

            String sql =
                " SELECT * FROM ( " +
                    " SELECT TOP " + limit + " * FROM ( " +
                        " SELECT TOP " + (limit + start) + " jobid, pid, notes, mac, pc, newjob, em, ftp, cddvd, job_type, handled_by, test_job, register_date, job_deadline, print_job, Fpaper, Fcolor, Fdelivery_date, Fdelivery_address, job_status, file_name, hold_job, Gpage, Gcolor,qty,size,unit, FPObject.* " +
                        " FROM Print_Item inner join FPObject on Print_Item.ObjectId = FPObject.ObjectId " +
                            query +
                        " ORDER BY " + orderby1 + ") as foo " +
                    " ORDER by " + orderby2 + ") as bar " +
                    " ORDER by " + orderby1;

            SqlTransaction trans = (SqlTransaction)transaction;
            SqlConnection conn = trans.Connection;
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = sql;

            List<PrintItem> printJobs = getQueryResult(cmd);
            cmd.Dispose();
            return printJobs;
        }

        public int count(String condition, DbTransaction transaction)
        {
            SqlTransaction trans = (SqlTransaction)transaction;
            String sql = "select count(*) as total from Print_Item inner join FPObject on Print_Item.ObjectId = FPObject.ObjectId " + condition;
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

        private List<PrintItem> getQueryResult(SqlCommand cmd)
        {
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            IAssetConsumptionDAO printJobDetailDAO = DAOFactory.getInstance().createPrintJobDetailDAO();
            IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
            IPrintJobCategoryDAO categoryDAO = DAOFactory.getInstance().createPrintJobCategoryDAO();
            IPrintItemDetailDAO printJobLookupDAO = DAOFactory.getInstance().createPrintJobLookupDAO();

            List<PrintItem> printItems = new List<PrintItem>();
            PrintItem printItem = null;
            String[] itemKeys;
            PrintItemDetail item = null;

            dt.Load(reader);
            reader.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    printItem = new PrintItem();
                    printItem.objectId = getInt(dt.Rows[i]["ObjectId"]);
                    printItem.createDate = getDateTime(dt.Rows[i]["CreateDate"]);
                    printItem.updateDate = getDateTime(dt.Rows[i]["UpdateDate"]);
                    printItem.updateBy = getString(dt.Rows[i]["UpdateBy"]);
                    printItem.isDeleted = (getInt(dt.Rows[i]["IsDeleted"]) == 1);
                    printItem.cddvd = getInt(dt.Rows[i]["cddvd"]) == 1 ? true : false;
                    printItem.em = getInt(dt.Rows[i]["em"]) == 1 ? true : false;
                    printItem.Fcolor = getString(dt.Rows[i]["Fcolor"]);
                    printItem.Fdelivery_address = getString(dt.Rows[i]["Fdelivery_address"]);
                    printItem.Fdelivery_date = getString(dt.Rows[i]["Fdelivery_date"]);
                    printItem.file_name = getString(dt.Rows[i]["file_name"]);
                    printItem.Fpaper = getString(dt.Rows[i]["Fpaper"]);
                    printItem.ftp = getInt(dt.Rows[i]["ftp"]) == 1 ? true : false;
                    printItem.Gcolor = getString(dt.Rows[i]["Gcolor"]);
                    printItem.Gpage = getString(dt.Rows[i]["Gpage"]);
                    printItem.handled_by = userDAO.get(getInt(dt.Rows[i]["handled_by"]), cmd.Transaction);
                    printItem.hold_job = getInt(dt.Rows[i]["hold_job"]) == 1 ? true : false;
                    printItem.job_deadline = getString(dt.Rows[i]["job_deadline"]);
                    printItem.job_status = getString(dt.Rows[i]["job_status"]);
                    printItem.job_type = categoryDAO.get(getString(dt.Rows[i]["job_type"]), cmd.Transaction);
                    printItem.jobid = getString(dt.Rows[i]["jobid"]);
                    printItem.mac = getInt(dt.Rows[i]["mac"]) == 1 ? true : false;
                    printItem.newjob = getInt(dt.Rows[i]["newjob"]) == 1 ? true : false;
                    printItem.notes = getString(dt.Rows[i]["notes"]);
                    printItem.pc = getInt(dt.Rows[i]["pc"]) == 1 ? true : false;
                    printItem.pid = getString(dt.Rows[i]["pid"]);
                    printItem.item_detail = getString(dt.Rows[i]["print_job"]);
                    printItem.register_date = getString(dt.Rows[i]["register_date"]);
                    printItem.test_job = getInt(dt.Rows[i]["test_job"]) == 1 ? true : false;
                    printItem.qty = getString(dt.Rows[i]["qty"]);
                    printItem.size = getString(dt.Rows[i]["size"]);
                    printItem.unit = getString(dt.Rows[i]["unit"]);

                    printItem.print_job_detail = printJobDetailDAO.search(" where jobid = '" + printItem.jobid + "' and IsDeleted = 0 ", cmd.Transaction);

                    printItem.print_job_items = new List<PrintItemDetail>();
                    if (printItem.item_detail != null)
                    {
                        itemKeys = printItem.item_detail.Split('/');
                        for (int j = 0; j < itemKeys.Length; j++)
                        {
                            item = printJobLookupDAO.get(itemKeys[j],cmd.Transaction);
                            if (item != null)
                                printItem.print_job_items.Add(item);
                        }
                    }

                    printItems.Add(printItem);
                }
            }
            return printItems;
        }
    }
}
