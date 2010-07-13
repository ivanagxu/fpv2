using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IFPRoleDAO
    {
        bool add(FPRole role, DbTransaction transaction);
        bool update(FPRole role, DbTransaction transaction);
        bool delete(FPRole role, DbTransaction transaction);
        FPRole get(int objId, DbTransaction transaction);
        List<FPRole> search(string query, DbTransaction transaction);
        List<FPRole> getRoleByUser(UserAC user, DbTransaction transaction);
       
    }
}
