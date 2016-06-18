using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using Caliburn.Micro;
using EZ_B;
using EZ_B.Joystick;
using FollowMe.ArDrone;
using FollowMe.Configuration;
using FollowMe.Enums;
using FollowMe.EzRobot;
using FollowMe.Interfaces;
using FollowMe.Messages;
using FollowMe.ViewModels;
using Timer = System.Timers.Timer;
using FollowMe.WebService;
using System.ServiceModel;
using System.Threading.Tasks;

namespace FollowMe {
    /// <summary>
    /// The viewModel for the shellview.
    /// </summary>
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : PropertyChangedBase, IShell, IHandle<HuePickerMessage>, IHandle<StopMessage>
    {
        #region privates

        private readonly float autonomousFlyingSteeringForce = 0.002f;

        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IFlyingRobot flyingRobot;
        private readonly IFlyingRobotConfigurationHandler flyingRobotConfigurationHandler;
        private readonly ITargetLocatorFactory targetLocatorFactory;
        private ITargetLocator targetLocator;
        private static readonly ILog Log = LogManager.GetLog(typeof(ShellViewModel));
        private readonly FileBasedTrackingConfigProvider fileBasedTrackingConfigProvider = new FileBasedTrackingConfigProvider();
        private Joystick joystick;
        private Camera camera;
        private int batteryLevel;
        private bool button1Pressed;
        private bool button2Pressed;
        private bool button3Pressed;
        private bool button4Pressed;
        private bool button5Pressed;
        private bool button6Pressed;
        private bool button7Pressed;
        private bool button8Pressed;
        private float leftStickXAxis;
        private float leftStickYAxis;
        private float rightStickXAxis;
        private float rightStickYAxis;
        private float targetXCoordinate;
        private float targetYCoordinate;
        private List<JoystickDevice> availableJoystickDevices;
        private string qrCode;
        private Control cameraPanel = new Control();
        private JoystickDevice selectedJoystickDevice;
        private string droneStatus;
        private HuePickerForm huePickerForm;
        private HuePickerForm dangerHuePickerForm;
        private bool connectedToFlyingRobot;
        private string selectedJoystick;
        private float maxYaw;
        private int maxVerticalSpeed;
        private float maxEulerAngle;
        private int maxAltitude;
        private bool isOutside;
        private bool flyingWithoutShell;
        private ArDroneConfig arDroneConfig = new ArDroneConfig();
        private bool objectDetectionEnabled;
        private bool cameraStarted;
        private int searchObjectSizePixels;
        private byte colorBrightness;
        private float saturationMin;
        private float saturationMax;
        private float luminanceMin;
        private float luminanceMax;
        private int hueMin;
        private int hueMax;
        private bool huePickerIsVisible;

        private bool dangerTrackingPreviewEnabled;
        private int dangerSearchObjectSizePixels;
        private byte dangerColorBrightness;
        private float dangerSaturationMin;
        private float dangerSaturationMax;
        private float dangerLuminanceMax;
        private int dangerHueMin;
        private int dangerHueMax;
        private float dangerLuminanceMin;

        readonly CameraPreviewForm cameraPreviewForm = new CameraPreviewForm();
        private bool trackingPreviewEnabled;
        private bool searchObjectTopLeft;
        private bool searchObjectTopCenter;
        private bool searchObjectTopRight;
        private bool searchObjectCenterLeft;
        private bool searchObjectCenterCenter;
        private bool searchObjectCenterRight;
        private bool searchObjectBottomLeft;
        private bool searchObjectBottomCenter;
        private bool searchObjectBottomRight;
        private TargetLocation LastKnownTargetLocationByColor;
        private string commandsForAutonomousFlight;

        private bool flyingAtonomous;
        private Timer autonomousTimer;
        private bool searchObjectLocationUnknown;

        private bool remoteControlServiceIsRunning;
        private bool remoteControlRequestsStop;

        private ServiceHost remoteControlServiceHost;

        #endregion
        
        #region public properties

        /// <summary>
        /// The Config of the AR.Drone
        /// </summary>
        public ArDroneConfig ArDroneConfig
        {
            get
            {
                return arDroneConfig;

            }
            set
            {

                arDroneConfig = value;
                NotifyOfPropertyChange(() => ArDroneConfig);
            }
        }

        /// <summary>
        /// The battery level of the AR.Drone - value between 0 and 100, lower than 17 is not good
        /// </summary>
        public int BatteryLevel
        {
            get { return batteryLevel; }
            set
            {
                batteryLevel = value;
                NotifyOfPropertyChange(() => BatteryLevel);
            }
        }

        public String FoundFreeParkingLots
        {
            get { return freeParkingLotString; }
            set
            {
                freeParkingLotString = value;
                NotifyOfPropertyChange(() => FoundFreeParkingLots);
            }
        }


        public bool ConnectedToFlyingRobot
        {
            get { return connectedToFlyingRobot; }
            set
            {
                connectedToFlyingRobot = value;
                NotifyOfPropertyChange(() => ConnectedToFlyingRobot);
            }
        }

        /// <summary>
        /// If true, the detection of objects is enabled
        /// </summary>
        public bool ObjectDetectionEnabled
        {
            get { return objectDetectionEnabled; }
            set
            {
                objectDetectionEnabled = value;
                if (value)
                {
                    targetLocator = targetLocatorFactory.CreateTargetLocator(this.camera);
                }
                NotifyOfPropertyChange(() => ObjectDetectionEnabled);
            }
        }

        public bool TrackingPreviewEnabled
        {
            get { return trackingPreviewEnabled; }
            set
            {
                trackingPreviewEnabled = value;
                NotifyOfPropertyChange(() => TrackingPreviewEnabled);
            }
        }

        public bool DangerTrackingPreviewEnabled
        {
            get { return dangerTrackingPreviewEnabled; }
            set
            {
                dangerTrackingPreviewEnabled = value;
                NotifyOfPropertyChange(() => DangerTrackingPreviewEnabled);
            }
        }

        public bool CameraStarted
        {
            get { return cameraStarted; }
            set
            {
                cameraStarted = value;
                NotifyOfPropertyChange(() => CameraStarted);
            }
        }

        /// <summary>
        /// true, if the button 1 of the joystick is pressed
        /// </summary>
        public bool Button1Pressed
        {
            get { return button1Pressed; }
            set
            {
                button1Pressed = value;
                NotifyOfPropertyChange(() => Button1Pressed);
            }
        }
        
        /// <summary>
        /// true, if the button 2 of the joystick is pressed
        /// </summary>
        public bool Button2Pressed
        {
            get { return button2Pressed; }
            set
            {
                button2Pressed = value;
                NotifyOfPropertyChange(() => Button2Pressed);
            }
        }

        /// <summary>
        /// true, if the button 3 of the joystick is pressed
        /// </summary>
        public bool Button3Pressed
        {
            get { return button3Pressed; }
            set
            {
                button3Pressed = value;
                NotifyOfPropertyChange(() => Button3Pressed);
            }
        }

        /// <summary>
        /// true, if the button 2 of the joystick is pressed
        /// </summary>
        public bool Button4Pressed
        {
            get { return button4Pressed; }
            set
            {
                button4Pressed = value;
                NotifyOfPropertyChange(() => Button4Pressed);
            }
        }

        /// <summary>
        /// true, if the button 5 of the joystick is pressed
        /// </summary>
        public bool Button5Pressed
        {
            get { return button5Pressed; }
            set
            {
                button5Pressed = value;
                NotifyOfPropertyChange(() => Button5Pressed);
            }
        }

        /// <summary>
        /// true, if the button 6 of the joystick is pressed
        /// </summary>
        public bool Button6Pressed
        {
            get { return button6Pressed; }
            set
            {
                button6Pressed = value;
                NotifyOfPropertyChange(() => Button6Pressed);
            }
        }

        /// <summary>
        /// true, if the button 7 of the joystick is pressed
        /// </summary>
        public bool Button7Pressed
        {
            get { return button7Pressed; }
            set
            {
                button7Pressed = value;
                NotifyOfPropertyChange(() => Button7Pressed);
            }
        }

        /// <summary>
        /// true, if the button 8 of the joystick is pressed
        /// Used to TakeOff
        /// </summary>
        public bool Button8Pressed
        {
            get { return button8Pressed; }
            set
            {
                button8Pressed = value;
                NotifyOfPropertyChange(() => Button8Pressed);
            }
        }

        /// <summary>
        /// Yaw - X axis of left stick
        /// </summary>
        public float LeftStickXAxis
        {
            get { return leftStickXAxis; }
            set
            {
                leftStickXAxis = value; 
                NotifyOfPropertyChange(() => LeftStickXAxis);
            }
        }

        /// <summary>
        /// Pitch - Y axis of left stick
        /// </summary>
        public float LeftStickYAxis
        {
            get { return leftStickYAxis; }
            set
            {
                leftStickYAxis = value; 
                NotifyOfPropertyChange(() => LeftStickYAxis);
            }
        }

        /// <summary>
        /// Roll - X axis of right stick
        /// </summary>
        public float RightStickXAxis
        {
            get { return rightStickXAxis; }
            set
            {
                rightStickXAxis = value;
                NotifyOfPropertyChange(() => RightStickXAxis);
            }
        }

        /// <summary>
        /// Nick -  Y axis of right stick
        /// </summary>
        public float RightStickYAxis
        {
            get { return rightStickYAxis; }
            set
            {
                rightStickYAxis = value; 
                NotifyOfPropertyChange(() => RightStickYAxis);
            }
        }

        /// <summary>
        /// The X coordinate of a recognized target
        /// </summary>
        public float TargetXCoordinate
        {
            get { return targetXCoordinate; }
            set
            {
                targetXCoordinate = value; 
                NotifyOfPropertyChange(() => TargetXCoordinate);
            }
        }

        /// <summary>
        /// The Y coordinate of a recognized target
        /// </summary>
        public float TargetYCoordinate
        {
            get { return targetYCoordinate; }
            set
            {
                targetYCoordinate = value; 
                NotifyOfPropertyChange(() => TargetYCoordinate);
            }
        }

        /// <summary>
        /// The list of all actual available joysticks on the machine
        /// </summary>
        public List<JoystickDevice> AvailableJoystickDevices
        {
            get
            {
                if (availableJoystickDevices == null)
                {
                    availableJoystickDevices = new List<JoystickDevice>();
                    RefreshJoysticks();
                }
                return availableJoystickDevices;
            }
            set
            {
                availableJoystickDevices = value; 
                NotifyOfPropertyChange(() => AvailableJoystickDevices);
            }
        }

        /// <summary>
        /// The selected joystick
        /// </summary>
        public JoystickDevice SelectedJoystickDevice
        {
            get { return selectedJoystickDevice; }
            set
            {
                selectedJoystickDevice = value;
                if (value != null)
                {
                    ActivateJoystick();
                }
                SelectedJoystick = string.Format("{0} Axes, {1} Buttons", selectedJoystickDevice.Axes, selectedJoystickDevice.Buttons);
                NotifyOfPropertyChange(() => SelectedJoystickDevice);
            }
        }

        public string SelectedJoystick
        {
            get { return selectedJoystick; }
            set
            {
                selectedJoystick = value; 
                NotifyOfPropertyChange(() => SelectedJoystick);
            }
        }

        /// <summary>
        /// The text of therecognized QR-Code
        /// </summary>
        public string QrCode
        {
            get { return qrCode; }
            set
            {
                qrCode = value; 
                NotifyOfPropertyChange(() => QrCode);
            }
        }

        /// <summary>
        /// The control to show the camera view
        /// </summary>
        public Control CameraPanel
        {
            get { return cameraPanel; }
            set
            {
                cameraPanel = value;
                NotifyOfPropertyChange(() => CameraPanel);
            }
        }

        public string DroneStatus
        {
            get { return droneStatus; }
            set
            {
                droneStatus = value;
                NotifyOfPropertyChange(() => DroneStatus);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HuePickerIsVisible
        {
            get { return huePickerIsVisible; }
            set
            {
                huePickerIsVisible = value;
                NotifyOfPropertyChange(() => HuePickerIsVisible);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DangerHuePickerIsVisible
        {
            get { return dangerHuePickerIsVisible; }
            set
            {
                dangerHuePickerIsVisible = value;
                NotifyOfPropertyChange(() => DangerHuePickerIsVisible);
            }
        }
        #endregion

        #region Object tracking - person

        public int SearchObjectSizePixels
        {
            get { return searchObjectSizePixels; }
            set
            {
                searchObjectSizePixels = value;
                NotifyOfPropertyChange(() => SearchObjectSizePixels);
            }
        }

        public byte ColorBrightness
        {
            get { return colorBrightness; }
            set
            {
                colorBrightness = value; 
                NotifyOfPropertyChange(() => ColorBrightness);
            }
        }

        public float SaturationMin
        {
            get { return saturationMin; }
            set
            {
                saturationMin = value; 
                NotifyOfPropertyChange(() => SaturationMin);
            }
        }

        public float SaturationMax
        {
            get { return saturationMax; }
            set
            {
                saturationMax = value;
                NotifyOfPropertyChange(() => SaturationMax);
            }
        }

        public float LuminanceMin
        {
            get { return luminanceMin; }
            set
            {
                luminanceMin = value;
                NotifyOfPropertyChange(() => LuminanceMin);
            }
        }

        public float LuminanceMax
        {
            get { return luminanceMax; }
            set
            {
                luminanceMax = value;
                NotifyOfPropertyChange(() => LuminanceMax);
            }
        }

        public int HueMin
        {
            get { return hueMin; }
            set
            {
                hueMin = value;
                NotifyOfPropertyChange(() => HueMin);
            }
        }

        public int HueMax
        {
            get { return hueMax; }
            set
            {
                hueMax = value;
                NotifyOfPropertyChange(() => HueMax);
            }
        }

        #region Visualization of target in camera view
        public bool SearchObjectTopLeft
        {
            get { return searchObjectTopLeft; }
            set
            {
                searchObjectTopLeft = value;
                NotifyOfPropertyChange(() => SearchObjectTopLeft);
            }
        }

        public bool SearchObjectTopCenter
        {
            get { return searchObjectTopCenter; }
            set
            {
                searchObjectTopCenter = value;
                NotifyOfPropertyChange(() => SearchObjectTopCenter);
            }
        }

        public bool SearchObjectTopRight
        {
            get { return searchObjectTopRight; }
            set
            {
                searchObjectTopRight = value;
                NotifyOfPropertyChange(() => SearchObjectTopRight);
            }
        }

        public bool SearchObjectCenterLeft
        {
            get { return searchObjectCenterLeft; }
            set
            {
                searchObjectCenterLeft = value;
                NotifyOfPropertyChange(() => SearchObjectCenterLeft);
            }
        }

        public bool SearchObjectCenterCenter
        {
            get { return searchObjectCenterCenter; }
            set
            {
                searchObjectCenterCenter = value; 
                NotifyOfPropertyChange(() => SearchObjectCenterCenter);
            }
        }

        public bool SearchObjectCenterRight
        {
            get { return searchObjectCenterRight; }
            set
            {
                searchObjectCenterRight = value;
                NotifyOfPropertyChange(() => SearchObjectCenterRight);
            }
        }

        public bool SearchObjectBottomLeft
        {
            get { return searchObjectBottomLeft; }
            set
            {
                searchObjectBottomLeft = value; 
                NotifyOfPropertyChange(() => SearchObjectBottomLeft);
            }
        }

        public bool SearchObjectBottomCenter
        {
            get { return searchObjectBottomCenter; }
            set
            {
                searchObjectBottomCenter = value;
                NotifyOfPropertyChange(() => SearchObjectBottomCenter);
            }
        }

        public bool SearchObjectBottomRight
        {
            get { return searchObjectBottomRight; }
            set
            {
                searchObjectBottomRight = value;
                NotifyOfPropertyChange(() => SearchObjectBottomRight);
            }
        }

        public bool SearchObjectLocationUnknown
        {
            get { return searchObjectLocationUnknown; }
            set
            {
                searchObjectLocationUnknown = value;
                NotifyOfPropertyChange(() => SearchObjectLocationUnknown);
            }
        }

        private bool glyphAheadDetected;
        private string remoteServiceUri;
        private bool dangerHuePickerIsVisible;
        private QrCodeDetection qrCodeDetection;
        private HashSet<string> freeParkingLots = new HashSet<string>();
        private string freeParkingLotString;

        public bool GlyphAheadDetected
        { 
            get { return glyphAheadDetected; }
            set
            {
                glyphAheadDetected = value;
                NotifyOfPropertyChange(() => GlyphAheadDetected);
            }
        }

        #endregion

        #endregion

        #region Object tracking - danger

        public int DangerSearchObjectSizePixels
        {
            get { return dangerSearchObjectSizePixels; }
            set
            {
                dangerSearchObjectSizePixels = value;
                NotifyOfPropertyChange(() => DangerSearchObjectSizePixels);
            }
        }

        public byte DangerColorBrightness
        {
            get { return dangerColorBrightness; }
            set
            {
                dangerColorBrightness = value;
                NotifyOfPropertyChange(() => DangerColorBrightness);
            }
        }

        public float DangerSaturationMin
        {
            get { return dangerSaturationMin; }
            set
            {
                dangerSaturationMin = value;
                NotifyOfPropertyChange(() => DangerSaturationMin);
            }
        }

        public float DangerSaturationMax
        {
            get { return dangerSaturationMax; }
            set
            {
                dangerSaturationMax = value;
                NotifyOfPropertyChange(() => DangerSaturationMax);
            }
        }

        public float DangerLuminanceMin
        {
            get { return dangerLuminanceMin; }
            set
            {
                dangerLuminanceMin = value;
                NotifyOfPropertyChange(() => DangerLuminanceMin);
            }
        }

        public float DangerLuminanceMax
        {
            get { return dangerLuminanceMax; }
            set
            {
                dangerLuminanceMax = value;
                NotifyOfPropertyChange(() => DangerLuminanceMax);
            }
        }

        public int DangerHueMin
        {
            get { return dangerHueMin; }
            set
            {
                dangerHueMin = value;
                NotifyOfPropertyChange(() => DangerHueMin);
            }
        }

        public int DangerHueMax
        {
            get { return dangerHueMax; }
            set
            {
                dangerHueMax = value;
                NotifyOfPropertyChange(() => DangerHueMax);
            }
        }

        #endregion

        #region Autonomous flight

        public string CommandsForAutonomousFlight
        {
            get { return commandsForAutonomousFlight; }
            set
            {
                commandsForAutonomousFlight = value; 
                NotifyOfPropertyChange(() => CommandsForAutonomousFlight);
            }
        }

        public bool FlyingAtonomous
        {
            get { return flyingAtonomous; }
            set
            {
                flyingAtonomous = value;
                NotifyOfPropertyChange(() => FlyingAtonomous);
            }
        }

        #endregion

        #region Service for SmartPhone

        public string RemoteServiceUri
        {
            get { return remoteServiceUri; }
            set
            {
                remoteServiceUri = value;
                NotifyOfPropertyChange(() => RemoteServiceUri);
            }
        }

        public bool RemoteControlServiceIsRunning
        {
            get { return remoteControlServiceIsRunning; }
            set
            {
                remoteControlServiceIsRunning = value;
                NotifyOfPropertyChange(() => RemoteControlServiceIsRunning);
            }
        }

        public bool RemoteControlRequestsStop
        {
            get { return remoteControlRequestsStop; }
            set
            {
                remoteControlRequestsStop = value;
                NotifyOfPropertyChange(() => RemoteControlRequestsStop);
            }
        }
        public void ButtonStartRemoteControlServiceHost()
        {
            Log.Info("Start RemoteControlServiceHost");
            RemoteControlServiceIsRunning = true;
            remoteControlServiceHost = RemoteControlServiceHost.Instance;

            RemoteControlServiceHost.Start();
            
            foreach (var address in remoteControlServiceHost.BaseAddresses)
            {
                RemoteServiceUri = address.ToString();
                break;
            }
        }

        public void ButtonResetRequestedStop()
        {
            Log.Info("Reset requested stop");
            RemoteControlRequestsStop = false;
        }

        public void ButtonStopRemoteControlServiceHost()
        {
            Log.Info("Stop RemoteControlServiceHost");
            RemoteControlServiceIsRunning = false;
            RemoteControlServiceHost.Stop();

            RemoteServiceUri = "Service gestoppt.";
            
        }

        #endregion

        #region Settings

        /// <summary>
        /// Set true if you are flying outside 
        /// ->  Method:EZ_B.AR­Drone.­Drone­Control.­Set­Is­Outside(­System.­Boolean) 
        /// </summary>
        public bool IsOutside
        {
            get { return isOutside; }
            set
            {
                isOutside = value; 
                NotifyOfPropertyChange(() => IsOutside);
            }
        }

        /// <summary>
        /// Set to TRUE if you are flying with the outside shell 
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Is­Flying­Without­Shell(­System.­Boolean) 
        /// </summary>
        public bool FlyingWithoutShell
        {
            get { return flyingWithoutShell; }
            set
            {
                flyingWithoutShell = value;
                NotifyOfPropertyChange(() => FlyingWithoutShell);
            }
        }

        /// <summary>
        /// Maximum yaw (spin) speed of the AR.Drone, in radians per second. 
        /// Recommanded values goes from (0.7) 40/s to (6.11) 350/s. Others values may cause instability. Default: 3.0 
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Yaw(­System.­Single) 
        /// </summary>
        public float MaxYaw
        {
            get { return maxYaw; }
            set
            {
                maxYaw = value;
                NotifyOfPropertyChange(() => MaxYaw);
            }
        }

        /// <summary>
        /// Maximum vertical speed of the AR.Drone, in milimeters per second. 
        /// Recommanded values goes from 200 to 2000. Others values may cause instability. Default: 1000 
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­SetVZ­Max(­System.­Int32) 
        /// </summary>
        public int MaxVerticalSpeed
        {
            get { return maxVerticalSpeed; }
            set
            {
                maxVerticalSpeed = value; 
                NotifyOfPropertyChange(() => MaxVerticalSpeed);
            }
        }

        /// <summary>
        /// Set maximum bending angle for drone in radians for pitch and roll. 
        /// I.E. Maximum angle for going forward, back, left or right.
        /// This does not affect YAW (spin) Floating point between 0 (0 deg) and 0.52 (32 deg) Default: 0.25 
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Euler­Angle­Max(­System.­Single) 
        /// </summary>
        public float MaxEulerAngle
        {
            get { return maxEulerAngle; }
            set
            {
                maxEulerAngle = value;
                NotifyOfPropertyChange(() => MaxEulerAngle);
            }
        }

        /// <summary>
        /// Maximum drone altitude in millimeters.
        /// Give an integer value between 500 and 5000 to prevent the drone from flying above this limit, 
        /// or set it to 10000 to let the drone fly as high as desired. Default: 3000 
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Altitude­Max(­System.­Int32)
        /// </summary>
        public int MaxAltitude
        {
            get { return maxAltitude; }
            set
            {
                maxAltitude = value;
                NotifyOfPropertyChange(() => MaxAltitude);
            }
        }

        #endregion


        /// <summary>
        /// Constructor.
        /// Starts a timer which checks every 5 seconds the battery level of the connected AR.Drone
        /// </summary>
        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IFlyingRobot flyingRobot, IFlyingRobotConfigurationHandler flyingRobotConfigurationHandler, ITargetLocatorFactory targetLocatorFactory)
        {
            if (windowManager == null) throw new ArgumentNullException("windowManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (flyingRobot == null) throw new ArgumentNullException("flyingRobot");
            if (flyingRobotConfigurationHandler == null)
                throw new ArgumentNullException("flyingRobotConfigurationHandler");
            if (targetLocatorFactory == null) throw new ArgumentNullException("targetLocatorFactory");
            

            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;
            this.flyingRobot = flyingRobot;
            this.flyingRobotConfigurationHandler = flyingRobotConfigurationHandler;
            this.targetLocatorFactory = targetLocatorFactory;
            this.eventAggregator.Subscribe(this);

            Log.Info("Init");
            
            RefreshJoysticks();

            ConnectedToFlyingRobot = false;
            HuePickerIsVisible = false;

            var arDroneStatusTimer = new Timer();
            arDroneStatusTimer.Elapsed += OnArDroneStatusTimedEvent;
            arDroneStatusTimer.Interval = 10000;
            arDroneStatusTimer.Enabled = true;


            var trackingConfig = fileBasedTrackingConfigProvider.LoadTrackingConfig();

            // Person tracking config
            SearchObjectSizePixels = trackingConfig.SearchObjectSizePixels;
            HueMax = trackingConfig.HueMax;
            HueMin = trackingConfig.HueMin;
            SaturationMax = trackingConfig.SaturationMax;
            SaturationMin = trackingConfig.SaturationMin;
            LuminanceMax = trackingConfig.LuminanceMax;
            LuminanceMin = trackingConfig.LuminanceMin;

            // Danger tracking config
            DangerSearchObjectSizePixels = trackingConfig.DangerSearchObjectSizePixels;
            DangerHueMax = trackingConfig.DangerHueMax;
            DangerHueMin = trackingConfig.DangerHueMin;
            DangerLuminanceMax = trackingConfig.DangerLuminanceMax;
            DangerLuminanceMin = trackingConfig.DangerLuminanceMin;
            DangerSaturationMax = trackingConfig.DangerSaturationMax;
            DangerSaturationMin = trackingConfig.DangerSaturationMin;

        }

        #region buttons
        /// <summary>
        /// Connect to AR.Drone.
        /// The PC must first be connected via WIFI to the AR.Drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonConnect(object sender, RoutedEventArgs e)
        {
            try
            {
                var controlConfig = flyingRobot.Connect();

                Log.Info(controlConfig);
                if (!string.IsNullOrEmpty(controlConfig))
                {
                    DroneStatus = "Verbunden";
                    ConnectedToFlyingRobot = true;
                }
                else
                {
                    DroneStatus = "Nicht Verbunden";
                    ConnectedToFlyingRobot = false;
                }
            }
            catch (Exception exception)
            {
                DroneStatus = "Fehler " + exception;
                ConnectedToFlyingRobot = false;
                Log.Error(exception);
            }
        }

       

        /// <summary>
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Yaw(­System.­Single) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendMaxYaw(object sender, RoutedEventArgs e)
        {
            Log.Info("SetYaw: {0}", MaxYaw);
            flyingRobotConfigurationHandler.SetMaxYaw(MaxYaw);
        }

        /// <summary>
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­SetVZ­Max(­System.­Int32) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendMaxVerticalSpeed(object sender, RoutedEventArgs e)
        {
            Log.Info("SetVZMax: {0}", MaxVerticalSpeed);
            flyingRobotConfigurationHandler.SetMaxVerticalSpeed(MaxVerticalSpeed);
        }

        /// <summary>
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Euler­Angle­Max(­System.­Single)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendMaxEulerAngle(object sender, RoutedEventArgs e)
        {
            Log.Info("SetMaxEulerAngle: {0}", MaxEulerAngle);
            flyingRobotConfigurationHandler.SetMaxEulerAngle(MaxEulerAngle);
        }

        /// <summary>
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Altitude­Max(­System.­Int32)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendMaxAltitude(object sender, RoutedEventArgs e)
        {
            Log.Info("SetAltitudeMax: {0}", MaxAltitude);
            flyingRobotConfigurationHandler.SetMaxAltitude(MaxAltitude);
        }

        /// <summary>
        ///  ->  Method:EZ_B.AR­Drone.­Drone­Control.­Set­Is­Outside(­System.­Boolean) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendIsOutside(object sender, RoutedEventArgs e)
        {
            Log.Info("SetIsOutside: {0}", IsOutside);
            flyingRobotConfigurationHandler.SetIsOutside(IsOutside);
        }

        /// <summary>
        /// -> Method:EZ_B.AR­Drone.­Drone­Control.­Set­Is­Flying­Without­Shell(­System.­Boolean) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendFlyingWithoutShell(object sender, RoutedEventArgs e)
        {
            Log.Info("SetIsFlyingWithoutShell: {0}", FlyingWithoutShell);
            flyingRobotConfigurationHandler.SetIsFlyingWithoutShell(FlyingWithoutShell);
        }

        /// <summary>
        /// Start the camera.
        /// Set handler for OnNewFrame event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonShowCamera(object sender, RoutedEventArgs e)
        {
            flyingRobot.SwitchVideoChannelToVertical();

            camera = new Camera(UcezbConnectProvider.Instance.EZB);
            camera.OnNewFrame += _camera_QrDetection;
           
            camera.OnNewFrame += _camera_OnNewFrame;
            Log.Info("StartCamera");

            cameraPreviewForm.Show();
            camera.StartCamera(
                    new ValuePair(Camera.CAMERA_NAME_AR_DRONE, Camera.CAMERA_NAME_AR_DRONE),
                    cameraPreviewForm.panel1,
                    320,
                    240);
            //camera.QuadBottomY = 33;
            CameraStarted = true;
            qrCodeDetection = new QrCodeDetection(camera);


        }

        private void _camera_QrDetection()
        {
            
            try
            {
                qrCode = qrCodeDetection.GetQRCodeText();
                if (qrCode != null)
                {
                    
                    freeParkingLots.Add(qrCode);
                    FoundFreeParkingLots = string.Join("\n", freeParkingLots);
                    if (qrCode.Equals("ParkingEnd") && freeParkingLots.Count > 0)
                    {
                        flyingRobot.Land();
                        var freeParkingLostsEnumerator = freeParkingLots.GetEnumerator();

                        if (freeParkingLostsEnumerator.MoveNext())
                        {
                            String firstParkingLot = freeParkingLostsEnumerator.Current;
                            System.IO.File.WriteAllText(@"C:\temp\parkinglot.txt", firstParkingLot.Replace("Parking", ""));

                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            
            
        }

        public void ButtonStartParkingLotSearch(object sender, RoutedEventArgs e)
        {
            Log.Info("Start parking lot search");

            freeParkingLots.Clear();
            FoundFreeParkingLots = "";

            //flyingRobot.TakeOff();

        }

        public void ButtonStopCamera(object sender, RoutedEventArgs e)
        {
            CameraStarted = false;

            cameraPreviewForm.Hide();
            camera.StopCamera();   
        }

        /// <summary>
        /// Disconnect from AR.Drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonDisconnect(object sender, RoutedEventArgs e)
        {
            Log.Info("Disconnect");
            flyingRobot.Disconnect();
            ConnectedToFlyingRobot = false;
            BatteryLevel = 0;
        }

        /// <summary>
        /// Play LED animation on AR.Drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonBlinkLeds(object sender, RoutedEventArgs e)
        {
            Log.Info("PlayLedAnimation");
            flyingRobot.PlayLedAnimation();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonShowActualConfigOfArDrone(object sender, RoutedEventArgs e)
        {
            Log.Info("GetControlConfig");

            var controlConfig = flyingRobotConfigurationHandler.GetControlConfig();
            
            Log.Info(controlConfig);
            var arDroneConfigProvider = new ControlConfigBasedArDroneConfigProvider(controlConfig);
            ArDroneConfig = arDroneConfigProvider.GetArDroneConfig();
            windowManager.ShowWindow(new ControlConfigViewModel(controlConfig));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonSendDefaultValues(object sender, RoutedEventArgs e)
        {
            Log.Info("SendDefaultValues");
            flyingRobotConfigurationHandler.SendDefaultValues();
        }

        /// <summary>
        /// Refresh the joystick list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonRefreshJoysticks(object sender, RoutedEventArgs e)
        {
            RefreshJoysticks();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonShowHuePicker(object sender, RoutedEventArgs e)
        {
            huePickerForm = new HuePickerForm(eventAggregator, new HuePickerMessage(HuePickerMessageType.Person,  HueMin, HueMax));
            huePickerForm.Show();
            HuePickerIsVisible = true;
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonHideHuePicker(object sender, RoutedEventArgs e)
        {
            huePickerForm.Hide();
            HuePickerIsVisible = false;
        }

        public void DangerButtonShowHuePicker(object sender, RoutedEventArgs e)
        {
            dangerHuePickerForm = new HuePickerForm(eventAggregator, new HuePickerMessage(HuePickerMessageType.Danger, DangerHueMin, DangerHueMax));
            dangerHuePickerForm.Show();
            DangerHuePickerIsVisible = true;
        }

        public void DangerButtonHideHuePicker(object sender, RoutedEventArgs e)
        {
            dangerHuePickerForm.Hide();
            DangerHuePickerIsVisible = false;
        }

        public void ButtonSaveTrackingConfig(object sender, RoutedEventArgs e)
        {
            fileBasedTrackingConfigProvider.StoreTrackingConfig(new TrackingConfig
            {
                Date = DateTime.Now,
                HueMax = HueMax,
                HueMin = HueMin,
                SearchObjectSizePixels = SearchObjectSizePixels,
                LuminanceMax = LuminanceMax,
                LuminanceMin = LuminanceMin,
                SaturationMax = SaturationMax,
                SaturationMin = SaturationMin,
                DangerHueMax = DangerHueMax,
                DangerHueMin = DangerHueMin,
                DangerLuminanceMax = DangerLuminanceMax,
                DangerLuminanceMin = DangerLuminanceMin,
                DangerSaturationMax = DangerSaturationMax,
                DangerSaturationMin = DangerSaturationMin,
                DangerSearchObjectSizePixels = DangerSearchObjectSizePixels
            });
        }
            

        public void ButtonStartAutonomousFlight(object sender, RoutedEventArgs e)
        {
            Log.Info(" ++++++++ START AUTONOMOUS FLIGHT +++++++++++++");
            FlyingAtonomous = true;
            autonomousTimer = new Timer
            {
                Interval = 400,
                Enabled = true
            };
            autonomousTimer.Elapsed += AutonomousTimerOnElapsed;
        }

        private void AutonomousTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (Button6Pressed)
            {
                var moveFlyingRobot = false;
                if (SearchObjectCenterLeft || SearchObjectBottomLeft || SearchObjectTopLeft)
                {
                    // steer left
                    Log.Warn("######### AUTONOM ######## Roll LEFT");
                    moveFlyingRobot = true;
                    flyingRobot.Roll(HorizontalDirection.Left, autonomousFlyingSteeringForce);
                }
                else if (SearchObjectCenterRight || SearchObjectTopRight || SearchObjectBottomRight)
                {
                    // steer right
                    Log.Warn("######### AUTONOM ######## Roll RIGHT");
                    moveFlyingRobot = true;
                    flyingRobot.Roll(HorizontalDirection.Right, autonomousFlyingSteeringForce);
                    //flyingRobot.Yaw(HorizontalDirection.Right, autonomousFlyingSteeringForce);
                }
                else if(SearchObjectLocationUnknown)
                {
                    
                }

                if(moveFlyingRobot == false)
                {
                    Log.Info("Hover");
                    flyingRobot.Hover();
                }
            }
        }

        public void ButtonStopAutonomousFlight(object sender, RoutedEventArgs e)
        {
            Log.Info(" ++++++++ STOP AUTONOMOUS FLIGHT +++++++++++++");
            if (autonomousTimer != null) autonomousTimer.Elapsed -= AutonomousTimerOnElapsed;
            FlyingAtonomous = false;
        }

        #endregion

     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnArDroneStatusTimedEvent(object source, ElapsedEventArgs e)
        {
            BatteryLevel = flyingRobot.GetBatteryLevel();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshJoysticks()
        {
            if (joystick != null)
            {
                joystick.Dispose();
                joystick = null;
            }

            AvailableJoystickDevices.Clear();
            AvailableJoystickDevices = new List<JoystickDevice>();
            var availableDevices = Joystick.GetAvailableDevices();
            if (availableDevices != null)
            {
                foreach (var availableDevice in availableDevices)
                {
                    AvailableJoystickDevices.Add(availableDevice);
                }
            }
        }

        /// <summary>
        /// Activate the selected joystick for EZB.
        /// Define method _joystick_OnControllerAction to be called on a controller action.
        /// </summary>
        private void ActivateJoystick()
        {
            JoystickDevice joystickDevice = SelectedJoystickDevice;
            joystick = new Joystick(joystickDevice, UcezbConnectProvider.Instance.EZB)
            {
                EventWatcherResolution = 100
            };
            joystick.OnControllerAction += _joystick_OnControllerAction;
            joystick.StartEventWatcher();
        }

        /// <summary>
        /// Called on a controller action.
        /// Check every button if pressed and every stick if moved and execute the defined methods.
        /// </summary>
        private void _joystick_OnControllerAction()
        {
            if (joystick.ButtonPressed(7) && joystick.ButtonPressed(6))
            {
                Log.Info("joystick.ButtonPressed(7) && (6)");
            }
            // Button 8 -> Takeoff (deadman switch)
            if (joystick.ButtonPressed(7) && !Button8Pressed)
            {
                Log.Info("joystick.ButtonPressed(7) && !Button8Pressed");
                flyingRobot.TakeOff();
                Button8Pressed = true;
            }
            else if (joystick.ButtonPressed(7) == false)
            {
                Log.Info("joystick.ButtonPressed(7) == false");

                flyingRobot.Land();
                Button8Pressed = false;
            }

            // Button 6 -> Autonomous flight
            if (joystick.ButtonPressed(5) && !Button6Pressed)
            {
                Log.Info("joystick.ButtonPressed(5) && !Button6Pressed");
                ButtonStartAutonomousFlight(null, null);
                Button6Pressed = true;
            }
            if (!joystick.ButtonPressed(5))
            {
                Log.Info("!joystick.ButtonPressed(5)");
                Button6Pressed = false;
                if (FlyingAtonomous)
                {
                    ButtonStopAutonomousFlight(null, null);
                }
            }
            
            // Button 7 -> Emergency
            if (joystick.ButtonPressed(6))
            {
                flyingRobot.Emergency();
                Log.Info("Emergency");
                Button7Pressed = true;
            }
            else
            {
                Button7Pressed = false;
            }

            // Button 1 -> blink LEDs
            if (joystick.ButtonPressed(0))
            {
                Button1Pressed = true;
                flyingRobot.PlayLedAnimation();
            }
            else
            {
                Button1Pressed = false;
            }

            // Button 2
            //  Should be called before take-off (start engines). Must be called on a flat surface. This flattens the trim values for the surface. 
            if (joystick.ButtonPressed(1))
            {
                Button2Pressed = true;
                flyingRobot.SetFlatTrim();
            }
            else
            {
                Button2Pressed = false;
            }

            Button3Pressed = joystick.ButtonPressed(2);

            Button4Pressed = joystick.ButtonPressed(3);

            Button5Pressed = joystick.ButtonPressed(4);

            
            var moveFlyingRobot = false;
            
            // left stick, X axis -> yaw
            LeftStickXAxis = joystick.GetAxisX;
            if (joystick.GetAxisX > 0.3)
            {
                flyingRobot.Yaw(HorizontalDirection.Right, joystick.GetAxisY);
                moveFlyingRobot = true;
            }
            else if (joystick.GetAxisX < -0.3)
            {
                flyingRobot.Yaw(HorizontalDirection.Left, joystick.GetAxisY);
                moveFlyingRobot = true;
            }
            
            // left stick, Y axis -> pitch
            LeftStickYAxis = joystick.GetAxisY;
            if (joystick.GetAxisY > 0.3)
            {
                flyingRobot.Pitch(VerticalDirection.Down, joystick.GetAxisY);
                moveFlyingRobot = true;
            }
            else if (joystick.GetAxisY < - 0.3)
            {
                flyingRobot.Pitch(VerticalDirection.Up, joystick.GetAxisY);
                moveFlyingRobot = true;
            }
            
            // right stick, X axis-> roll
            RightStickXAxis = joystick.GetAxisZ;
            // to the right
            if (joystick.GetAxisZ > 0.3)
            {
                flyingRobot.Roll(HorizontalDirection.Right, joystick.GetAxisZ);
                moveFlyingRobot = true;
            }
            // to the left
            else if (joystick.GetAxisZ < -0.3)
            {
                flyingRobot.Roll(HorizontalDirection.Left, joystick.GetAxisZ);
                moveFlyingRobot = true;
            }

            RightStickYAxis = joystick.GetAxisRz;
            if (joystick.GetAxisRz > 0.3)
            {
                flyingRobot.Nick(VerticalDirection.Down, joystick.GetAxisRz);
                moveFlyingRobot = true;
            }
            else if (joystick.GetAxisRz < -0.3)
            {
                flyingRobot.Nick(VerticalDirection.Up, joystick.GetAxisRz);
                moveFlyingRobot = true;
            }
            
            if (moveFlyingRobot == false)
            {
                flyingRobot.Hover();
            }
        }
        

        /// <summary>
        /// New camera frame available, so we look again for markers
        /// </summary>
        void _camera_OnNewFrame()
        {
            var targetLocationByColor = TargetLocation.Unknown;
            var targetLocationByGlyph = TargetLocation.Unknown;

            var dangerLocationByColor = TargetLocation.Unknown;

            if (targetLocator != null)
            {
                if (TrackingPreviewEnabled)
                {
                    try
                    {
                        targetLocationByColor = targetLocator.GetTargetLocation(TrackingPreviewEnabled,
                                            SearchObjectSizePixels,
                                            HueMin,
                                            HueMax,
                                            SaturationMin,
                                            SaturationMax,
                                            LuminanceMin,
                                            LuminanceMax);
                        LastKnownTargetLocationByColor = targetLocationByColor;
                        eventAggregator.Publish(new PersonLocationMessage(targetLocationByColor), action =>
                        {
                            Task.Factory.StartNew(action);

                        });
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception);
                    }
                }

                if (DangerTrackingPreviewEnabled)
                {
                    try
                    {
                        dangerLocationByColor = targetLocator.GetTargetLocation(DangerTrackingPreviewEnabled,
                                            DangerSearchObjectSizePixels,
                                            DangerHueMin,
                                            DangerHueMax,
                                            DangerSaturationMin,
                                            DangerSaturationMax,
                                            DangerLuminanceMin,
                                            DangerLuminanceMax);

                        eventAggregator.Publish(new DangerLocationMessage(dangerLocationByColor), action =>
                        {
                            Task.Factory.StartNew(action);

                        });
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception);
                    }

                }

                Log.Info("Locations: Person {0} - Danger {1}", targetLocationByColor, dangerLocationByColor);

                //try
                //{
                //    targetLocationByGlyph = targetLocator.GetGlyphLocation();
                //    eventAggregator.Publish(new DangerLocationMessage(targetLocationByGlyph), action =>
                //    {
                //        Task.Factory.StartNew(action);

                //    });
                //}
                //catch (Exception exception)
                //{
                //    Log.Error(exception);
                //}


                
            }

            camera.UpdatePreview(255);

            SearchObjectBottomLeft = false;
            SearchObjectBottomCenter = false;
            SearchObjectBottomRight = false;
            SearchObjectCenterLeft = false;
            SearchObjectCenterCenter = false;
            SearchObjectCenterRight = false;
            SearchObjectTopLeft = false;
            SearchObjectTopCenter = false;
            SearchObjectTopRight = false;
            SearchObjectLocationUnknown = false;


            switch (targetLocationByColor)
            {
                case TargetLocation.BottomLeft:
                    SearchObjectBottomLeft = true;
                    break;
                case TargetLocation.BottomCenter:
                    SearchObjectBottomCenter = true;
                    break;
                case TargetLocation.BottomRight:
                    SearchObjectBottomRight = true;
                    break;
                case TargetLocation.CenterLeft:
                    SearchObjectCenterLeft = true;
                    break;
                case TargetLocation.CenterCenter:
                    SearchObjectCenterCenter = true;
                    break;
                case TargetLocation.CenterRight:
                    SearchObjectCenterRight = true;
                    break;
                case TargetLocation.TopLeft:
                    SearchObjectTopLeft = true;
                    break;
                case TargetLocation.TopCenter:
                    SearchObjectTopCenter = true;
                    break;
                case TargetLocation.TopRight:
                    SearchObjectTopRight = true;
                    break;
                case TargetLocation.Unknown:
                    SearchObjectLocationUnknown = true;
                    break;
            }


            //if(targetLocationByGlyph == TargetLocation.TopCenter 
            //    || targetLocationByGlyph == TargetLocation.CenterCenter)
            //{

            //    Log.Info("ATTENTION!!!!! Target ahead!!!");
            //    GlyphAheadDetected = true;
            //}
            //else
            //{
            //    GlyphAheadDetected = false;
            //}

            
        }

        #region handle messages from eventAggregator
        public void Handle(HuePickerMessage message)
        {
            if (message.Type == HuePickerMessageType.Person)
            {
                HueMax = message.HueMax;
                HueMin = message.HueMin;
            }
            else
            {
                DangerHueMax = message.HueMax;
                DangerHueMin = message.HueMin;
            }
        }

        public void Handle(StopMessage message)
        {
            RemoteControlRequestsStop = true;
        }

        #endregion

     
    }
}