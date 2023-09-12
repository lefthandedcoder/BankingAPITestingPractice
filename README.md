# **Banking API**
### The Banking API provides basic banking functionalities such as depositing and withdrawing money and checking account balances. 
### It's built using .NET, and features a simple in-memory database to manage account balances.

## **Features:**
### Deposit money into an account.
### Withdraw money from an account.
### Check account balance.

## **Endpoints:**
### POST /deposit - Deposit a certain amount to a user's account.
### POST /withdraw - Withdraw a certain amount from a user's account.
### GET /balance/{userId} - Retrieve the balance of a specific user.

## **Validation:**
### **Deposit:**
Negative amounts or zero are not allowed.
Cannot deposit more than $10,000 in a single transaction.
Cannot withdraw more than 90% of an account balance.

## **Withdrawal:**
Withdrawals should not result in negative balances.
Withdrawal amounts cannot be negative.

## **Testing:**
The API uses SpecFlow for BDD testing.

## **Test Scenarios:**

Test deposit functionality with valid amounts.
Test deposit functionality with amounts greater than $10,000.
Test withdrawal functionality to ensure it's not possible to have negative balances.

## Usage:

### Deposit:

`POST /deposit`

`Body:
{
    "UserId": 1,
    "Amount": 500.50
}`

### Withdraw:

`POST /withdraw`

`Body:
{
    "UserId": 1,
    "Amount": 100.50
}`

### Check Balance:

`GET /balance/1`

## **Future Improvements:**

Integration with a persistent database for robust data storage.
Implement authentication and authorization to ensure secure transactions.
Add interest rate calculations.
