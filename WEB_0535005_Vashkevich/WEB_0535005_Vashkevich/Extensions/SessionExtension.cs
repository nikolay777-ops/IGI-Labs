using Newtonsoft.Json;

namespace WEB_0535005_Vashkevich.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        { 
            var serializedItem = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedItem); 
        }

        public static T? Get<T>(this ISession session, string key) 
        {
            var item = session.GetString(key);
            return item == null ? Activator.CreateInstance<T>() : JsonConvert.DeserializeObject<T>(item);
        }
    }
}
