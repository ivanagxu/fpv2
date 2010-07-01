using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IPrintJobCategoryDAO : IBaseDAO
    {
        PrintJobCategory get(String id, DbTransaction transaction);
        List<PrintJobCategory> search(String query, DbTransaction transaction); 
    }
}
