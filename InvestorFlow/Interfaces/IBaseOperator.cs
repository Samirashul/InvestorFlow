namespace InvestorFlow.Interfaces
{
    public interface IBaseOperator
    {
        public string GetConnectionString();

        public string ReadField(string table, string column, string idColumn, string id);

        public bool UpdateField(string table, string column, string newValue, string idColumn, string id);

        public bool DeleteEntry(string table, string column, string key);

        public bool InsertEntry(string table, List<string> values);

        public bool InsertEntry(string table, int id, string value);

        public bool CheckIfEntryExists(string table, string column, string id);

        public bool CheckIfEntryExists(string table, Dictionary<string, string> parameters);


    }
}
