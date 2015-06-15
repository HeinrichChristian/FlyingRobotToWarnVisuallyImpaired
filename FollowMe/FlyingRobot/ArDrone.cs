using System.Threading;
using Caliburn.Micro;
using EZ_B;
using EZ_B.ARDrone;
using FollowMe.Enums;
using FollowMe.EzRobot;
using FollowMe.Interfaces;

namespace FollowMe.FlyingRobot
{
    public class ArDrone : IFlyingRobot, IFlyingRobotConfigurationHandler
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(ArDrone));

        /// <summary>
        /// The class provided by EZ Robot to get an easy access to the AR.Drone
        /// </summary>
        private readonly UCEZB_Connect ezbConnect = UcezbConnectProvider.Instance;
        
        /// <summary>
        /// This value is send to the drone
        /// </summary>
        private const float MoveSensitivivivity = 0.20f;

        /// <summary>
        /// After sending a move command this amount of milliseconds the thread sleeps
        /// </summary>
        private const int MoveSleepTimeMilliseconds = 100;

        
        public string Connect()
        {
            Log.Info("Connect()");
            ezbConnect.EZB.ARDrone.Connect(ARDrone.ARDroneVersionEnum.V2);
            var controlConfig = ezbConnect.EZB.ARDrone.GetControlConfig();
            return controlConfig;
        }

        public void Disconnect()
        {
            Log.Info("Disconnect()");
            ezbConnect.EZB.ARDrone.Disconnect();
        }

        public void TakeOff()
        {
            Log.Info("Takeoff()");
            ezbConnect.EZB.ARDrone.TakeOff();
        }

        public void Land()
        {
            Log.Info("Land()");
            ezbConnect.EZB.ARDrone.Land();
        }

        public void Emergency()
        {
            Log.Info("Emergency()");
            ezbConnect.EZB.ARDrone.Emergency();
        }

        public void Hover()
        {
            Log.Info("Hover()");
            ezbConnect.EZB.ARDrone.Hover();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="horizontalDirection"></param>
        /// <param name="value"></param>
        public void Roll(HorizontalDirection horizontalDirection, float value)
        {

            if (horizontalDirection == HorizontalDirection.Left)
            {
                Log.Info("Roll Left: joystick.GetAxisZ {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'",
                    value,
                    MoveSensitivivivity, 0, 0, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(-MoveSensitivivivity, 0, 0, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
        
            else if (horizontalDirection == HorizontalDirection.Right)
            {
                Log.Info("Roll Right: joystick.GetAxisZ {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value,
                    -MoveSensitivivivity, 0, 0, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(MoveSensitivivivity, 0, 0, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
        }

        public void Yaw(HorizontalDirection horizontalDirection, float value)
        {
            if (horizontalDirection == HorizontalDirection.Right)
            {
                Log.Info("Yaw Right: joystick.GetAxisY {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, 0, 0,
                    MoveSensitivivivity);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, 0, 0, MoveSensitivivivity);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
            else if (horizontalDirection == HorizontalDirection.Left)
            {
                Log.Info("Yaw Left: joystick.GetAxisY {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, 0, 0,
                    -MoveSensitivivivity);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, 0, 0, -MoveSensitivivivity);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();    
            }
        }

        public void Nick(VerticalDirection verticalDirection, float value)
        {
            if (verticalDirection == VerticalDirection.Down)
            {
                Log.Info("Nick Down: joystick.GetAxisRz {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, MoveSensitivivivity, 0, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, MoveSensitivivivity, 0, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
            else if (verticalDirection == VerticalDirection.Up)
            {
                Log.Info("Nick Up: joystick.GetAxisRz {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, -MoveSensitivivivity, 0, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, -MoveSensitivivivity, 0, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();   
            }
        }

        public void Pitch(VerticalDirection verticalDirection, float value)
        {
            if (verticalDirection == VerticalDirection.Down)
            {
                Log.Info("Pitch Down: joystick.GetAxisY {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, 0, -MoveSensitivivivity, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, 0, -MoveSensitivivivity, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
            else if (verticalDirection == VerticalDirection.Up)
            {
                Log.Info("Pitch Up: joystick.GetAxisY {0} -> SetProgressiveInputValues '{1}', '{2}', '{3}', '{4}'", value, 0, 0, MoveSensitivivivity, 0);
                ezbConnect.EZB.ARDrone.SetProgressiveInputValues(0, 0, MoveSensitivivivity, 0);
                //Thread.Sleep(MoveSleepTimeMilliseconds);
                //ezbConnect.EZB.ARDrone.Hover();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayLedAnimation()
        {
            Log.Info("PlayLedAnimation()");
            ezbConnect.EZB.ARDrone.PlayLedAnimation(Commands.LedAnimationEnum.BlinkRed, 2, 10);
        }

        public int GetBatteryLevel()
        {
            Log.Info("GetBatteryLevel()");
            var batteryLevel = ezbConnect.EZB.ARDrone.CurrentNavigationData.BatteryLevel;
            Log.Info("BatteryLevel: {0}", batteryLevel);
            return batteryLevel;
        }

        public void SetFlatTrim()
        {
            Log.Info("SetFlatTrim()");
            ezbConnect.EZB.ARDrone.SetFlatTrim();
        }

        #region IFlyingRobotConfigurationHandler implementation
        public void SetMaxYaw(float maxYaw)
        {
            Log.Info("SetMaxYaw({0})", maxYaw);
            ezbConnect.EZB.ARDrone.SetYaw(maxYaw);
        }

        public void SetMaxVerticalSpeed(int verticalSpeed)
        {
            Log.Info("SetMaxVerticalSpeed({0})", verticalSpeed);
            ezbConnect.EZB.ARDrone.SetVZMax(verticalSpeed);
        }

        public void SetMaxEulerAngle(float maxEulerAngle)
        {
            Log.Info("SetMaxEulerAngle({0})", maxEulerAngle);
            ezbConnect.EZB.ARDrone.SetEulerAngleMax(maxEulerAngle);
        }

        public void SetMaxAltitude(int maxAltitude)
        {
            Log.Info("SetMaxAltitude({0})", maxAltitude);
            ezbConnect.EZB.ARDrone.SetAltitudeMax(maxAltitude);
        }

        public void SetIsOutside(bool isOutside)
        {
            Log.Info("SetIsOutside({0})", isOutside);
            ezbConnect.EZB.ARDrone.SetIsOutside(isOutside);
        }

        public void SetIsFlyingWithoutShell(bool isFlyingWithoutShell)
        {
            Log.Info("SetIsFlyingWithoutShell({0})", isFlyingWithoutShell);
            ezbConnect.EZB.ARDrone.SetIsFlyingWithoutShell(isFlyingWithoutShell);
        }

        public string GetControlConfig()
        {
            Log.Info("GetControlConfig()");
            var controlConfig = ezbConnect.EZB.ARDrone.GetControlConfig();
            Log.Info("controlConfig: {0}", controlConfig);
            return controlConfig;
        }

        public void SendDefaultValues()
        {
            Log.Info("SendDefaultValues()");
            ezbConnect.EZB.ARDrone.SendDefaultValues();
        }

        #endregion
    }
}
