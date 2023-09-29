using BankingAPI.Database;
using BankingAPI.Models;
using BankingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly BankingService _bankingService;

        public BankingController()
        {
            _bankingService = new BankingService();
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] TransactionModel model)
        {
            if (model.Amount <= 0)
                return BadRequest("Cannot deposit a negative amount or zero.");
            if (model.Amount > 10000)
                return BadRequest("Cannot deposit more than $10,000 in a single transaction.");
            if (!ModelState.IsValid)
                return BadRequest("Invalid input. Please provide a valid decimal amount.");

            var result = _bankingService.Deposit(model.UserId, model.Amount);
            if (result.HasValue)
                return Ok(result.Value);
            else
                return NotFound("User not found.");
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] TransactionModel model)
        {
            if (!InMemoryDatabase.Accounts.ContainsKey(model.UserId))
                return NotFound("User not found.");
            if (model.Amount <= 0)
                return BadRequest("Withdrawal amount cannot be negative or zero.");
            if (!ModelState.IsValid)
                return BadRequest("Invalid input. Please provide a valid decimal amount.");

            var result = _bankingService.Withdraw(model.UserId, model.Amount);
            if (result.HasValue)
                return Ok(result.Value);
            else
                return BadRequest("Invalid withdrawal request. Check account balance and withdrawal restrictions.");
        }

        [HttpGet("balance/{userId}")]
        public IActionResult GetBalance(int userId)
        {
            var balance = _bankingService.GetBalance(userId);
            if (balance.HasValue)
                return Ok(new { UserId = userId, Balance = balance.Value });
            else
                return NotFound("User not found.");
        }
    }
}
