using Newtonsoft.Json;

namespace Unosquare.ToysAndGamesTest.Extensions
{
    public static class TestExtensions
    {
        public static string AsJson<T>(this T data) => JsonConvert.SerializeObject(data, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        });

    }

}
