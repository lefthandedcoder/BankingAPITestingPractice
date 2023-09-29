using BankingAPI.Database;

namespace BankingAPI.Services
{
    public class BankingService
    {
        public decimal? Deposit(int userId, decimal amount)
        {
            if (InMemoryDatabase.Accounts.ContainsKey(userId))
            {
                InMemoryDatabase.Accounts[userId] += amount;
                return InMemoryDatabase.Accounts[userId];
            }
            return null;
        }

        public decimal? Withdraw(int userId, decimal amount)
        {
            if (InMemoryDatabase.Accounts.ContainsKey(userId))
            {
                decimal currentBalance = InMemoryDatabase.Accounts[userId];
                decimal maxWithdrawableAmount = currentBalance * 0.9M;
                decimal postTransactionBalance = currentBalance - amount;

                if (amount <= maxWithdrawableAmount && postTransactionBalance >= 100)
                {
                    InMemoryDatabase.Accounts[userId] -= amount;
                    return InMemoryDatabase.Accounts[userId];
                }
            }
            return null;
        }

        public decimal? GetBalance(int userId)
        {
            if (InMemoryDatabase.Accounts.TryGetValue(userId, out decimal balance))
            {
                return balance;
            }
            return null;
        }
    }
}
