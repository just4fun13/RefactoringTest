using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class ClientRepository
    {
        private const string baseName = "appDatabase";
        private const string getClientCommandText = "uspGetClientById";
        private const string cliendId = "ClientId";
        private const string toCliendId = "@ClientId";
        private const string name = "Name";
        private const string clientStatus = "ClientStatus";

        public Client GetById(int id)
        {
            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings[baseName].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = getClientCommandText
                };

                var parameter = new SqlParameter(toCliendId, SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);
                
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client(
                        int.Parse(reader[cliendId].ToString()), 
                        reader[name].ToString(), 
                        (ClientStatus)int.Parse(clientStatus));
                }
            }

            return client;
        }
    }
}