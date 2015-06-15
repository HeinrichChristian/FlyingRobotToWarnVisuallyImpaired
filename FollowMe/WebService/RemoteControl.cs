using Caliburn.Micro;
using FollowMe.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.WebService
{
    /// <summary>
    /// RemoteControl to control the Ar.Drone indirectly.
    /// You can start and stop the person following.
    /// </summary>
    [ServiceBehavior(
    ConcurrencyMode=ConcurrencyMode.Single,
    InstanceContextMode=InstanceContextMode.Single)]
    public class RemoteControl : IRemoteControl, IHandle<DangerLocationMessage>, IHandle<PersonLocationMessage>
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(RemoteControl));
        private readonly IEventAggregator eventAggregator;

        private Enums.TargetLocation personLocation = Enums.TargetLocation.Unknown;
        private Enums.TargetLocation dangerLocation = Enums.TargetLocation.Unknown;
        public RemoteControl(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator = eventAggregator;

            this.eventAggregator.Subscribe(this);
        }

        /// <summary>
        /// Start the person following
        /// </summary>
        public void Start()
        {
            Log.Info("Start - BECAUSE OF SECURITY REASONS NOT IMPLEMENTED");
            
        }

        /// <summary>
        /// Stop the Person following
        /// </summary>
        public void Stop()
        {
            Log.Info("Stop");
            eventAggregator.Publish(new StopMessage(), action =>
            {
                Task.Factory.StartNew(action);

            });
        }


        public Enums.TargetLocation GetPersonLocation()
        {
            Log.Info("GetPersonLocation {0}", personLocation);            
            return personLocation;
        }

        public Enums.TargetLocation GetDangerLocation()
        {
            Log.Info("GetDangerLocation {0}", dangerLocation);            
            return dangerLocation;
        }


        public PersonAndDangerLocation GetPersonAndDangerLocation()
        {
            Log.Info("GetPersonAndDangerLocation, person: {0}, danger: {1}", personLocation, dangerLocation);            
            return new PersonAndDangerLocation 
            { 
                DangerLocation = dangerLocation,
                PersonLocation = personLocation
            };
        }

        public void Handle(DangerLocationMessage message)
        {
            this.dangerLocation = message.DangerLocation;
        }

        public void Handle(PersonLocationMessage message)
        {
            this.personLocation = message.PersonLocation;
        }
    }
}
