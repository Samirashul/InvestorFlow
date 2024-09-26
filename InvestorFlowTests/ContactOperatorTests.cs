using InvestorFlow.SqlOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestorFlowTests
{
    [TestClass]
    public class ContactOperatorTests
    {
        [TestMethod]
        public void DoesNameExistTest()
        { 
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(contactOperator.DoesNameExist("John Test"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void DoesNameExistFalse()
        {
            ContactOperator contactOperator = new ContactOperator();
            Assert.IsFalse(contactOperator.DoesNameExist(""));
        }

        [TestMethod]
        public void IsContactAttachedToFundNameTestFalse()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsFalse(contactOperator.IsContactAttachedToFund("John Test"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void IsContactAttachedToFundNameTestTrue()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            contactOperator.AssignContact("John Test", 1);
            Assert.IsTrue(contactOperator.IsContactAttachedToFund("John Test"));
            contactOperator.UnassignContact("John Test", 1);
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void IsContactAttachedToFundNameTestIdFalse()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsFalse(contactOperator.IsContactAttachedToFund("John Test", 1));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void IsContactAttachedToFundNameTestIdTrue()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            contactOperator.AssignContact("John Test", 1);
            Assert.IsTrue(contactOperator.IsContactAttachedToFund("John Test", 1));
            contactOperator.UnassignContact("John Test", 1);
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void ReadContactTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: , Phone Number: "));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void UpdateContactNameTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(contactOperator.UpdateContactName("John Test", "Johnathan Test"));
            Assert.IsTrue(contactOperator.ReadContact("Johnathan Test").Equals("Name: Johnathan Test, Email: , Phone Number: "));
            contactOperator.DeleteContact("Johnathan Test");
        }

        [TestMethod]
        public void UpdateContactEmailTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(contactOperator.UpdateEmail("John Test", "john@example.com"));
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: john@example.com, Phone Number: "));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void UpdateContactPhoneNumberTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "", ""]));
            Assert.IsTrue(contactOperator.UpdatePhoneNumber("John Test", "555-5555"));
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: , Phone Number: 555-5555"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void UpdateContactNoNewData()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "john@example.com", "555-5555"]));
            Assert.IsTrue(contactOperator.UpdateContact("John Test", "", "", ""));
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: john@example.com, Phone Number: 555-5555"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void DeleteContact()
        {
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.InsertEntry("Contacts", new List<string>(["John Test", "john@example.com", "555-5555"]));
            Assert.IsTrue(contactOperator.DeleteContact("John Test"));
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Contact does not exist"));
        }
    }
}
