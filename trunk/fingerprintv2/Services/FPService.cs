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
                        //do
                        //{
                        //    seqNo++;
                        //    seqNo = 100 + seqNo;
                        //    jobid = order.pid + "-" + order.print_job_list[i].job_type.category_code + ("" + seqNo).Substring(1);
                            
                        //} while (printJobDAO.get(jobid, transaction) != null);

                        int c = 0;
                        jobid = order.pid + "-" + order.print_job_list[i].job_type.category_code;
                        while (printJobDAO.get(jobid, transaction) != null)
                        {
                            c++;
                            jobid = order.pid + "-" + order.print_job_list[i].job_type.category_code + c;
                        }

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

                String jobid = "";
                //do
                //{
                //    seqNo++;
                //    seqNo = 1000 + seqNo;
                //    jobid = order.pid + job.job_type.category_code + ("" + seqNo).Substring(1);
                //} while (printJobDAO.get(jobid, transaction) != null);

                int c = 0;
                jobid = order.pid + "-" + job.job_type.category_code;
                while (printJobDAO.get(jobid, transaction) != null)
                {
                    c++;
                    jobid = order.pid + "-" + job.job_type.category_code + c;
                }

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

        public bool addNewUserAC(UserAC user, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDao = DAOFactory.getInstance().createUserDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                user.objectId = seqDAO.getNextObjectId(transaction);
                user.createDate = DateTime.Now;
                user.updateBy = currentUser.eng_name;
                user.isDeleted = false;
                userDao.add(user, transaction);
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

        public bool updateUserAC(UserAC user, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDao = DAOFactory.getInstance().createUserDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                user.updateDate = DateTime.Now;
                user.updateBy = currentUser.eng_name;
                user.isDeleted = false;
                userDao.update(user, transaction);
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

        public bool deleteUserAC(UserAC user, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDao = DAOFactory.getInstance().createUserDAO();
                user.updateDate = DateTime.Now;
                user.updateBy = currentUser.eng_name;
                userDao.delete(user, transaction);
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


        public bool addRole(FPRole role, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IFPRoleDAO roleDao = DAOFactory.getInstance().createFPRoleDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                role.objectId = seqDAO.getNextObjectId(transaction);
                role.updateBy = currentUser.eng_name;
                role.updateDate = DateTime.Now;
                role.name = role.name;
                role.other = role.other;
                roleDao.add(role, transaction);
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
        public bool updateRole(FPRole role, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IFPRoleDAO roleDao = DAOFactory.getInstance().createFPRoleDAO();
                role.updateBy = currentUser.eng_name;
                role.updateDate = DateTime.Now;
                role.name = role.name;
                role.other = role.other;
                roleDao.update(role, transaction);
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
        public bool deleteRole(FPRole role, UserAC currentUser)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IFPRoleDAO roleDao = DAOFactory.getInstance().createFPRoleDAO();
                role.updateBy = currentUser.eng_name;
                role.updateDate = DateTime.Now;
                role.name = role.name;
                role.other = role.other;
                roleDao.delete(role, transaction);
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
        public bool updateUserRole(UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IUserDAO userDao = DAOFactory.getInstance().createUserDAO();
                userDao.updateUserRole(user, transaction);
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

        public bool addCustomer(Customer customer, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerDAO customerDao = DAOFactory.getInstance().createCustomerDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                customer.objectId = seqDAO.getNextObjectId(transaction);
                customer.updateBy = user.eng_name;
                customer.createDate = DateTime.Now;
                customer.updateDate = DateTime.Now;
                customer.isDeleted = false;

                customerDao.add(customer, transaction);
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
        public bool addCustomerContact(CustomerContact cc, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerContactDAO ccDao = DAOFactory.getInstance().createCustomerContactDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                cc.objectId = seqDAO.getNextObjectId(transaction);
                cc.updateBy = user.eng_name;
                cc.createDate = DateTime.Now;
                cc.updateDate = DateTime.Now;
                cc.isDeleted = false;

                ccDao.add(cc, transaction);
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

        public bool updateCustomer(Customer customer, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerDAO customerDao = DAOFactory.getInstance().createCustomerDAO();

                customer.updateBy = user.eng_name;
                customer.updateDate = DateTime.Now;
                customer.isDeleted = false;

                customerDao.update(customer, transaction);
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
        public bool updateCustomerContact(CustomerContact cc, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerContactDAO ccDao = DAOFactory.getInstance().createCustomerContactDAO();

                cc.updateBy = user.eng_name;
                cc.updateDate = DateTime.Now;
                cc.isDeleted = false;

                ccDao.update(cc, transaction);
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
        public bool deleteCustomer(Customer customer, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerDAO customerDao = DAOFactory.getInstance().createCustomerDAO();

                customer.updateBy = user.eng_name;
                customer.updateDate = DateTime.Now;
                customer.isDeleted = false;

                customerDao.delete(customer, transaction);
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
        public bool deleteCustomerContact(CustomerContact cc, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                ICustomerContactDAO ccDao = DAOFactory.getInstance().createCustomerContactDAO();

                cc.updateBy = user.eng_name;
                cc.updateDate = DateTime.Now;
                cc.isDeleted = false;

                ccDao.delete(cc, transaction);
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


        public bool addDelivery(Delivery delivery, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IDeliveryDAO ccDao = DAOFactory.getInstance().createDeliveryDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                delivery.objectId = seqDAO.getNextObjectId(transaction);
                delivery.number = "DAA" + delivery.objectId;
                delivery.updateBy = user.eng_name;
                delivery.createDate = DateTime.Now;
                delivery.updateDate = DateTime.Now;
                delivery.isDeleted = false;

                ccDao.Add(delivery, transaction);
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

        public bool updateDelivery(Delivery delivery, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IDeliveryDAO ccDao = DAOFactory.getInstance().createDeliveryDAO();

                delivery.updateBy = user.eng_name;
                delivery.updateDate = DateTime.Now;
                delivery.isDeleted = false;

                ccDao.Update(delivery, transaction);
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
        public bool deleteDelivery(Delivery delivery, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IDeliveryDAO ccDao = DAOFactory.getInstance().createDeliveryDAO();

                delivery.updateBy = user.eng_name;
                delivery.updateDate = DateTime.Now;
                delivery.isDeleted = false;

                ccDao.delete(delivery, transaction);
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


        public int addInventory(Inventory inventory, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IInventoryDAO inventoryDao = DAOFactory.getInstance().createInventoryDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                inventory.objectId = seqDAO.getNextObjectId(transaction);
                inventory.assetno = "AAA" + inventory.objectId;
                inventory.updateBy = user.eng_name;
                inventory.createDate = DateTime.Now;
                inventory.updateDate = DateTime.Now;
                inventory.isDeleted = false;

                inventoryDao.Add(inventory, transaction);
                transaction.Commit();
                return inventory.objectId;
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
        public bool updateInventory(Inventory inventory, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IInventoryDAO ccDao = DAOFactory.getInstance().createInventoryDAO();

                inventory.updateBy = user.eng_name;
                inventory.updateDate = DateTime.Now;

                ccDao.Update(inventory, transaction);
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
        public bool deleteInventory(Inventory inventory, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IInventoryDAO ccDao = DAOFactory.getInstance().createInventoryDAO();

                inventory.updateBy = user.eng_name;
                inventory.updateDate = DateTime.Now;
                inventory.isDeleted = false;

                ccDao.delete(inventory, transaction);
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

        public bool addConsumption(Consumption consumption, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IConsumptionDAO inventoryDao = DAOFactory.getInstance().createConsumptionDAO();
                ISequenceDAO seqDAO = DAOFactory.getInstance().createSequenceDAO();
                consumption.objectId = seqDAO.getNextObjectId(transaction);
                consumption.updateBy = user.eng_name;
                consumption.createDate = DateTime.Now;
                consumption.updateDate = DateTime.Now;
                consumption.isDeleted = false;

                inventoryDao.add(consumption, transaction);
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
        public bool updateConsumption(Consumption consumption, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IConsumptionDAO ccDao = DAOFactory.getInstance().createConsumptionDAO();

                consumption.updateBy = user.eng_name;
                consumption.updateDate = DateTime.Now;

                ccDao.update(consumption, transaction);
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
        public bool deleteConsumption(Consumption consumption, UserAC user)
        {
            IDatabase db = DAOFactory.getInstance().getDatabase();
            DbConnection conn = db.getConnection();
            DbTransaction transaction = db.beginTransaction(conn);
            try
            {
                IConsumptionDAO ccDao = DAOFactory.getInstance().createConsumptionDAO();

                consumption.updateBy = user.eng_name;
                consumption.updateDate = DateTime.Now;
                consumption.isDeleted = false;

                ccDao.delete(consumption, transaction);
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
