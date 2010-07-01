using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using fpcore.Model;

namespace fpcore.DAO
{
    public interface ISectionDAO
    {
        bool add(Section section, DbTransaction transaction);
        bool delete(Section section, DbTransaction transaction);
        bool update(Section section, DbTransaction transaction);
        Section get(int objId, DbTransaction transaction);
        List<Section> search(string query, DbTransaction transaction);
    }
}
