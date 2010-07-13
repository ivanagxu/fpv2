using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;

namespace fingerprintv2.Services
{
    public interface IFPObjectService
    {
        //Get
        Customer getCustomerByCustomerID(String cid, UserAC user);
        Customer getCustomerByID(int objectid, UserAC user);
        PrintItem getPrintJobByID(String pid, UserAC user);
        PrintJobCategory getPrintJobCategoryByID(String id, UserAC user);
        
        PrintItemDetail getPrintJobLookupByID(String id, UserAC user);
        PrintOrder getPrintOrderByID(String pid, UserAC user);
        UserAC getUserByID(int uid, UserAC user);

        //Search
        List<PrintItemDetail> getPrintJobLookupByCategory(String categoryCode, UserAC user);
        List<String> getCategoryItemCodesByCategory(PrintJobCategory category, UserAC user);

        //Order
        List<PrintOrder> getAllOrder(int limit, int start, String sort, bool descending, UserAC user);
        int countAllOrder(String condition, UserAC user);

        //Item
        List<PrintItem> getAllJob(int limit, int start, String sort, bool descending, UserAC user);
        int countAllJob(String condition, UserAC user);
        List<PrintItem> getPrintJobByOrder(PrintOrder order, UserAC user);

        //Job
        AssetConsumption getPrintJobDetailByID(String id, UserAC user);

        //Customer
        List<Customer> getAllCustomer(int limit, int start, String sort, bool descending, UserAC user);
        int countCustomer(string condition, UserAC user);
        int countCustomerContact(string condition, UserAC user);
        List<CustomerContact> getAllCustomerContact(string query, UserAC user);
        CustomerContact getCustomerContactByCode(string customerCode,string ctype, UserAC user);
        List<CustomerContact> getContactsByCode(string code, UserAC user);

        //Category
        List<PrintJobCategory> getAllPrintJobCategory(UserAC user);

        //UserAC
        List<UserAC> getJobHandlers(UserAC user);
        List<UserAC> getSales(string query,UserAC user);

        //Delivery
        List<Delivery> getAllDeliveries(int limit, int start, string sort, bool descending,UserAC user);
        int deliveryCount(string condition, UserAC user);
        Delivery getDeliveryById(int id, UserAC user);

        //role
        List<FPRole> getRoles(string query, UserAC user);
        List<UserAC> getUsersByRole(string roleID, UserAC user);
        List<UserAC> getUserNotInRole(string roleID, UserAC user);

        //Inventory
        List<Inventory> getInventories(string query,int limit, int start, string sort, bool descending, UserAC user);
        int inventoryCount(string query, UserAC user);
        Inventory getInventoryById(int inventoryId, UserAC user);

        //consumption
        List<Consumption> getConsumptions(string query, UserAC user);
        Consumption getconsumption(int objectid, UserAC user);
        
    }
}
