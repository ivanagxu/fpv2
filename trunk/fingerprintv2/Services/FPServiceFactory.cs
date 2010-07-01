using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace fingerprintv2.Services
{
    public class FPServiceFactory
    {
        private static System.Object lockThis = new System.Object();
        private static FPServiceFactory instance = null;
        private FPServiceFactory()
        {
        }
        public static FPServiceFactory getInstance()
        {
            lock (lockThis)
            {
                if (instance == null)
                {
                    instance = new FPServiceFactory();
                }
                return instance;
            }
        }

        public IFPService createFPService(NameValueCollection parameters)
        {
            return FPService.getInstance(parameters);
        }
        public IFPObjectService createFPObjectService(NameValueCollection parameters)
        {
            return FPObjectService.getInstance(parameters);
        }
    }
}
