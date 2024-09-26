namespace InvestorFlow.Interfaces
{
    public interface IContactOperator
    {
        public bool DoesNameExist(string Name);

        public bool IsContactAttachedToFund(string Name);

        public bool IsContactAttachedToFund(string Name, int FundId);

        public string ReadContact(string Name);

        public bool UpdateContact(string OldName, string NewName = "", string Email = "", string PhoneNumber = "");

        public bool UpdateContactName(string Name, string NewValue);

        public bool UpdatePhoneNumber(string Name, string NewValue);

        public bool UpdateEmail(string Name, string NewValue);

        public bool DeleteContact(string Name);

        public bool AssignContact(string Name, int FundId);

        public bool UnassignContact(string Name, int FundId);

        public List<string> ListContactsForFund(int FundId);


    }
}
