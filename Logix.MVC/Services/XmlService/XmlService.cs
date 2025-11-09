using System.Text;
using System.Xml.Serialization;

namespace Logix.MVC.Services.XmlService
{
    public class XmlService : IXmlService
    {
        public async Task<byte[]> Write<T>(IList<T> registers)
        {
            var serializer = new XmlSerializer(typeof(List<T>));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, registers);

                return memoryStream.ToArray();
            }
        }
    }
}
