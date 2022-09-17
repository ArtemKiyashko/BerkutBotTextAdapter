using System;
using Newtonsoft.Json;

namespace BerkutTextAdapter.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object update) => JsonConvert.SerializeObject(update);
    }
}

