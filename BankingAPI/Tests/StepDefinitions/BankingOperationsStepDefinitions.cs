using BankingAPI.Controllers;
using BankingAPI.Database;
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
        private BankingServiceHelper _serviceHelper = new BankingServiceHelper();

        [BeforeScenario]
        public void Setup()
        {
            InMemoryDatabase.Reset();
        }

        [Given(@"user (\d+) has a balance of ""\$(.*)""")]
        public void GivenTheUserHasABalanceOf(int userId, decimal initialBalance)
        {
            _currentUserId = userId;
            _serviceHelper.SetUserBalance(userId, initialBalance);
        }

        [When(@"user (\d+) deposits ""\$(.*)""")]
        public void WhenTheUserDeposits(int userId, decimal amount)
        {
            _result = _serviceHelper.Deposit(userId, amount);
        }

        [When(@"user (\d+) withdraws ""\$(.*)""")]
        public void WhenTheUserWithdraws(int userId, decimal amount)
        {
            _result = _serviceHelper.Withdraw(userId, amount);
        }

        [Then(@"the new balance for user (\d+) should be ""\$(.*)""")]
        public void ThenTheNewBalanceShouldBe(int userId, decimal expectedBalance)
        {
            _result = _controller.GetBalance(userId);

            Assert.IsInstanceOf<OkObjectResult>(_result, "Unable to get balance for user or user not found.");

            dynamic value = ((OkObjectResult)_result).Value;
            decimal actualBalance = value.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Then(@"the withdrawal should be declined with message ""(.*)""")]
        public void ThenTheWithdrawalShouldBeDeclinedWithMessage(string expectedMessage)
        {
            Assert.IsNotNull(_result, "Latest response was not set.");

            AssertResultHasErrorMessageAndIsNotOk(_result, expectedMessage);
        }

        [Then(@"the deposit should be declined with message ""(.*)""")]
        public void ThenTheDepositShouldBeDeclinedWithMessage(string expectedMessage)
        {
            Assert.IsNotNull(_result, "Latest response was not set.");

            AssertResultHasErrorMessageAndIsNotOk(_result, expectedMessage);
        }

        private void AssertResultHasErrorMessageAndIsNotOk(IActionResult result, string expectedMessage)
        {
            Assert.IsInstanceOf<ObjectResult>(result, "The response was not an ObjectResult as expected.");

            var objectResult = (ObjectResult)result;

            Assert.IsFalse(objectResult.StatusCode.Equals(200), "The status code was OK but was not expected to be.");
            Assert.AreEqual(expectedMessage, objectResult.Value);
        }

    }
}
