using InvestorFlow.Interfaces;
using InvestorFlow.SqlOperations;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace InvestorFlow.Services
{
    public class ContactService : IContactService
    {
        private ContactOperator Operator;

        public ContactService() 
        { 
            Operator = new ContactOperator();
        }

        public bool CreateContact(string Name, string Email = "", string PhoneNumber = "")
        {
            return (Name is not null && !Name.Equals("") && !Operator.DoesNameExist(Name)) ? Operator.InsertEntry("Contacts", new List<string>([Name, Email, PhoneNumber])) : false;
        }

        public string ReadContact(string Name)
        {
            return Operator.CheckIfEntryExists("Contacts", "Name", Name) ? Operator.ReadContact(Name) : "NO CONTACT FOUND";
        }

        public bool UpdateContact(string Name, string NewName  ="", string Email = "", string PhoneNumber = "")
        {
            return Operator.CheckIfEntryExists("Contacts", "Name", Name) ? Operator.UpdateContact(Name, NewName, Email, PhoneNumber) : false;
        }

        public bool DeleteContact(string Name)
        {
            return (Operator.DoesNameExist(Name) && !Operator.IsContactAttachedToFund(Name)) ? Operator.DeleteContact(Name) : false;
        }

        public bool AssignContact(string Name, int FundId)
        {
            if (Operator.IsContactAttachedToFund(Name, FundId))
                return true;
            if(Operator.CheckIfEntryExists("Funds", "Id", $"{FundId}") && Operator.CheckIfEntryExists("Contacts", "Name", Name))
                return Operator.AssignContact(Name, FundId);
            return false;
        }

        public bool UnassignContact(string Name, int FundId)
        {
            if (!Operator.IsContactAttachedToFund(Name, FundId))
                return true;
            if (Operator.CheckIfEntryExists("Funds", "Id", $"{FundId}") && Operator.CheckIfEntryExists("Contacts", "Name", Name))
                return Operator.UnassignContact(Name, FundId);
            return false;
        }

        public string ListContactsForFund(int FundId)
        {
            return string.Join("\n", Operator.ListContactsForFund(FundId));
        }
    }
}
