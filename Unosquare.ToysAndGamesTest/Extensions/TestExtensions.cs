using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
