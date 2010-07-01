using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace fingerprintv2.Services
{
    public class FPServiceHolder
    {
        private static System.Object lockThis = new System.Object();
        private static FPServiceHolder instance = null;
        private IDictionary services = new Dictionary<String, Object>();

        private FPServiceHolder()
        {
        }
        public static FPServiceHolder getInstance()
        {
            lock (lockThis)
            {
                if (instance == null)
                {
                    instance = new FPServiceHolder();
                }
                return instance;
            }
        }
        public void addService(String serviceName, Object serviceObj)
        {
            lock (lockThis)
            {
                services.Add(serviceName, serviceObj);
            }
        }
        public Object getService(String serviceName)
        {
            lock (lockThis)
            {
                return services[serviceName];
            }
        }
        public bool removeService(String serviceName)
        {
            try
            {
                services.Remove(serviceName);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
