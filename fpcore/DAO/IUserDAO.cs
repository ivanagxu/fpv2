using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IUserDAO : IBaseDAO
    {
        UserAC get(int objId, DbTransaction transaction);
        List<UserAC> search(string query, DbTransaction transaction);
        bool add(UserAC user, DbTransaction transaction);
        bool delete(UserAC user, DbTransaction transaction);
        bool update(UserAC user, DbTransaction transaction);
        bool updateUserRole(UserAC user, DbTransaction transaction);
    }
}
