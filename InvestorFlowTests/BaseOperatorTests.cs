using InvestorFlow.SqlOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestorFlowTests
{
    [TestClass]
    public class BaseOperatorTests
    {
        [TestMethod]
        public void ReadFieldTest()
        { 
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(baseOperator.ReadField("Contacts", "Name", "Name", "John Test").Equals("John Test"));
            baseOperator.DeleteEntry("Contacts", "Name", "John Test");
        }

        [TestMethod]
        public void UpdateFieldTest()
        {
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(baseOperator.UpdateField("Contacts", "Name", "Johnathan Test", "Name", "John Test"));
            Assert.IsTrue(baseOperator.ReadField("Contacts", "Name", "Name", "Johnathan Test").Equals("Johnathan Test"));
            baseOperator.DeleteEntry("Contacts", "Name", "Johnathan Test");
        }

        [TestMethod]
        public void DeleteEntryTest()
        { 
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(baseOperator.DeleteEntry("Contacts", "Name", "John Test"));
        }

        [TestMethod]
        public void InsertEntryTest()
        {
            BaseOperator baseOperator = new BaseOperator();
            Assert.IsTrue(baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""])));
            Assert.IsTrue(baseOperator.ReadField("Contacts", "Name", "Name", "John Test").Equals("John Test"));
            baseOperator.DeleteEntry("Contacts", "Name", "John Test");
        }

        [TestMethod]
        public void InsertEntryTestOverload()
        {
            BaseOperator baseOperator = new BaseOperator();
            Assert.IsTrue(baseOperator.InsertEntry("Funds", 2, "TestFund"));
            Assert.IsTrue(baseOperator.ReadField("Funds", "Name", "Name", "TestFund").Equals("TestFund"));
            baseOperator.DeleteEntry("Funds", "Name", "Test Fund");
        }

        [TestMethod]
        public void CheckIfEntryExistsTest()
        {
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(baseOperator.CheckIfEntryExists("Contacts", "Name", "John Test"));
            baseOperator.DeleteEntry("Contacts", "Name", "John Test");
        }

        [TestMethod]
        public void CheckIfEntryExistsTestDictionaryOverload()
        {
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Name", "John Test");
            Assert.IsTrue(baseOperator.CheckIfEntryExists("Contacts", dictionary));
            baseOperator.DeleteEntry("Contacts", "Name", "John Test");
        }

        [TestMethod]
        public void CheckIfEntryExistsTestDictionaryOverloadMultipleParams()
        {
            BaseOperator baseOperator = new BaseOperator();
            baseOperator.InsertEntry("Contacts", new List<string>(["John Test", "john@example.com", "555-5555"]));
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Name", "John Test");
            dictionary.Add("PhoneNumber", "555-5555");
            dictionary.Add("Email", "john@example.com");
            Assert.IsTrue(baseOperator.CheckIfEntryExists("Contacts", dictionary));
            baseOperator.DeleteEntry("Contacts", "Name", "John Test");
        }

    }
}
