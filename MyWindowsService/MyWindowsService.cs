using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MyWindowsService
{
    public partial class MyWindowsService : ServiceBase
    {
        
        public MyWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HelperClass helper = new HelperClass();
            if(helper.CheckForInternetConnection())
            {
                helper.WriteLog("Service started");
                Message m = new Message("Windows Service started at "+helper.GetIPAddress());
                helper.SendNotification(m);
            }
        }

        protected override void OnStop()
        {
            HelperClass helper = new HelperClass();
            if (helper.CheckForInternetConnection())
            {
                helper.WriteLog("Service stopped");
                Message m = new Message("Windows Service stopped at " +helper.GetIPAddress());
                helper.SendNotification(m);
            }
        }
    }
}
