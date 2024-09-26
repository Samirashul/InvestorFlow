namespace InvestorFlow.Interfaces
{
    public interface IContactService
    {

        bool CreateContact(string Name, string Email = "", string PhoneNumber = "");

        string ReadContact(string Name);

        bool UpdateContact(string Name, string NewName = "", string Email = "", string PhoneNumber = "");

        bool DeleteContact(string Name);

        bool AssignContact(string Name, int FundId);

        bool UnassignContact(string Name, int FundId);

        string ListContactsForFund(int FundId);
    }
}
