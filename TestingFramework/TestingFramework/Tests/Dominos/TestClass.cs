using NUnit.Framework;
using System;

namespace TestingFramework.Tests.Dominos
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void Test()
        {
            DateTime datetime = DateTime.Now.AddDays(8);
            var shortformat = datetime.ToShortDateString();
            var shortformat1 = datetime.ToString();

        }
    }
}
