using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Logix.Application.Extensions
{
    public static class SessionExtensions
    {
        public static void AddData<T>(this ISession session, string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
        }

        public static T GetData<T>(this ISession session, string key)
        {
            if (session != null && !string.IsNullOrEmpty(key))
            {
                string json = session.GetString(key);
                return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json); 
            }
            return default(T);
        }
    }
}
