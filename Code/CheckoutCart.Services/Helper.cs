using System;
using System.IO;
using Newtonsoft.Json;

namespace CheckoutCart.Services
{
    public static class Helper
    {
        /// <summary>
        /// Return a safe type for a json file content
        /// </summary>
        /// <typeparam name="T">Type that represent the json file</typeparam>
        /// <param name="filePath">json file path</param>
        /// <returns></returns>
        public static T DeserializeJsonFile<T>(string filePath)
        {
            
            var jsonString = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath,filePath));
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Create next step instance for the processor chain
        /// </summary>
        /// <typeparam name="T">Type of the instantiated instance</typeparam>
        /// <param name="typeName">The full qualified type that will be instantiated</param>
        /// <returns></returns>
        public static T GetNextStep<T>(string typeName) where T:class
        {
            if (string.IsNullOrEmpty(typeName))
                return default(T);
            
            var type = Type.GetType(typeName);
            if (type != null)
                return (T) Activator.CreateInstance(type);

            return default(T);
        }
    }
}
