using FollowMe.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.WebService
{
    [ServiceContract]
    interface IRemoteControl
    {
        [OperationContract]
        void Start();

        [OperationContract]
        void Stop();

        [OperationContract]
        TargetLocation GetPersonLocation();

        [OperationContract]
        TargetLocation GetDangerLocation();


        [OperationContract]
        PersonAndDangerLocation GetPersonAndDangerLocation();
    }
}
