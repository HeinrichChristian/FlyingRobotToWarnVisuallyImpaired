using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

using System.Runtime.Serialization;
using FollowMe.Enums;

namespace FollowMe.WebService
{
    [DataContract]
    public class PersonAndDangerLocation
    {
        [DataMember]
        public TargetLocation PersonLocation { get; set; }


        [DataMember]
        public TargetLocation DangerLocation { get; set; }
    }
}
