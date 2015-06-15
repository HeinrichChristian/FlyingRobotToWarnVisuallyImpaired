using FollowMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.Messages
{
    public class DangerLocationMessage
    {
        private TargetLocation targetLocation;
        public DangerLocationMessage(TargetLocation dangerLocation)
        {
            this.targetLocation = dangerLocation;
        }

        public TargetLocation DangerLocation
        {
            get { return targetLocation; }
        }
    }
}
