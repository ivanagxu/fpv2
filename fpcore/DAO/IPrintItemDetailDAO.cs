using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IPrintItemDetailDAO
    {
        List<PrintItemDetail> search(String query, DbTransaction transaction);
        PrintItemDetail get(string id, DbTransaction transaction);
        List<String> getItemNamesByCategoryId(String id, DbTransaction transaction);
    }
}
