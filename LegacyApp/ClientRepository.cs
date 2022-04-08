using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class ClientRepository
    {
        private readonly string baseName = "appDatabase";
        private readonly string getClientCommandText = "uspGetClientById";
        private readonly string cliendId = "ClientId";
        private readonly string toCliendId = "@ClientId";
        private readonly string name = "Name";
        private readonly string clientStatus = "ClientStatus";

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
                    client = new Client
                    {
                        Id = int.Parse(reader[cliendId].ToString()),
                        Name = reader[name].ToString(),
                        ClientStatus = (ClientStatus)int.Parse(clientStatus)
                    };
                }
            }

            return client;
        }
    }
}