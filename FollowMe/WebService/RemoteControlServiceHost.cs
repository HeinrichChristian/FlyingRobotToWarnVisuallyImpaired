using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.WebService
{
    public sealed class RemoteControlServiceHost
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(RemoteControlServiceHost));

        private static ServiceHost remoteControlServiceHost;
        
        private static readonly object padlock = new object();


        private RemoteControlServiceHost()
        {
            Log.Info("private RemoteControlServiceHost()");
        }

        
        public static ServiceHost Instance
        {
            get
            {
                lock(padlock)
                {                    
                    if(remoteControlServiceHost == null)
                    {
                        try
                        {
                            var eventAggregator = IoC.Get<IEventAggregator>();
                            remoteControlServiceHost = new ServiceHost(new RemoteControl(eventAggregator));                            
                        }
                        catch(Exception e)
                        {
                            System.Windows.MessageBox.Show(e.ToString());
                        }
                        
                    }

                }
                return remoteControlServiceHost;
            }
        }

        public static void Start()
        {
            remoteControlServiceHost.Open();
        }

        public static void Stop()
        {
            remoteControlServiceHost.Close();
            remoteControlServiceHost = null;
        }
    }
}
