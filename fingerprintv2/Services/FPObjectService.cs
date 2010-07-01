using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using fpcore.Model;
using fpcore.DAO;
using System.Data.Common;

namespace fingerprintv2.Services
{
    public class FPObjectService : IFPObjectService
    {
        private static Object lockThis = new Object();
        private static FPObjectService instance = null;
        public string connStr { get; set; }

        private FPObjectService() { }
        private FPObjectService(NameValueCollection parameters)
        {
            connStr = parameters["connStr"];
        }

        public static IFPObjectService getInstance(NameValueCollection parameters)
        {
            lock (lockThis)
            {
                if (instance == null)
                {
                    instance = new FPObjectService(parameters);
                }
                return instance;
            }
        }

        #region IFPObjectService Members

        public Customer getCustomerByCustomerID(string cid, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerDAO customerDAO = DAOFactory.getInstance().createCustomerDAO();
                Customer c = customerDAO.get(cid, transaction);
                transaction.Commit();
                return c;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public PrintItem getPrintJobByID(string jobid, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                PrintItem job = printJobDAO.get(jobid, transaction);
                transaction.Commit();
                return job;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public PrintJobCategory getPrintJobCategoryByID(string id, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintJobCategoryDAO categoryDAO = DAOFactory.getInstance().createPrintJobCategoryDAO();
                PrintJobCategory category = categoryDAO.get(id, transaction);
                transaction.Commit();
                return category;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public AssetConsumption getPrintJobDetailByID(string id, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IAssetConsumptionDAO acDAO = DAOFactory.getInstance().createPrintJobDetailDAO();
                List<AssetConsumption> jobs = acDAO.search(" where IsDeleted = 0 and FPObject.ObjectId = " + id, transaction);
                transaction.Commit();
                if (jobs.Count > 0)
                    return jobs[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public PrintItemDetail getPrintJobLookupByID(string id, UserAC user)
        {
            throw new NotImplementedException();
        }

        public PrintOrder getPrintOrderByID(string pid, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintOrderDAO PrintOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                PrintOrder order = PrintOrderDAO.get(pid, transaction);
                transaction.Commit();
                return order;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public UserAC getUserByID(int uid, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
                UserAC tuser = userDAO.get(uid, transaction);
                transaction.Commit();
                return tuser;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<PrintItemDetail> getPrintJobLookupByCategory(string categoryCode, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDetailDAO lookupDAO = DAOFactory.getInstance().createPrintJobLookupDAO();
                List<PrintItemDetail> lookups = lookupDAO.search(" where category = '" + categoryCode + "' and code_desc is not null order by ordering asc", transaction);

                transaction.Commit();
                return lookups;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<String> getCategoryItemCodesByCategory(PrintJobCategory category, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDetailDAO lookupDAO = DAOFactory.getInstance().createPrintJobLookupDAO();
                List<String> items = lookupDAO.getItemNamesByCategoryId(category.id, transaction);

                transaction.Commit();
                return items;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        public int countAllOrder(String condition, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintOrderDAO printOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                int count = printOrderDAO.count(condition, transaction);
                transaction.Commit();
                return count;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<PrintOrder> getAllOrder(int limit, int start, String sort, bool descending, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintOrderDAO PrintOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                List<PrintOrder> orders = PrintOrderDAO.search("  where isdeleted = 0  ", limit, start, sort, descending, transaction);
                transaction.Commit();
                return orders;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public int countAllJob(String condition, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                int count = printJobDAO.count(condition, transaction);
                transaction.Commit();
                return count;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<PrintItem> getAllJob(int limit, int start, String sort, bool descending, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                List<PrintItem> jobs = printJobDAO.search("  where isdeleted = 0  ", limit, start, sort, descending, transaction);
                transaction.Commit();
                return jobs;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<PrintItem> getPrintJobByOrder(PrintOrder order, UserAC user)
        {
            if (order == null)
                return new List<PrintItem>();

            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                List<PrintItem> jobs = printJobDAO.search("where pid='" + order.pid + "' and isdeleted = 0 ", 1000, 0, "jobid", true, transaction);
                transaction.Commit();
                return jobs;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Customer> getAllCustomer(int limit, int start, String sort, bool descending, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerDAO customerDAO = DAOFactory.getInstance().createCustomerDAO();
                List<Customer> customers = customerDAO.search("  where isdeleted = 0  ", limit, start, sort, descending, transaction);
                transaction.Commit();
                return customers;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<PrintJobCategory> getAllPrintJobCategory(UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintJobCategoryDAO categoryDAO = DAOFactory.getInstance().createPrintJobCategoryDAO();
                List<PrintJobCategory> categorys = categoryDAO.search("  where isdeleted = 0  ", transaction);
                transaction.Commit();
                return categorys;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<UserAC> getJobHandlers(UserAC user)
        {

            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
                List<UserAC> users = userDAO.search("  where isdeleted = 0  ", transaction);
                transaction.Commit();
                return users;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<UserAC> getSales(UserAC user)
        {

            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
                List<UserAC> users = userDAO.search("  where isdeleted = 0  ", transaction);
                transaction.Commit();
                return users;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
