using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobCacheTests
{
    public class TestClass
    {
        public string Name { get; set; }

        //Something random yet vaugly complex 
        public Exception Exception { get; set; }

        public TestClass()
        {
            Exception = new Exception("Test");
            Name = "Test!";
        }
    }
}
