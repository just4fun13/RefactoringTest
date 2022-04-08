using System;

namespace LegacyApp
{
    public class UserService
    {
        private int creditLimit = 500;
        private int requiredAge = 21;

        private bool NameIsNull(string name, string anotherName) => (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(anotherName));

        private bool MailIsInvalid(string mail) => (!mail.Contains("@") || !mail.Contains("."));

        private int GetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        public bool AddUser(string firName, string surname, string email, DateTime dateOfBirth, int clientId)
        {

            if (NameIsNull(firName, surname) || MailIsInvalid(email) || (GetAge(dateOfBirth) < requiredAge))
                return false;



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