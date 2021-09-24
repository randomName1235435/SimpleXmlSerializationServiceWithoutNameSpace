using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationServices
{
    public class SerializationServiceWithoutNameSpace
    {
        public void SaveToFile<TType>(TType serializeObject, string fullFileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(fullFileName, false, Encoding.UTF8);
                var ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // empty namespace
                new XmlSerializer(serializeObject.GetType()).Serialize(streamWriter, serializeObject, ns);
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null) streamWriter.Dispose();
            }
        }

        public TType LoadFromFile<TType>(string fullFileName)
        {
            var fileStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var serializer = (TType) new XmlSerializer(typeof(TType)).Deserialize(XmlReader.Create(streamReader));
            fileStream.Close(); // sofort wieder schließen, damit das dann weggeräumt werden kann
            return serializer;
        }
    }
}
