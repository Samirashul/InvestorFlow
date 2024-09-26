using InvestorFlow.Models;
using InvestorFlow.SqlOperations;
using InvestorFlow.SqlOperators;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using InvestorFlow.Interfaces;

namespace InvestorFlow.SqlOperations
{
    public class ContactOperator : BaseOperator, IContactOperator
    {
        public bool DoesNameExist(string Name)
        {
            if(CheckIfEntryExists("Contacts", "Name", Name))
                return true;
            return false;
        }

        public bool IsContactAttachedToFund(string Name)
        {
            if (CheckIfEntryExists("FundsContacts", "ContactName", Name))
                return true;
            return false;
        }

        public bool IsContactAttachedToFund(string Name, int FundId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FundId", $"{FundId}");
            parameters.Add("ContactName", Name);
            if (CheckIfEntryExists("FundsContacts", "ContactName", Name))
                return true;
            return false;
        }

        public string ReadContact(string Name)
        {
            if (!DoesNameExist(Name))
                return "Contact does not exist";
            string email = ReadField("Contacts", "Email", "Name", Name);
            string phoneNumber = ReadField("Contacts", "PhoneNumber", "Name", Name);
            return $"Name: {Name}, Email: {email}, Phone Number: {phoneNumber}";
        }

        public bool UpdateContact(string OldName, string NewName = "", string Email = "", string PhoneNumber = "")
        {
            if (NewName.Equals(""))
                NewName = OldName;
            if (Email.Equals(""))
                Email = ReadField("Contacts", "Email", "Name", OldName);
            if (PhoneNumber.Equals(""))
                PhoneNumber = ReadField("Contacts", "PhoneNumber", "Name", OldName);

            bool response = true;
            response = response && UpdatePhoneNumber(OldName, PhoneNumber);
            response = response && UpdateEmail(OldName, Email);
            response = response && UpdateContactName(OldName, NewName);

            return response;
        }

        public bool UpdateContactName(string Name, string NewValue)
        {
            if ((DoesNameExist(NewValue) && !Name.Equals(NewValue)) || !DoesNameExist(Name))
                return false;
            return UpdateField("Contacts", "Name", NewValue, "Name", Name);
        }

        public bool UpdatePhoneNumber(string Name, string NewValue)
        {
            return UpdateField("Contacts", "PhoneNumber", NewValue, "Name", Name);
        }

        public bool UpdateEmail(string Name, string NewValue)
        {
            return UpdateField("Contacts", "Email", NewValue, "Name", Name);
        }

        public bool DeleteContact(string Name)
        {
            return DeleteEntry("Contacts", "Name", Name);
        }

        public bool AssignContact(string Name, int FundId)
        {
            if(IsContactAttachedToFund(Name, FundId))
                return false;

            InsertEntry("FundsContacts", FundId, Name);
            return true;
        }

        public bool UnassignContact(string Name, int FundId)
        {
            Dictionary <string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FundId", $"{FundId}");
            parameters.Add("ContactName", Name);

            if (!CheckIfEntryExists("FundsContacts", parameters))
                return false;

            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand _cmd = new SqlCommand($"DELETE FROM FundsContacts Where FundId = '{FundId}' AND ContactName = '{Name}'", _con))
                {
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return true;
        }

        public List<string> ListContactsForFund(int FundId)
        {
            List<string> contacts = new List<string>();
            
            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                string queryStatement = $"SELECT ContactName FROM FundsContacts WHERE FundId={FundId}";

                using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                {
                    DataTable tempTable = new DataTable("TempTable");

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(tempTable);
                    _con.Close();

                    foreach (DataRow row in tempTable.Rows)
                    {
                        contacts.Add(row["ContactName"].ToString());
                    }
                }
            }

            return contacts;
        }
    }
}
