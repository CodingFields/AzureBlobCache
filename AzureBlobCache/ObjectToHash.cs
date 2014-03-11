using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AzureBlobCache
{
    public static class ObjectHasher
    {
        public static string ObjectToHash(this Object value)
        {
            return ComputeHash(ObjectToByteArray(value));
        }

        private static string ComputeHash(byte[] objectAsBytes)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                var sb = new StringBuilder();
                foreach (byte t in result)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private static byte[] ObjectToByteArray(Object objectToSerialize)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new SystemTypesResolver() };
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objectToSerialize, settings));
        }
    }

    public class SystemTypesResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p => p.PropertyType.Namespace.StartsWith("System") && !p.PropertyType.FullName.Contains("Stream")).ToList();
        }
    }
}
