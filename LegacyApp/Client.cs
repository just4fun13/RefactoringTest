namespace LegacyApp
{
    public class Client
    {
        private readonly string vipString = "VeryImportantClient";
        private readonly string ipString = "ImportantClient";

        public bool IsVip { get; private set; }
        public bool IsIp { get; private set; }
        public Client(int id, string name, ClientStatus clientStatus)
        {
            Id = id;
            Name = name;
            ClientStatus = clientStatus;
            IsVip = (Name == vipString);
            IsIp = (Name == ipString);
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public ClientStatus ClientStatus { get; private set; }
    }
}