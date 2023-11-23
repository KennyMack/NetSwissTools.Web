namespace Clients.API
{
    public static class MemoryStore
    {
        public static List<Client> Clients = new();
        public static List<Client> ClientsPending = new();
    }
}
