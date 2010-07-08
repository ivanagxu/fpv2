using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IConsumptionDAO:IBaseDAO
    {
        Consumption get(int objId, DbTransaction transaction);
        List<Consumption> search(string query, DbTransaction transaction);
        bool add(Consumption consumption, DbTransaction transaction);
        bool delete(Consumption consumption, DbTransaction transaction);
        bool update(Consumption consumption, DbTransaction transaction);
        decimal cateStoredCount(int inventoryid, DbTransaction transaction);
    }
}
