using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using fpcore.Model;

namespace fpcore.DAO
{
    public interface IPrintOrderDAO : IBaseDAO
    {
        bool add(PrintOrder order, DbTransaction transaction);
        bool update(PrintOrder order, DbTransaction transaction);
        bool delete(PrintOrder order, DbTransaction transaction);
        PrintOrder get(String id, DbTransaction transaction);
        int count(String condition, DbTransaction transaction);
        List<PrintOrder> search(string query, int limit, int start, String sort, bool descending, DbTransaction transaction);
    }
}
