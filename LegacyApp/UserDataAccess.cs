
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class UserDataAccess
    {
        private const string firstNameParam = "@Firstname";
        private const string surnameParam = "@Surname";
        private const string dataOfBirthParam = "@DateOfBirth";
        private const string emailParam = "@EmailAddress";
        private const string hasCreditLimitParam = "@HasCreditLimit";
        private const string creditLimitValParam = "@CreditLimit";
        private const string clientIdParam = "@ClientId";
        private const string command = "uspAddUser";

        public static void AddUser(User user)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = UserDataAccess.command
                };

                var firstNameParameter = new SqlParameter(firstNameParam, SqlDbType.VarChar, 50) {Value = user.FirstName};
                command.Parameters.Add(firstNameParameter);
                var surnameameParameter = new SqlParameter(surnameParam, SqlDbType.VarChar, 50) {Value = user.Surname};
                command.Parameters.Add(surnameameParameter);
                var dateOfBirthParameter = new SqlParameter(dataOfBirthParam, SqlDbType.DateTime) {Value = user.DateOfBirth};
                command.Parameters.Add(dateOfBirthParameter);
                var emailAddressParameter = new SqlParameter(emailParam, SqlDbType.VarChar, 50) {Value = user.EmailAddress};
                command.Parameters.Add(emailAddressParameter);
                var hasCreditLimitParameter = new SqlParameter(hasCreditLimitParam, SqlDbType.Bit) {Value = user.HasCreditLimit};
                command.Parameters.Add(hasCreditLimitParameter);
                var creditLimitParameter = new SqlParameter(creditLimitValParam, SqlDbType.Int) {Value = user.CreditLimit};
                command.Parameters.Add(creditLimitParameter);
                var clientIdParameter = new SqlParameter(clientIdParam, SqlDbType.Int) {Value = user.Client.Id};
                command.Parameters.Add(clientIdParameter);

                command.ExecuteScalar();
            }
        }
    }
}