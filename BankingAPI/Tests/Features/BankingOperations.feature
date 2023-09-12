Feature: Banking Operations

Scenario: User 1 Deposits Money Successfully
  Given user 1 has a balance of "$500"
  When user 1 deposits "$100"
  Then the new balance for user 1 should be "$600"

Scenario: User 2 Withdraws Money Successfully
  Given user 2 has a balance of "$500"
  When user 2 withdraws "$100"
  Then the new balance for user 2 should be "$400"

Scenario: User 3 Tries to Go Below Minimum Balance
  Given user 3 has a balance of "$150"
  When user 3 withdraws "$100"
  Then the withdrawal should be declined with message "Account balance cannot go below $100."

Scenario: User 4 Tries a Large Single Transaction Withdrawal
  Given user 4 has a balance of "$10000"
  When user 4 withdraws "$9001"
  Then the withdrawal should be declined with message "Cannot withdraw more than 90% of the balance. Max withdrawable amount is $9000.0."

Scenario: User 5 Tries a Large Single Transaction Deposit
  Given user 5 has a balance of "$500"
  When user 5 deposits "$11000"
  Then the deposit should be declined with message "Cannot deposit more than $10,000 in a single transaction."

Scenario: User 6 Attempts to Withdraw a Negative Amount
  Given user 6 has a balance of "$1000"
  When user 6 withdraws "$-50"
  Then the withdrawal should be declined with message "Withdrawal amount cannot be negative or zero."

Scenario: User 7 Attempts to Withdraw a Zero Amount
  Given user 7 has a balance of "$1000"
  When user 7 withdraws "$0"
  Then the withdrawal should be declined with message "Withdrawal amount cannot be negative or zero."

Scenario: User 8 Attempts to Deposit a Negative Amount
  Given user 8 has a balance of "$1000"
  When user 8 deposits "$-50"
  Then the deposit should be declined with message "Cannot deposit a negative amount or zero."

Scenario: User 9 Attempts to Deposit a Zero Amount
  Given user 9 has a balance of "$1000"
  When user 9 deposits "$0"
  Then the deposit should be declined with message "Cannot deposit a negative amount or zero."

Scenario: User 10 Withdraws a Valid Decimal Amount
  Given user 10 has a balance of "$1000"
  When user 10 withdraws "$50.75"
  Then the new balance for user 10 should be "$949.25"

Scenario: User 11 Deposits a Valid Decimal Amount
  Given user 11 has a balance of "$1000"
  When user 11 deposits "$50.75"
  Then the new balance for user 11 should be "$1050.75"
