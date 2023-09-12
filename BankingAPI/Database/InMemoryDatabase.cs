namespace BankingAPI.Database
{
    public class InMemoryDatabase
    {
        public static Dictionary<int, decimal> Accounts = new Dictionary<int, decimal>();

        public static void Reset()
        {
            Accounts.Clear();
        }
    }
}
