using System;
using System.Collections.Generic;
using Aton.ServiceHost.ServiceConfigHandler;
using System.Collections.Concurrent;
using System.IO;
using Aton.ServiceHost.Common;

namespace Aton.ServiceHost
{
    /// <summary>
    /// service manager 
    /// </summary>
    internal class ServiceManage
    {
        private ConcurrentDictionary<string, IAtonService> dictService = new ConcurrentDictionary<string, IAtonService>();

        public  bool TryGetType(string type, out Type result)
        {
            try
            {
                result = Type.GetType(type, true);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = null;
                return false;
            }
        }
        /// <summary>
        /// create type instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public  bool TryCreateInstance<T>(string type, out T result)
        {
            Type instanceType = null;
            result = default(T);

            if (!TryGetType(type, out instanceType))
                return false;

            try
            {
                object instance = Activator.CreateInstance(instanceType);
                result = (T)instance;
                return true;
            }
            catch (Exception ex)
            {
                NativeHelper.FileTracerHelper.Trace(ex);
                return false;
            }
        }

        /// <summary>
        /// load service config
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public bool LoadServiceInfo(AtonServiceConfigHandler configHandler)
        {
            foreach (var item in configHandler)
            {
                    IAtonService service;
                    if (TryCreateInstance<IAtonService>(item.Type, out service))
                    {
                        dictService.TryAdd(item.Name, service);
                    }
                    else
                    {
                        dictService.Clear();
                        return false;
                    }
            }
            return true;
        }
        /// <summary>
        /// start service
        /// </summary>
        /// <returns></returns>
        public bool StartService()
        {           
            if (dictService.Count == 0) return false;
            foreach (var item in dictService)
            {
                if (!item.Value.StartService())
                    return false;
            }
            return true;
        }
        /// <summary>
        /// stop service
        /// </summary>
        /// <returns></returns>
        public bool StopService()
        {
            if (dictService.Count == 0) return false;
            foreach (var item in dictService)
            {
                if (!item.Value.StopService())
                    return false;
            }
            return true;
        }
    }
}
