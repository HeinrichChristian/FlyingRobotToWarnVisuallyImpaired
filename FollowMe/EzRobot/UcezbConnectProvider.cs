using EZ_B;

namespace FollowMe.EzRobot
{
    public class UcezbConnectProvider
    {
        private static UCEZB_Connect instance;
        public static UCEZB_Connect Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UCEZB_Connect();
                }
                return instance;
            }
            

        }
    }
}
