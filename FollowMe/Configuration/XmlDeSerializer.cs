using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FollowMe.Configuration
{
    public class XmlDeSerializer
    {
        public static string Serialize<T>(T value)
        {
            string serializedXml;
            XmlSerializer xmlserializer = new XmlSerializer(typeof (T));
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);

                    serializedXml = stringWriter.ToString();

                    writer.Close();
                }
            }

            return serializedXml;
        }

        public static T Deserialize<T>(string xmlString) 
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
            using (TextReader xmlReader = new StringReader(xmlString))
            {
               return (T)xmlserializer.Deserialize(xmlReader);
            }
           
        }
    }
}
