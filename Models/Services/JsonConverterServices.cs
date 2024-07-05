using Newtonsoft.Json;

namespace VFMDesctop.Models.Services
{
    internal class JsonConverterServices
    {
        public static T Deserialize<T> (string Json) => JsonConvert.DeserializeObject<T>(Json);
        public static string Serialize<T> (T Object) => JsonConvert.SerializeObject(Object);
    }
}
