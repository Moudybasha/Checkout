using System;
using System.IO;
using Newtonsoft.Json;

namespace CheckoutCart.Services
{
    public static class Helper
    {
        public static T DeserializeJsonFile<T>(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(filePath);
        }

        public static T GetNextStep<T>(string typeName) where T:class
        {
            var type = Type.GetType(typeName);
            if (type != null)
                return (T) Activator.CreateInstance(type);

            return default(T);
        }
    }
}
