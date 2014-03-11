using System;
using System.IO;
using AzureBlobCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureBlobCacheTests
{
    [TestClass]
    public class UnitTest
    {

        const string TestString = "This is a test string. There are many strings like this string but this string is mine.";

        [TestMethod]
        public void TestObjectToHash()
        {
            var obj = new TestClass();

            obj.ObjectToHash();
        }

        //[TestMethod]
        //public void TestBlobHandler()
        //{
        //    /*
        //     * Insert your blob storage connection string to run this test.
        //     */
        //    BlobHandler.AccountConnectionString = "";

        //    Assert.IsTrue(BlobHandler.UploadText("test", "testpath.txt", TestString));

        //    Assert.IsTrue(BlobHandler.BlobExists("test", "testpath.txt"));

        //    Assert.AreEqual(TestString, BlobHandler.DownloadText("test", "testpath.txt"));

        //    Assert.IsTrue(BlobHandler.DeleteBlob("test", "testpath.txt"));
        //}

        //[TestMethod]
        //public void TestBlobCache()
        //{
        //    /*
        //     * Insert your blob storage connection string to run this test.
        //     */

        //    var obj = new TestClass();

        //    BlobCache.AccountConnectionString = "";

        //    Assert.IsTrue(BlobCache.Update<TestClass>(obj, obj.ObjectToHash(), Caches.Misc));

        //    Assert.AreEqual(obj.ObjectToHash(), BlobCache.Get<TestClass>(obj.ObjectToHash(), Caches.Misc).ObjectToHash());

        //    Assert.IsTrue(BlobCache.Clean(obj.ObjectToHash(), Caches.Misc));
        //}
    }
}
