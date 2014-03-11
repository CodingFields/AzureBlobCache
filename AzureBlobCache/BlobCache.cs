using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace AzureBlobCache
{
    public static class BlobCache
    {

        public static string AccountConnectionString
        {
            set { BlobHandler.AccountConnectionString = value; }
        }

        public static Enviornments Enviornment { get; set; }

        private const string Salt = "akjhfdoiualknedf089723joiz0dsf97asdin90u1";

        public static bool Clean(string name, Caches cache)
        {
            try
            {
                return BlobHandler.DeleteBlob("sitecache",
                    String.Format("{0}/{1}.json", cache, (name + Salt + Enviornment).ObjectToHash()));
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }
            return false;
        }

        public static bool Update<T>(object value, string name, Caches cache) where T : class
        {
            try
            {
                var upload = JsonConvert.SerializeObject(value);
                return BlobHandler.UploadText("sitecache",
                    String.Format("{0}/{1}.json", cache, (name + Salt + Enviornment).ObjectToHash()), upload);
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }
            return false;
        }

        public static T Get<T>(string name, Caches cache) where T : class
        {
            try
            {
                var json = BlobHandler.DownloadText("sitecache",
                    String.Format("{0}/{1}.json", cache, (name + Salt + Enviornment).ObjectToHash()));

                if (String.IsNullOrEmpty(json))
                    return null;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }
            return null;
        }
    }
}
