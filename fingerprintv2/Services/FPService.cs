using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using fpcore.Model;
using fpcore.DAO;
using System.Data.SqlClient;
using System.Data.Common;

namespace fingerprintv2.Services
{
    public class FPService : IFPService
    {
        private static Object lockThis = new Object();
        private static FPService instance = null;
        private string connStr = "";

        private FPService(NameValueCollection parameters)
        {
            connStr = parameters["ConnStr"];
        }

        public static IFPService getInstance(NameValueCollection parameters)
        {
            lock (lockThis)
            {
                if (instance == null)
                {
                    instance = new FPService(parameters);
                }
                return instance;
            }
        }

        public UserAC login(string userName, string userPassword)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDAO = DAOFactory.getInstance().createUserDAO();
                List<UserAC> users = userDAO.search("where user_name = '" + userName + "' and user_password = '" + userPassword + "'", transaction);
                transaction.Commit();

                if (users == null || users.Count == 0)
                    return null;
                else
                    return users[0];
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

        public bool newOrder(PrintOrder order, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                //[yy][mm][dd][3digits]
                IPrintOrderDAO printOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                ICustomerContactDAO contactDAO = DAOFactory.getInstance().createCustomerContactDAO();
                int seqNo = 0;
                String pid = DateTime.Now.ToString("yyMMdd");
                do
                {
                    seqNo++;
                    seqNo = 1000 + seqNo;

                } while (printOrderDAO.get(pid + ("" + seqNo).Substring(1),transaction) != null);

                order.pid = pid + ("" + seqNo).Substring(1);
                order.objectId = seqDAO.getNextObjectId(transaction);
                order.updateBy = user.eng_name;

                order.customer_contact.objectId = seqDAO.getNextObjectId(transaction);
                order.customer_contact.updateBy = user.eng_name;
                contactDAO.add(order.customer_contact, transaction);
                printOrderDAO.add(order, transaction);



                if (order.print_job_list != null)
                {
                    String jobid = "";
                    for (int i = 0; i < order.print_job_list.Count; i++)
                    {
                        order.print_job_list[i].job_deadline = order.order_deadline;
                        order.print_job_list[i].pid = order.pid;

                        seqNo = 0;
                        do
                        {
                            seqNo++;
                            seqNo = 100 + seqNo;
                            jobid = order.pid + order.print_job_list[i].job_type.category_code + ("" + seqNo).Substring(1);
                        } while (printJobDAO.get(jobid, transaction) != null);
                        order.print_job_list[i].jobid = jobid;

                        order.print_job_list[i].objectId = seqDAO.getNextObjectId(transaction);
                        order.print_job_list[i].updateBy = user.eng_name;
                        printJobDAO.add(order.print_job_list[i], transaction);
                    }
                }
                transaction.Commit();
                return true;
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
        public bool updateJob(PrintItem job, UserAC user)
        {
            if (job == null)
                return false;

            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                IPrintOrderDAO orderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                job.updateBy = user.eng_name;

                bool success = printJobDAO.update(job, transaction);

                
                PrintOrder order = orderDAO.get(job.pid, transaction);
                
                bool isOrderFinished = true;
                for(int i = 0 ; i < order.print_job_list.Count; i++)
                {
                    if (order.print_job_list[i].job_status != PrintItem.STATUS_FINISH)
                    {
                        isOrderFinished = false;
                        break;
                    }
                }

                order.status = isOrderFinished ?  PrintOrder.STATUS_FINISH : PrintOrder.STATUS_PENDING;
                order.updateDate = DateTime.Now;
                order.updateBy = user.eng_name;
                orderDAO.update(order, transaction);


                transaction.Commit();
                return success;
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
        public bool addNewJob(PrintOrder order, PrintItem job, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                int seqNo = 0;

                //String jobid = "";
                //do
                //{
                //    seqNo++;
                //    seqNo = 1000 + seqNo;
                //    jobid = order.pid + job.job_type.category_code + ("" + seqNo).Substring(1);
                //} while (printJobDAO.get(jobid, transaction) != null);

                jobid = order.pid + "-" + job.job_type.category_code;

                job.jobid = jobid;
                job.job_deadline = order.order_deadline;
                job.pid = order.pid;
                job.objectId = seqDAO.getNextObjectId(transaction);
                job.updateBy = user.eng_name;

                printJobDAO.add(job, transaction);

                transaction.Commit();
                return true;
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

        public bool updateOrder(PrintOrder order, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                //[yy][mm][dd][3digits]
                IPrintOrderDAO printOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();

                if (order.customer_contact.objectId == 0)
                {
                    ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                    ICustomerContactDAO contactDAO = DAOFactory.getInstance().createCustomerContactDAO();
                    order.customer_contact.objectId = seqDAO.getNextObjectId(transaction);
                    order.customer_contact.updateBy = user.eng_name;
                    contactDAO.add(order.customer_contact, transaction);
                }

                order.updateBy = user.eng_name;
                printOrderDAO.update(order, transaction);

                transaction.Commit();
                return true;
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

        public bool deleteOrder(PrintOrder order, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                //[yy][mm][dd][3digits]
                IPrintOrderDAO printOrderDAO = DAOFactory.getInstance().createPrintOrderDAO();
                order.updateBy = user.eng_name;
                printOrderDAO.delete(order, transaction);
                IPrintItemDAO printJobDAO = DAOFactory.getInstance().createPrintJobDAO();

                if (order.print_job_list != null)
                {
                    for (int i = 0; i < order.print_job_list.Count; i++)
                    {
                        order.print_job_list[i].updateBy = user.eng_name;
                        printJobDAO.delete(order.print_job_list[i], transaction);
                    }
                }

                transaction.Commit();
                return true;
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
        public bool deleteJob(PrintItem job, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IPrintItemDAO jobDAO = DAOFactory.getInstance().createPrintJobDAO();
                job.updateBy = user.eng_name;
                jobDAO.delete(job, transaction);
                transaction.Commit();
                return true;
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
        public bool addNewJobDetail(AssetConsumption detail, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IAssetConsumptionDAO detailDAO = DAOFactory.getInstance().createPrintJobDetailDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();

                detail.objectId = seqDAO.getNextObjectId(transaction);
                detail.updateBy = user.eng_name;
                detailDAO.add(detail, transaction);
                transaction.Commit();
                return true;
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
        public bool deleteJobDetail(AssetConsumption detail, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IAssetConsumptionDAO detailDAO = DAOFactory.getInstance().createPrintJobDetailDAO();
                detail.updateBy = user.eng_name;
                detailDAO.delete(detail, transaction);
                transaction.Commit();
                return true;
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
    }
}
