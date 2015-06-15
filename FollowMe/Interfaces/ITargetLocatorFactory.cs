using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ_B;

namespace FollowMe.Interfaces
{
    public interface ITargetLocatorFactory
    {
        ITargetLocator CreateTargetLocator(Camera camera);
    }
}
