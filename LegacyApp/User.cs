using System;

namespace LegacyApp
{
    public class User
    {
        public User(Client client, DateTime dateOfBirth, string email, string firName, string surname)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = email;
            FirstName = firName;
            Surname = surname;
        }

        public Client Client { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public int CreditLimit { get; set; }
        public bool HasCreditLimit { get; set; }
    }
}