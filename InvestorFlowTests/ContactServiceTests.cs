using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using InvestorFlow.Services;
using InvestorFlow.SqlOperations;

namespace InvestorFlowTests
{
    [TestClass]
    public class ContactServiceTests
    {
        [TestMethod]
        public void CreateContactTestPositiveName()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            Assert.IsTrue(contactOperator.DoesNameExist("John Test"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void CreateContactTestNegativeName()
        {
            ContactService service = new ContactService();
            Assert.IsFalse(service.CreateContact(""));
        }

        [TestMethod]
        public void CreateContactTestExistingName()
        {
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            Assert.IsFalse(service.CreateContact("John Test"));
            ContactOperator contactOperator = new ContactOperator();
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void CreateContactTestNameEmail()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test", "john@example.com");
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: john@example.com, Phone Number: "));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void CreateContactTestNamePhoneNumber()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test", "", "555-5555");
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: , Phone Number: 555-5555"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void CreateContactTestNamePhoneNumberEmail()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test", "john@email.com", "555-5555");
            Assert.IsTrue(contactOperator.ReadContact("John Test").Equals("Name: John Test, Email: john@email.com, Phone Number: 555-5555"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void ReadContactTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test", "john@email.com", "555-5555");
            Assert.IsTrue(service.ReadContact("John Test").Equals("Name: John Test, Email: john@email.com, Phone Number: 555-5555"));
            contactOperator.DeleteContact("John Test");
        }

        [TestMethod]
        public void UpdateContact()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test", "john@email.com", "555-5555");
            service.UpdateContact("John Test", "Johnathan Test", "newEmail@example.com", "555-5556");
            Assert.IsTrue(contactOperator.ReadContact("Johnathan Test").Equals("Name: Johnathan Test, Email: newEmail@example.com, Phone Number: 555-5556"));
            contactOperator.DeleteContact("Johnathan Test");
        }

        [TestMethod]
        public void UpdateContactNoName()
        {
            ContactService service = new ContactService();
            Assert.IsFalse(service.UpdateContact("", "Johnathan Test", "newEmail@example.com", "555-5556"));
        }

        [TestMethod]
        public void DeleteContactTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            Assert.IsTrue(service.DeleteContact("John Test"));
        }

        [TestMethod]
        public void DeleteContactTestNoName()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            Assert.IsFalse(service.DeleteContact(""));
        }

        [TestMethod]
        public void AssignContactTest()
        { 
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            Assert.IsTrue(service.AssignContact("John Test", 1));
            ContactOperator contactOperator = new ContactOperator();
            Assert.IsTrue(contactOperator.IsContactAttachedToFund("John Test", 1));
            Assert.IsTrue(service.UnassignContact("John Test", 1));
            service.DeleteContact("John Test");
        }

        [TestMethod]
        public void DeleteAssignedContactTest()
        {
            ContactOperator contactOperator = new ContactOperator();
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            service.AssignContact("John Test", 1);
            Assert.IsFalse(service.DeleteContact("John Test"));
            service.UnassignContact("John Test", 1);
            Assert.IsTrue(service.DeleteContact("John Test"));
        }

        [TestMethod]
        public void ListContactsForFundTest()
        {
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            service.AssignContact("John Test", 1);
            Assert.IsTrue(service.ListContactsForFund(1).Equals("John Test"));
            service.UnassignContact("John Test", 1);
            service.DeleteContact("John Test");
        }

        [TestMethod]
        public void ListContactsForFundTwoPeopleTest()
        {
            ContactService service = new ContactService();
            service.CreateContact("John Test");
            service.AssignContact("John Test", 1);
            service.CreateContact("Tim Test");
            service.AssignContact("Tim Test", 1);
            Assert.IsTrue(service.ListContactsForFund(1).Equals("John Test\nTim Test"));
            service.UnassignContact("John Test", 1);
            service.DeleteContact("John Test");
            service.UnassignContact("Tim Test", 1);
            service.DeleteContact("Tim Test");
        }

        [TestMethod]
        public void ListContactsForFundNoPeopleTest()
        {
            ContactService service = new ContactService();
            Assert.IsTrue(service.ListContactsForFund(1).Equals(""));
        }

    }
}
