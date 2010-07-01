using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;
using System.Data.Common;

namespace fpcore.DAO
{
    public interface IAssetConsumptionDAO : IBaseDAO
    {
        bool add(AssetConsumption jobDetail, DbTransaction transaction);
        bool delete(AssetConsumption jobDetail, DbTransaction transaction);
        bool update(AssetConsumption jobDetail, DbTransaction transaction);
        List<AssetConsumption> get(String jobid, DbTransaction transaction);
        AssetConsumption get(int objId, DbTransaction transaction);
        List<AssetConsumption> search(String query, DbTransaction transaction);
    }
}
