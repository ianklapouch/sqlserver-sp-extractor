using sqlserver_sp_extractor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace sqlserver_sp_extractor.Services
{

    public class DbService
    {
        readonly string connectionString;
        private SqlConnection connection;

        public DbService(Connection connection)
        {
            this.connectionString = $"Server={connection.ServerName};Database={connection.DataBase};User Id={connection.Login};Password={connection.Password};MultipleActiveResultSets=True";
        }

        public void OpenConnection()
        {
            connection ??= new SqlConnection(connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public string GetStoredProcedureText(string procedureName)
        {
            //OpenConnection();
            string query = "sp_helptext";
            SqlCommand command = new(query, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlParameter parameter = new("@objname", SqlDbType.NVarChar, 776)
            {
                Value = procedureName
            };
            command.Parameters.Add(parameter);
         
            try
            {
                using SqlDataReader reader = command.ExecuteReader();
                string procedureText = "";
                while (reader.Read())
                {
                    procedureText += reader["Text"].ToString();
                }
                return procedureText;
            }
            catch (SqlException)
            {
                return string.Empty;
            }
        }
    }
}
