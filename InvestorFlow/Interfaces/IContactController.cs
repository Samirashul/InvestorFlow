namespace InvestorFlow.Interfaces
{
    public interface IContactController
    {
        public bool CreateContact(string Name, string Email = "", string PhoneNumber = "");

        public string ReadContact(string Name);

        public bool UpdateContact(string Name, string NewName = "", string Email = "", string PhoneNumber = "");

        public bool DeleteContact(string Name);

        public bool AssignContact(string Name, int FundId);

        public bool UnassignContact(string Name, int FundId);

        public string ListContacts(int FundId);
    }
}
