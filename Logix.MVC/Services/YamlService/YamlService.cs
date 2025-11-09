using System.Text;
using YamlDotNet.Serialization;

namespace Logix.MVC.Services.YamlService
{
    public class YamlService : IYamlService
    {
        public async Task<byte[]> Write<T>(IList<T> registers)
        {
            var serializer = new SerializerBuilder().Build();

            string yamlString = serializer.Serialize(registers);

            return Encoding.UTF8.GetBytes(yamlString);
        }
    }
}
