namespace NKZSoft.Template.Common.Tests
{
    using System.Net.Http;
    using System.Text;
    using Newtonsoft.Json;

    public class JsonContent<T> : StringContent
        where T : class
    {
        private static JsonSerializerSettings Settings { get; } = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public JsonContent(T obj)
            : base(JsonConvert.SerializeObject(obj, Settings), Encoding.UTF8, "application/json")
        {
        }
    }
}
