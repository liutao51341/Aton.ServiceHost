using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.IO;
using Aton.ServiceHost.ServiceConfigHandler;


namespace Aton.ServiceHost
{
    partial class MainService : ServiceBase
    {
        AtonServiceConfigHandler configHandler;
        ServiceManage sm;

        public MainService()
        {
            InitializeComponent();
            try
            {
                configHandler = new AtonServiceConfigHandler();
                sm = new ServiceManage();
            }
            catch (Exception ex)
            {
                NativeHelper.FileTracerHelper.Trace(ex);
            }
      
        }

        protected override void OnStart(string[] args)
        {
            if (sm.LoadServiceInfo(configHandler))
            {
                sm.StartService();
                base.OnStart(args);
            }
            else
            {
                NativeHelper.FileTracerHelper.Trace("Start host service fail");
               
            }
        }

        protected override void OnStop()
        {
            sm.StartService();
            base.OnStop();
        }

        protected override void OnShutdown()
        {
            sm.StopService();
            base.OnShutdown();
        }
    }
}
