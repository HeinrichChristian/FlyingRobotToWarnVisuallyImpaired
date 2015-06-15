using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using Caliburn.Micro;
using FollowMe.Messages;

namespace FollowMe
{
    public partial class HuePickerForm : Form
    {
        private readonly IEventAggregator eventAggregator;
        
        private HuePicker huePicker;

        private HuePickerMessageType type;

        public HuePickerForm(IEventAggregator eventAggregator, HuePickerMessage huePickerMessage)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator = eventAggregator;
            
            InitializeComponent();
            huePicker1.Max = huePickerMessage.HueMax;
            huePicker1.Min = huePickerMessage.HueMin;
            huePicker1.Invalidate();
            type = huePickerMessage.Type;
            huePicker1.ValuesChanged += huePicker1_ValuesChanged;
        }

        void huePicker1_ValuesChanged(object sender, EventArgs e)
        {
            eventAggregator.Publish(new HuePickerMessage(type, huePicker1.Min, huePicker1.Max), action =>
            {
                Task.Factory.StartNew(action);

            });
        }
     
    }
}
