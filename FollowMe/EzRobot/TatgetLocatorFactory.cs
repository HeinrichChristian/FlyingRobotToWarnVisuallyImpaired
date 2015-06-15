using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ_B;
using FollowMe.Interfaces;

namespace FollowMe.EzRobot
{
    public class TatgetLocatorFactory : ITargetLocatorFactory
    {
        public ITargetLocator CreateTargetLocator(Camera camera)
        {
            var targetLocator = new EzRobotCameraTargetLocator(camera);
            return targetLocator;
        }
    }
}
