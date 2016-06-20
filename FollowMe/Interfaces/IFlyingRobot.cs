using System.Security.Policy;
using FollowMe.Enums;

namespace FollowMe.Interfaces
{
    public interface IFlyingRobot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The actual config of the flying robot</returns>
        string Connect();

        /// <summary>
        /// Close the connection to the flying robot. 
        /// No more control possible. You should land before doing this.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Start motors and take off
        /// </summary>
        void TakeOff();

        /// <summary>
        /// Land softly
        /// </summary>
        void Land();

        /// <summary>
        /// Cut the motors, land (very) hard
        /// </summary>
        void Emergency();

        /// <summary>
        /// Hold the position
        /// </summary>
        void Hover();

        /// <summary>
        /// Turn around longitudinal axis
        /// </summary>
        /// <param name="horizontalDirection">Movement of the head of the flying robot</param>
        /// <param name="value">&gt; 0 - roll to the right</param>
        void Roll(HorizontalDirection horizontalDirection, float value);

        /// <summary>
        /// Turn around vertical axis
        /// </summary>
        /// <param name="horizontalDirection"></param>
        /// <param name="value"></param>
        void Yaw(HorizontalDirection horizontalDirection, float value);
        

        /// <summary>
        /// Turn around lateral axis
        /// </summary>
        /// <param name="verticalDirection">Movement of the head of the flying robot</param>
        /// <param name="value"></param>
        void Nick(VerticalDirection verticalDirection, float value);

        /// <summary>
        /// Go straight up or down.
        /// Gain or loose height.
        /// </summary>
        /// <param name="verticalDirection">Movement of the whole flying robot</param>
        /// <param name="value"></param>
        void Pitch(VerticalDirection verticalDirection, float value);

        /// <summary>
        /// Let blink the LEDs on the flying robot
        /// </summary>
        void PlayLedAnimation();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>batteryLevel</returns>
        int GetBatteryLevel();

        /// <summary>
        /// 
        /// </summary>
        void SetFlatTrim();

        void SwitchVideoChannelToVertical();

        
    }
}
