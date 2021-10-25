using System;

namespace LegacyApp.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }

        static void ProveAddUser(string[] args)
        {
            /*
             * НЕ НУЖНО МЕНЯТЬ КОД В ЭТОМ ФАЙЛЕ
             */
            
            var userService = new UserService();
            var addResult = userService.AddUser("Владислав", "Бочкарев", "kemsikov@bk.ru", new DateTime(2003, 10, 1), 18);
            Console.WriteLine("Adding Владислав Бочкарев was " + (addResult ? "successful" : "unsuccessful"));
        }
    }
}