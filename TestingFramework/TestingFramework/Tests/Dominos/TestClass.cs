using NUnit.Framework;
using System;
using TestingFramework.Helpers;
using TestingFramework.Models;

namespace TestingFramework.Tests.Dominos
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void Test()
        {
           

            var testDi = new DominosCreatedAccountEntity[]
            {
                new DominosCreatedAccountEntity
                {
                    Email = "tetstst",
                FirstName = "Vladyslav",
                LastName = "Petrov",
                Sex = "Чоловік"
                },
                new DominosCreatedAccountEntity
                {
                    Email = "test",
                    FirstName = "test1",
                    LastName = "test2"
                }
            };

            ExcelFileHelpers.WriteDominosAccountsDataToExcel(@"F:\vladtex1t.xlsx", testDi);


            DominosCreatedAccountEntity accountData = new DominosCreatedAccountEntity
            {
                Email = "tetstst",
                FirstName = "Vladyslav",
                LastName = "Petrov",
                Sex = "Чоловік"
            };

            var numberOfProperties = accountData.GetType().GetProperties();

        }
    }
}
