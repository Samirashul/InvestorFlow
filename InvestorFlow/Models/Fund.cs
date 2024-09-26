namespace InvestorFlow.Models
{
    public class Fund
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Fund(int id, string Name)
        { 
            this.Id = id;
            this.Name = Name;
        }
    }
}
