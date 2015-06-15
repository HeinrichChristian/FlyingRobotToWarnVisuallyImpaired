using System.IO;
using Caliburn.Micro;

namespace FollowMe.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class FileBasedTrackingConfigProvider
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(FileBasedTrackingConfigProvider));
        private const string FileName = "trackingConfig.xml";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackingConfig"></param>
        public void StoreTrackingConfig(TrackingConfig trackingConfig)
        {
            Log.Info("StoreTrackingConfig");

            var trackingConfigString = XmlDeSerializer.Serialize(trackingConfig);
            File.WriteAllText(FileName, trackingConfigString);
        }

        public TrackingConfig LoadTrackingConfig()
        {
            Log.Info("LoadTrackingConfig");

            if (File.Exists(FileName) == false)
            {
                Log.Warn("File {0} does not exist.", FileName);    
                return new TrackingConfig();
            }

            var trackingConfigString = File.ReadAllText(FileName);
            var trackingConfig = XmlDeSerializer.Deserialize<TrackingConfig>(trackingConfigString);

            return trackingConfig;
        }
    }
}
