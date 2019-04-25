using System;
using Checkout.CrossCutting.Framework.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tt = typeof(TT);
            TT x  = (TT) Activator.CreateInstance(typeof(UnitTestProject1.TT));
            StringResources.ResourceManager.GetString("LogFileName");
        }

        
        
    }

    public class TT { }
}
