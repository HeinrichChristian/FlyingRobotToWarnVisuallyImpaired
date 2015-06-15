using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using FollowMe.Configuration;
using FollowMe.EzRobot;
using FollowMe.Interfaces;

namespace FollowMe {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    public class AppBootstrapper : BootstrapperBase
    {
        private CompositionContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure() {
            //container = new SimpleContainer();
            var fileBasedTrackingConfigProvider = new FileBasedTrackingConfigProvider();
            container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            CompositionBatch batch = new CompositionBatch();

            
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            var arDrone = new FlyingRobot.ArDrone();
            batch.AddExportedValue<IFlyingRobot>(arDrone);
            batch.AddExportedValue<IFlyingRobotConfigurationHandler>(arDrone);
            batch.AddExportedValue<ITargetLocatorFactory>(new TatgetLocatorFactory());

            container.Compose(batch);
            
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        //protected override IEnumerable<object> GetAllInstances(Type service) {
        //    return container.GetAllInstances(service);
        //}

        //protected override void BuildUp(object instance) {
        //    container.BuildUp(instance);
        //}

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
            


        }
    }
}