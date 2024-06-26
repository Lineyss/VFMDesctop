using Newtonsoft.Json;

namespace VFMDesctop.Models.Services
{
    internal class JsonService
    {
        public string Serialize<T>(T serializeObject)
        {
            return JsonConvert.SerializeObject(serializeObject);
        }

        public T Deserialize<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
