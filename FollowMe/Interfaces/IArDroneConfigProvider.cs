using FollowMe.ArDrone;
using FollowMe.Configuration;

namespace FollowMe.Interfaces
{
    public interface IArDroneConfigProvider
    {
        ArDroneConfig GetArDroneConfig();
    }
}