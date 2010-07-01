using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IFPObjectDAO
    {
        bool update(FPObject obj, DbTransaction transaction);
        bool delete(FPObject obj, DbTransaction transaction);
        FPObject get(int objectId, DbTransaction transaction);
        bool add(FPObject obj, DbTransaction transaction);
        int nextObjectId(DbTransaction transaction);
    }
}
