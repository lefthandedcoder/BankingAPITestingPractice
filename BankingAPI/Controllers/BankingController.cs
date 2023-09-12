using BankingAPI.Database;
using BankingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    {
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] TransactionModel model)
        {
            // Check for negative input amounts
            if (model.Amount <= 0)
                return BadRequest("Cannot deposit a negative amount or zero.");


            // Check if more than $10,000 is deposited at once
            if (model.Amount > 10000)
                return BadRequest("Cannot deposit more than $10,000 in a single transaction.");

            // Check if user exists
            if (InMemoryDatabase.Accounts.ContainsKey(model.UserId))
            {
                InMemoryDatabase.Accounts[model.UserId] += model.Amount;
                return Ok(InMemoryDatabase.Accounts[model.UserId]);
            }

            // Error handling for invalid input
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please provide a valid decimal amount.");
            }

            return NotFound("User not found.");
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] TransactionModel model)
        {
            // Check if user is in database
            if (!InMemoryDatabase.Accounts.ContainsKey(model.UserId))
                return NotFound("User not found.");

            // Check if amount is negative
            if (model.Amount <= 0)
            {
                return BadRequest("Withdrawal amount cannot be negative or zero.");
            }

            // Error handling for invalid input
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please provide a valid decimal amount.");
            }

            decimal currentBalance = InMemoryDatabase.Accounts[model.UserId];
            decimal maxWithdrawableAmount = currentBalance * 0.9M;

            // Check if withdraw amount exceeds max withdrawable amount
            if (model.Amount > maxWithdrawableAmount)
                return BadRequest($"Cannot withdraw more than 90% of the balance. Max withdrawable amount is ${maxWithdrawableAmount}.");

            decimal postTransactionBalance = currentBalance - model.Amount;

            // Check if post transaction balance is below 100
            if (postTransactionBalance < 100)
                return BadRequest("Account balance cannot go below $100.");

            InMemoryDatabase.Accounts[model.UserId] -= model.Amount;
            return Ok(InMemoryDatabase.Accounts[model.UserId]);
        }

        [HttpGet("balance/{userId}")]
        public IActionResult GetBalance(int userId)
        {
            if (InMemoryDatabase.Accounts.TryGetValue(userId, out decimal balance))
            {
                return Ok(new { UserId = userId, Balance = balance });
            }
            return NotFound("User not found.");
        }
    }
}