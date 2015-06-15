using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FollowMe.RemoteControl.Resources;
using FollowMe.RemoteControl.ViewModels;
using System.Windows.Threading;
using FollowMe.RemoteControl.FollowMeService;
using System.ComponentModel;
using Microsoft.Devices;
using Windows.Phone.Speech.Synthesis;

namespace FollowMe.RemoteControl
{
    public partial class MainPage : PhoneApplicationPage
    {
        RemoteControlClient remoteControlClient = new RemoteControlClient();
        VibrateController vibrateController = VibrateController.Default;
        private bool personLocalized;
        public bool PersonLocalized
        {
            get
            {
                return personLocalized;
            }
            set
            {
                if (personLocalized == value)
                    return;

                personLocalized = value;
                if(value)
                {
                    Speak("Found you!");
                }
                else
                {
                    Speak("Lost you!");
                }
            }
        }
        private DispatcherTimer pollTimer;
        
        // Konstruktor
        public MainPage()
        {
            InitializeComponent();

            // Datenkontext des Steuerelements LongListSelector auf die Beispieldaten festlegen
            DataContext = App.ViewModel;
            remoteControlClient.GetPersonAndDangerLocationCompleted += remoteControlClient_GetPersonAndDangerLocationCompleted;
            pollTimer = new System.Windows.Threading.DispatcherTimer();
            pollTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            pollTimer.Interval = new TimeSpan(0, 0, 1);
            pollTimer.Start();
      
            vibrateController.Start(TimeSpan.FromMilliseconds(500));         
        }

        private void Speak(string textToSpeak)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            
            synth.SpeakTextAsync(textToSpeak);
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                remoteControlClient.GetPersonAndDangerLocationAsync();
            }
            catch (Exception exception)
            {
                Dispatcher.BeginInvoke(
                  () =>
                  {
                      App.ViewModel.ErrorMessage = exception.ToString();
                  });
            }
        }
        void remoteControlClient_GetPersonAndDangerLocationCompleted(object sender, GetPersonAndDangerLocationCompletedEventArgs e)
        {
            FollowMe.Enums.TargetLocation personLocation = FollowMe.Enums.TargetLocation.Unknown;
            FollowMe.Enums.TargetLocation dangerLocation = FollowMe.Enums.TargetLocation.Unknown;

            try
            {

                personLocation = e.Result.PersonLocation;
                dangerLocation = e.Result.DangerLocation;
            }
            catch (Exception exception)
            {
                Dispatcher.BeginInvoke(
                    () =>
                    {
                        App.ViewModel.ErrorMessage = exception.ToString();
                    });
                return;
            }


            Dispatcher.BeginInvoke(
                 () =>
                 {
                     App.ViewModel.ErrorMessage = string.Empty;
                 });



            if (personLocation.ToString() == TargetLocation.Unknown.ToString())
            {
                PersonLocalized = false;
                Dispatcher.BeginInvoke(
                    () =>
                    {
                        App.ViewModel.PersonDetected = false;
                        App.ViewModel.PersonLocation = string.Empty;
                    });
            }
            else
            {
                PersonLocalized = true;
                Dispatcher.BeginInvoke(
                    () =>
                    {
                        App.ViewModel.PersonDetected = true;
                        App.ViewModel.PersonLocation = personLocation.ToString();
                    });

            }

            if(dangerLocation.ToString() == TargetLocation.Unknown.ToString())
            {
                Dispatcher.BeginInvoke(
                                  () =>
                                  {
                                      App.ViewModel.DangerDetected = false;
                                      App.ViewModel.DangerLocation = string.Empty;
                                  });
            }
            else
            {
                vibrateController.Start(TimeSpan.FromMilliseconds(1000));
               
                if(dangerLocation.ToString().ToLower().Contains("right"))
                {
                    Speak("Danger ahead, right side!");
                }
                else if (dangerLocation.ToString().ToLower().Contains("left"))
                {
                    Speak("Danger ahead, left side!");
                } else  if (dangerLocation.ToString().ToLower().Contains("center"))
                {
                    Speak("Danger ahead!");
                }

                Dispatcher.BeginInvoke(
                    () =>
                    {
                        App.ViewModel.DangerDetected = true;
                        App.ViewModel.DangerLocation = dangerLocation.ToString();
                    });

            }
        }
          
      



        public void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            Speak("Request Stop!");
            remoteControlClient.StopCompleted += remoteControlClient_StopCompleted;
            remoteControlClient.StopAsync();
        }

        void remoteControlClient_StopCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }
    }
}