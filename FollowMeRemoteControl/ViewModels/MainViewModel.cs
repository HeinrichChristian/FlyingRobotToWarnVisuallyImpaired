using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FollowMe.RemoteControl.Resources;
using System.Windows;

namespace FollowMe.RemoteControl.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
         
        }

        /// <summary>
        /// Eine Auflistung für ItemViewModel-Objekte.
        /// </summary>
     
        private bool personDetected;

        private bool dangerDetected;

        private string dangerLocation;
        private string personLocation;

        private string errorMessage;

        private bool errorOccured;


        public bool PersonDetected
        {
            get
            {
                return personDetected;
            }

            set
            {
                personDetected = value;
                NotifyPropertyChanged("PersonDetected");
            }
        }

        public bool DangerDetected
        {
            get
            {
                return dangerDetected;
            }

            set
            {
                dangerDetected = value;
                NotifyPropertyChanged("DangerDetected");
            }
        }
       
        public string PersonLocation
        {
            get { return personLocation; }
            
            set
            {
                personLocation = value;
                NotifyPropertyChanged("PersonLocation");
            }
        }

        public string DangerLocation
        {
            get
            {
                return dangerLocation;
            }
            set
            {
                dangerLocation = value;
                NotifyPropertyChanged("DangerLocation");
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                if(value == string.Empty)
                {
                    ErrorOccured = false;
                }
                else
                {
                    ErrorOccured = true;
                }
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        public bool ErrorOccured
        {
            get { return errorOccured; }
            set
            {
                errorOccured = value;
                NotifyPropertyChanged("ErrorOccured");                
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Erstellt einige ItemViewModel-Objekte und fügt diese zur Items-Auflistung hinzu.
        /// </summary>
        public void LoadData()
        {
            // TODO connect to service

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    
    }
}