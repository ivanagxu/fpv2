using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IDepartmentDAO
    {
        bool add(Department dept, DbTransaction transaction);
        bool delete(Department dept, DbTransaction transaction);
        bool update(Department dept, DbTransaction transaction);
        Department get(int objId, DbTransaction transaction);
        List<Department> search(string query, DbTransaction transaction);
    }
}
