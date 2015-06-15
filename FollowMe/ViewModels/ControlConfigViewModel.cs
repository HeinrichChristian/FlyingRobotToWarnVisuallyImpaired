using System;
using System.ComponentModel.Composition;
using System.Windows.Forms.VisualStyles;
using Caliburn.Micro;

namespace FollowMe.ViewModels
{
    [Export(typeof(ControlConfigViewModel))]
    public class ControlConfigViewModel : PropertyChangedBase
    {
        private string controlConfig;
        private string headerWithTimestamp;

        [ImportingConstructor]
        public ControlConfigViewModel(string controlConfig)
        {
            this.controlConfig = controlConfig;

            HeaderWithTimestamp = string.Format("ControlConfig   -   Stand: {0}", DateTime.Now);
        }

        public string HeaderWithTimestamp
        {
            get { return headerWithTimestamp; }
            private set
            {
                headerWithTimestamp = value;
                NotifyOfPropertyChange(() => HeaderWithTimestamp);
            }
        }

        /// <summary>
        /// The control config exported from AR.Drone
        /// </summary>
        public string ControlConfig
        {
            get { return controlConfig; }
            set
            {
                controlConfig = value;
                NotifyOfPropertyChange(() => ControlConfig);
            }
        }
    }
}
