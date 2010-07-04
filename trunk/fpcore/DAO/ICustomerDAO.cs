using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface ICustomerDAO : IBaseDAO
    {
        Customer get(String cid, DbTransaction transaction);
        Customer getByID(int objectid, DbTransaction transaction);
        List<Customer> search(String query, int limit, int start, String sort, bool descending, DbTransaction transaction);
        bool add(Customer customer, DbTransaction transaction);
        bool update(Customer customer, DbTransaction transaction);
        bool delete(Customer customer, DbTransaction transaction);
        int count(String condition, DbTransaction transaction);
    }
}
