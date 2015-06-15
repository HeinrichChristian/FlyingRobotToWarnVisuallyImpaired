using System.IO;
using FollowMe.ArDrone;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FollowMe.UnitTests.ArDrone
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für ControlConfigBasedArDroneConfigProviderTest
    /// </summary>
    [TestClass]
    public class ControlConfigBasedArDroneConfigProviderTest
    {
        private readonly string controlConfig;
        public ControlConfigBasedArDroneConfigProviderTest()
        {
            controlConfig = File.ReadAllText("ArDrone/TestControlConfig.txt");
        }

        [TestMethod]
        public void TestConstructor()
        {
            var arDroneConfig = new ControlConfigBasedArDroneConfigProvider(controlConfig);

            Assert.IsNotNull(arDroneConfig);
        }

        [TestMethod]
        public void TestAltitudeMax()
        {
            var arDroneConfigProvider = new ControlConfigBasedArDroneConfigProvider(controlConfig);
            var arDroneConfig = arDroneConfigProvider.GetArDroneConfig();
            Assert.AreEqual(arDroneConfig.AltitudeMax, 2026);
        }
    }
}
