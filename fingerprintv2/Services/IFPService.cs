using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fpcore.Model;

namespace fingerprintv2.Services
{
    public interface IFPService
    {
        UserAC login(string userName, string userPassword);
        bool newOrder(PrintOrder order, UserAC user);
        bool updateJob(PrintItem job, UserAC user);
        bool addNewJob(PrintOrder order, PrintItem job, UserAC user);
        bool updateOrder(PrintOrder order, UserAC user);
        bool deleteOrder(PrintOrder order, UserAC user);
        bool deleteJob(PrintItem job, UserAC user);
        bool addNewJobDetail(AssetConsumption detail, UserAC user);
        bool deleteJobDetail(AssetConsumption detail, UserAC user);

        bool addNewUserAC(UserAC user, UserAC currentUser);
        bool updateUserAC(UserAC user, UserAC currentUser);
        bool deleteUserAC(UserAC user, UserAC currentUser);

        bool addRole(FPRole role, UserAC currentUser);
        bool updateRole(FPRole role, UserAC currentUser);
        bool deleteRole(FPRole role, UserAC currentUser);
        bool updateUserRole(UserAC user);
    }
}
