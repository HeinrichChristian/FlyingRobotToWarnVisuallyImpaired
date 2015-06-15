using System;
using System.Text;
using System.Collections.Generic;
using FollowMe.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FollowMe.UnitTests.Configuration
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für XmlDeSerializerTestcs
    /// </summary>
    [TestClass]
    public class XmlDeSerializerTestcs
    {
        public XmlDeSerializerTestcs()
        {
            //
            // TODO: Konstruktorlogik hier hinzufügen
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestSerialize()
        { 
            // ARRANGE
            var trackingConfig = new TrackingConfig();
            trackingConfig.Date = DateTime.Parse("1977-08-12");
            trackingConfig.HueMax = 278;
            trackingConfig.HueMin = 189;
            trackingConfig.LuminanceMax = 0.6f;
            trackingConfig.LuminanceMin = 0.2f;
            trackingConfig.SaturationMax = 0.88f;
            trackingConfig.SaturationMin = 0.321f;

            // ACT
            var serializedTrackingConfig = XmlDeSerializer.Serialize(trackingConfig);

            // ASSERT
            // TODO!!
        }

        [TestMethod]
        public void TestDeserialize()
        {
            // ARRANGE
            var testString = @"<?xml version='1.0' encoding='utf-16'?><TrackingConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><Date>1977-08-12T00:00:00</Date><SearchObjectSizePixels>0</SearchObjectSizePixels><HueMin>189</HueMin><HueMax>278</HueMax><SaturationMin>0.321</SaturationMin><SaturationMax>0.88</SaturationMax><LuminanceMin>0.2</LuminanceMin><LuminanceMax>0.6</LuminanceMax></TrackingConfig>";

            // ACT
            var trackingConfig = XmlDeSerializer.Deserialize<TrackingConfig>(testString);

            // ASSERT
            Assert.IsTrue(trackingConfig.LuminanceMax == 0.6f);
        }
    }
}
