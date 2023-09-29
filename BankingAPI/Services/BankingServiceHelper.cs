// This is a simplified version; you might expand on this for a full implementation.
using BankingAPI.Controllers;
using BankingAPI.Database;
using BankingAPI.Models;
using Microsoft.AspNetCore.Mvc;

public class BankingServiceHelper
{
    private BankingController _controller = new BankingController();

    public IActionResult Deposit(int userId, decimal amount)
    {
        return _controller.Deposit(new TransactionModel { UserId = userId, Amount = amount });
    }

    public IActionResult Withdraw(int userId, decimal amount)
    {
        return _controller.Withdraw(new TransactionModel { UserId = userId, Amount = amount });
    }

    public IActionResult GetBalance(int userId)
    {
        return _controller.GetBalance(userId);
    }

    public void SetUserBalance(int userId, decimal balance)
    {
        if (InMemoryDatabase.Accounts.ContainsKey(userId))
        {
            InMemoryDatabase.Accounts[userId] = balance;
        }
        else
        {
            InMemoryDatabase.Accounts.Add(userId, balance);
        }
    }
}
