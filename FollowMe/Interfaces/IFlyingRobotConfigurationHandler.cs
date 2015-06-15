namespace FollowMe.Interfaces
{
    public interface IFlyingRobotConfigurationHandler
    {
        void SetMaxYaw(float maxYaw);

        void SetMaxVerticalSpeed(int verticalSpeed);

        void SetMaxEulerAngle(float maxEulerAngle);

        void SetMaxAltitude(int maxAltitude);

        void SetIsOutside(bool isOutside);

        void SetIsFlyingWithoutShell(bool isFlyingWithoutShell);

        string GetControlConfig();

        void SendDefaultValues();
    }
}
