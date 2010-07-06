using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IDeliveryDAO:IBaseDAO
    {
        Delivery Get(int objId, DbTransaction transaction);
        bool Add(Delivery delivery,DbTransaction transaction);
        bool Update(Delivery delivery, DbTransaction transaction);
        bool delete(Delivery delivery, DbTransaction transaction);
        int count(string condition, DbTransaction transaction);
        List<Delivery> List(string query, int limit, int start,string sortExpression, bool sortDirection, DbTransaction transaction);
    }
}
