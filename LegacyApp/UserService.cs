using System;

namespace LegacyApp
{
    public class UserService
    {
        private int creditLimit = 500;

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firName) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User(client, dateOfBirth, email, firName, surname);

            
            if (client.IsVip)
            {
                // Пропустить проверку лимита
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditServiceClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.FirstName, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (client.IsIp)
                user.CreditLimit *= 2;
   

            if (user.HasCreditLimit && user.CreditLimit < creditLimit)
            {
                return false;
            }

            UserDataAccess.AddUser(user);

            return true;
        }


    }
}