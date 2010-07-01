using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface ISequenceDAO
    {
        int getNextObjectId(DbTransaction transaction);
    }
}
