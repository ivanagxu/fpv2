using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface ICustomerContactDAO
    {
        List<CustomerContact> search(string query, DbTransaction transaction);
        CustomerContact get(int contact_id, DbTransaction transaction);
        bool add(CustomerContact contact, DbTransaction transaction);
        bool update(CustomerContact cc, DbTransaction transaction);
        bool delete(CustomerContact cc, DbTransaction transaction);
        int count(String condition, DbTransaction transaction);
        CustomerContact getCustomerContactByCode(string customerCode,string ctype, DbTransaction transaction);
    }
}
