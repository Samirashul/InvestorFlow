using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DbExtensions;
using InvestorFlow.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Identity.Client;

namespace InvestorFlow.SqlOperators
{
    public class BaseOperator : IBaseOperator
    {
        private string _connectionString = "server=localhost;database=InvestorFlow;integrated Security=SSPI;";

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public string ReadField(string table, string column, string idColumn, string id)
        {
            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                string queryStatement = $"SELECT TOP 1 {column} FROM {table} WHERE {idColumn}='{id}'";

                using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                {
                    DataTable tempTable = new DataTable("TempTable");

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(tempTable);
                    _con.Close();
                    if (tempTable.Rows.Count == 0)
                        return "NO RESULTS FOUND";

                    return tempTable.Rows[0][column].ToString();
                }
            }
        }

        public bool UpdateField(string table, string column, string newValue, string idColumn, string id)
        {
            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand _cmd = new SqlCommand($"UPDATE {table} SET {column} = '{newValue}' WHERE {idColumn} = '{id}'", _con))
                {
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return true;
        }

        public bool DeleteEntry(string table, string column, string key)
        {

            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand _cmd = new SqlCommand($"DELETE FROM {table} Where {column} = '{key}'", _con))
                {
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return true;
        }

        public bool InsertEntry(string table, List<string> values)
        {

            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand _cmd = new SqlCommand($"INSERT INTO {table} values ('{string.Join("', '", values)}');", _con))
                {
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return true;
        }

        public bool InsertEntry(string table, int id, string value)
        {
            using (SqlConnection _con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand _cmd = new SqlCommand($"INSERT INTO {table} values ({id}, '{value}');", _con))
                {
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return true;
        }

        public bool CheckIfEntryExists(string table, string column, string id)
        {

            using (SqlConnection _con = new SqlConnection(_connectionString))
            {
                string queryStatement = $"SELECT * FROM dbo.{table} WHERE {column} = '{id}'";

                using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                {
                    DataTable tempTable = new DataTable("TempTable");

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(tempTable);
                    _con.Close();

                    return tempTable.Rows.Count > 0 ? true : false;

                }
            }
        }

        public bool CheckIfEntryExists(string table, Dictionary<string, string> parameters)
        {
            using (SqlConnection _con = new SqlConnection(_connectionString))
            {
                var query = new SqlBuilder().SELECT("*")
                    .FROM(table);

                foreach (KeyValuePair<string, string> kvp in parameters)
                    query.WHERE($"{kvp.Key} = '{kvp.Value}'");

                using (SqlCommand _cmd = new SqlCommand(query.ToString(), _con))
                {
                    DataTable tempTable = new DataTable("TempTable");

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(tempTable);
                    _con.Close();

                    return tempTable.Rows.Count > 0 ? true : false;

                }
            }
        }
    }
}
