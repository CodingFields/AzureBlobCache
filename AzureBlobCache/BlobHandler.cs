using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlobCache
{
    public static class BlobHandler
    {
        public static CloudBlobClient BlobClient = null;

        public static string AccountConnectionString
        {
            set
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(value);
                BlobClient = storageAccount.CreateCloudBlobClient();
            }
        }

        public static bool BlobExists(string container, string path)
        {
            if(BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(path);

            return blob.Exists();
        }

        public static MemoryStream Download(string container, string path)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(path);
            var stream = new MemoryStream();
            blob.DownloadToStream(stream);

            return stream;
        }

        public static string DownloadText(string container, string path)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(path);

            if (!blob.Exists())
                return null;

            return blob.DownloadText();
        }

        public static bool UploadText(string container, string path, string value)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(path);

            blob.UploadText(value);

            return blob.Exists();
        }

        public static List<string> ListBlobsInFolder(string container, string path)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            return blobContainer.ListBlobs(path).Select(b => b.Uri.ToString()).ToList();
        }

        public static bool DeleteBlob(string container, string file)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(file);

            return blob.DeleteIfExists();
        }

        public static void UploadBlob(string fileName, Stream fileStream, string container)
        {
            if (BlobClient == null)
                throw new TypeInitializationException("AzureBlobCache.BlobHandler", new Exception("AccountConnectionString must be set before any methods can be called."));

            var blobContainer = BlobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();

            var blob = blobContainer.GetBlockBlobReference(fileName);

            blob.DeleteIfExists();

            fileStream.Seek(0, SeekOrigin.Begin);

            blob.UploadFromStream(fileStream);

            if (!blob.Exists())
            {
                throw new FileNotFoundException(string.Format("Blob was uploaded to {0} but couldn't be found after upload. URI: {1}", fileName, blob.Uri));
            }
        }
    }
}
