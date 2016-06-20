using EZ_B;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowMe.EzRobot
{
    class QrCodeDetection
    {

        private readonly Camera camera;
        private static readonly ILog Log = LogManager.GetLog(typeof(EzRobotCameraTargetLocator));

        public QrCodeDetection(Camera camera)
        {
            if (camera == null) throw new ArgumentNullException("camera");
            this.camera = camera;
        }

        public String GetQRCodeText()
        {
            
            ObjectLocation objectLocation = null;
            String qrCodeText = null;
            try
            {
                objectLocation = camera.CameraQRCodeDetection.GetObjectLocationByQRCode();


            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }

            camera.UpdatePreview(255);


            if (objectLocation != null && objectLocation.IsObjectFound)
            {

                qrCodeText = objectLocation.QRCodeText;
                Log.Info("Found qr code" + qrCodeText);

            }

            return qrCodeText;
        }
    }
}
