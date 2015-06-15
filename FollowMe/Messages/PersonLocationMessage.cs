using FollowMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.Messages
{
    public class PersonLocationMessage
    {
        private TargetLocation targetLocation;
        public PersonLocationMessage(TargetLocation personLocation)
        {
            targetLocation = personLocation;
        }

        public TargetLocation PersonLocation 
        { 
            get { return targetLocation; } 
        }
    }
}
