using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IInventoryDAO : IBaseDAO
    {
        Inventory Get(int objId, DbTransaction transaction);
        bool Add(Inventory inventory, DbTransaction transaction);
        bool Update(Inventory inventory, DbTransaction transaction);
        bool delete(Inventory inventory, DbTransaction transaction);
        int count(string condition, DbTransaction transaction);
        List<Inventory> List(string query, int limit, int start, string sortExpression, bool sortDirection, DbTransaction transaction);
    }
}
