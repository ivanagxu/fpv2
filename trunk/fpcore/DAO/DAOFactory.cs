using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.DAO.MSSql;

namespace fpcore.DAO
{
    public class DAOFactory
    {
        private static DAOFactory instance = null;
        public static Object lockObj = new Object();

        public static readonly String DATABASE_ACCESS = "ACCESS";
        public static readonly String DATABASE_MSSQL = "MSSQL";


        private String databaseName = "";
        private String connStr = "";
        private IDatabase database = null;
        private IUserDAO userDAO = null;
        private ICustomerDAO customerDAO = null;
        private IPrintJobCategoryDAO printJobCategoryDAO = null;
        private IAssetConsumptionDAO printJobDetailDAO = null;
        private IPrintOrderDAO printOrderDAO = null;
        private IPrintItemDAO printJobDAO = null;
        private IPrintItemDetailDAO printJobLookupDAO = null;
        private IFPObjectDAO fpObjectDAO = null;
        private ISectionDAO sectionDAO = null;
        private IDepartmentDAO departmentDAO = null;
        private IFPRoleDAO roleDAO = null;
        private ICustomerContactDAO contactDAO = null;
        private ISequenceDAO seqDAO = null;
        private IDeliveryDAO deliveryDAO = null;
        private IInventoryDAO inventoryDAO = null;
        private IConsumptionDAO consumptionDAO=null ;

        private DAOFactory(String databaseName, String connStr)
        {
            this.databaseName = databaseName;
            this.connStr = connStr;
        }

        public static DAOFactory getInstance(string databaseName, string connStr)
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new DAOFactory(databaseName, connStr);
                }

                return instance;
            }
        }

        public static DAOFactory getInstance()
        {
            lock(lockObj)
            {
                if (instance == null)
                    throw new Exception("getInstance failed, require to use 'getInstance(String databaseName)' at first");
                return instance;
            }
        }

        public IDatabase getDatabase()
        {
            lock (lockObj)
            {
                if (database == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                    {
                        database = new MSSqlDatabase();
                        database.setConnectionString(connStr);
                    }

                    if (database == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return database;
            }
        }

        public IUserDAO createUserDAO()
        {
            lock(lockObj)
            {
                if(userDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        userDAO = new UserMSSqlDAO();

                    if (userDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return userDAO;
            }
        }

        public ICustomerDAO createCustomerDAO()
        {
            lock (lockObj)
            {
                if (customerDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        customerDAO = new CustomerMSSqlDAO();

                    if (customerDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return customerDAO;
            }
        }

        public IPrintItemDAO createPrintJobDAO()
        {
            lock (lockObj)
            {
                if (printJobDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        printJobDAO = new PrintItemMSSqlDAO();

                    if (printJobDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return printJobDAO;
            }
        }

        public IAssetConsumptionDAO createPrintJobDetailDAO()
        {
            lock (lockObj)
            {
                if (printJobDetailDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        printJobDetailDAO = new AssetConsumptionMSSqlDAO();

                    if (printJobDetailDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return printJobDetailDAO;
            }
        }

        public IPrintJobCategoryDAO createPrintJobCategoryDAO()
        {
            lock (lockObj)
            {
                if (printJobCategoryDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        printJobCategoryDAO = new PrintJobCategoryMSSqlDAO();

                    if (printJobCategoryDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return printJobCategoryDAO;
            }
        }

        public IPrintOrderDAO createPrintOrderDAO()
        {
            lock (lockObj)
            {
                if (printOrderDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        printOrderDAO = new PrintOrderMSSqlDAO();

                    if (printOrderDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return printOrderDAO;
            }
        }

        public IPrintItemDetailDAO createPrintJobLookupDAO()
        {
            lock (lockObj)
            {
                if (printJobLookupDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        printJobLookupDAO = new PrintItemDetailMSSqlDAO();

                    if (printJobLookupDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return printJobLookupDAO;
            }
        }

        public IFPObjectDAO createFPObjectDAO()
        {
            lock (lockObj)
            {
                if (fpObjectDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        fpObjectDAO = new FPObjectMSSqlDAO();

                    if (fpObjectDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return fpObjectDAO;
            }
        }

        public ISectionDAO createSectionDAO()
        {
            lock (lockObj)
            {
                if (sectionDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        sectionDAO = new SectionMSSqlDAO();

                    if (sectionDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return sectionDAO;
            }
        }

        public IDepartmentDAO createDepartmentDAO()
        {
            lock (lockObj)
            {
                if (departmentDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        departmentDAO = new DepartmentMSSqlDAO();

                    if (departmentDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return departmentDAO;
            }
        }

        public IFPRoleDAO createFPRoleDAO()
        {
            lock (lockObj)
            {
                if (roleDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        roleDAO = new FPRoleMSSqlDAO();

                    if (roleDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return roleDAO;
            }
        }

        public ICustomerContactDAO createCustomerContactDAO()
        {
            lock (lockObj)
            {
                if (contactDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        contactDAO = new CustomerContactMSSqlDAO();

                    if (contactDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return contactDAO;
            }
        }
        public ISequenceDAO createSequenceDAO()
        {
            lock (lockObj)
            {
                if (seqDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        seqDAO = new SequenceMSSqlDAO();

                    if (seqDAO == null)
                        throw new Exception("Unsupported database : " + databaseName);
                }

                return seqDAO;
            }
        }

        public IDeliveryDAO createDeliveryDAO()
        {
            lock (lockObj)
            {
                if (deliveryDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        deliveryDAO = new DeliveryMSSqlDAO();
                    if (deliveryDAO == null)
                        throw new Exception("Unsupported database: " + databaseName);
                }
                return deliveryDAO;
            }
        }

        public IInventoryDAO createInventoryDAO()
        {
            lock (lockObj)
            {
                if (inventoryDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        inventoryDAO = new InventoryMSSqlDAO();
                    if(inventoryDAO ==null )
                        throw new Exception("Unsupported database: " + databaseName);
                }
                return inventoryDAO;
            }
        }

        public IConsumptionDAO createConsumptionDAO()
        {
            lock (lockObj)
            {
                if (consumptionDAO == null)
                {
                    if (databaseName == DATABASE_MSSQL)
                        consumptionDAO = new ConsumptionMSSqlDAO();
                    if (consumptionDAO ==null )
                        throw new Exception("Unsupported database: " + databaseName);
                }
                return consumptionDAO;
            }
        }
    }
}
