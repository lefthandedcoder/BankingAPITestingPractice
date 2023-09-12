using BankingAPI.Controllers;
using BankingAPI.Database;
using BankingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BankingAPI.StepDefinitions
{
    [Binding]
    public class BankingOperationsStepDefinitions
    {
        private int _currentUserId;
        private IActionResult _result;
        private BankingController _controller = new BankingController();

        [BeforeScenario]
        public void Setup()
        {
            InMemoryDatabase.Reset();
        }

        [Given(@"user (\d+) has a balance of ""\$(.*)""")]
        public void GivenTheUserHasABalanceOf(int userId, decimal initialBalance)
        {
            _currentUserId = userId;
            if (InMemoryDatabase.Accounts.ContainsKey(userId))
            {
                InMemoryDatabase.Accounts[userId] = initialBalance;
            }
            else
            {
                InMemoryDatabase.Accounts.Add(userId, initialBalance);
            }
        }

        [When(@"user (\d+) deposits ""\$(.*)""")]
        public void WhenTheUserDeposits(int userId, decimal amount)
        {
            _result = _controller.Deposit(new TransactionModel { UserId = userId, Amount = amount });
        }

        [When(@"user (\d+) withdraws ""\$(.*)""")]
        public void WhenTheUserWithdraws(int userId, decimal amount)
        {
            _result = _controller.Withdraw(new TransactionModel { UserId = userId, Amount = amount });
        }

        [Then(@"the new balance for user (\d+) should be ""\$(.*)""")]
        public void ThenTheNewBalanceShouldBe(int userId, decimal expectedBalance)
        {
            _result = _controller.GetBalance(userId);

            if (_result is OkObjectResult okResult)
            {
                dynamic value = okResult.Value;
                decimal actualBalance = value.Balance;
                Assert.AreEqual(expectedBalance, actualBalance);
            }
            else
            {
                Assert.Fail("Unable to get balance for user or user not found.");
            }
        }

        [Then(@"the withdrawal should be declined with message ""(.*)""")]
        public void ThenTheWithdrawalShouldBeDeclinedWithMessage(string expectedMessage)
        {
            if (_result == null)
            {
                Assert.Fail("Latest response was not set.");
            }
            if (_result is ObjectResult objectResult)
            {
                Assert.IsFalse(objectResult.StatusCode.Equals("200"));
                Assert.AreEqual(expectedMessage, objectResult.Value);
            }
        }

        [Then(@"the deposit should be declined with message ""(.*)""")]
        public void ThenTheDepositShouldBeDeclinedWithMessage(string expectedMessage)
        {
            if (_result == null)
            {
                Assert.Fail("Latest response was not set.");
            }
            if (_result is ObjectResult objectResult)
            {
                Assert.IsFalse(objectResult.StatusCode.Equals("200"));
                Assert.AreEqual(expectedMessage, objectResult.Value);
            }
        }

    }
}
