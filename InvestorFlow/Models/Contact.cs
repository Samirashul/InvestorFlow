namespace InvestorFlow.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Contact(string Name, string Email = "", string PhoneNumber = "")
        { 
            this.Name = Name;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
    }
}
