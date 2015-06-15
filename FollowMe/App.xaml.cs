using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Logging.log4net;
using log4net.Config;

namespace FollowMe
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            LogManager.GetLog = type => new log4netLogger(type);
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
        }

        public App()
        {
            var log = LogManager.GetLog(GetType());
            log.Info("Hello Caliburn.Micro.Logging.NLog World!");
        }

    }
}
