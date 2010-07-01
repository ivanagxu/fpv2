using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IPrintItemDAO : IBaseDAO
    {
        bool add(PrintItem printJob, DbTransaction transaction);
        bool update(PrintItem printJob, DbTransaction transaction);
        bool delete(PrintItem printJob, DbTransaction transaction);
        List<PrintItem> search(string query, int limit, int start, String sort, bool descending, DbTransaction transaction);
        PrintItem get(String jobid, DbTransaction transaction);
        int count(String condition, DbTransaction transaction);
    }
}
