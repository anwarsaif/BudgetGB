using System.Text;
using System.Text.Json;

namespace Logix.MVC.Services.JsonService
{
    public class JsonService : IJsonService
    {
        public async Task<byte[]> Write<T>(IList<T> registers)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(registers));
        }
    }
}
