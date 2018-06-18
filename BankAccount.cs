/*
    This file will contain the definition of a bank account.

    1. It has a 10-digit number that uniquely identifies the bank account.
    2. It has a string that stores the name or names of the owners.
    3. The balance can be retrieved.
    4. It accepts deposits.
    5. It accepts withdrawals.
    6. The initial balance must be positive.
    7. Withdrawals cannot result in a negative balance.

*/

using System;
using System.Collections.Generic;

// This quickstart is relatively small, so you'll put all the code in
// one namespace.
namespace classes
{
    // public class BankAccount defines the class, or type, you are creating.
    // Everything inside the { and } that follows the class declaration defines
    // the behavior of the class. There are five members of the BankAccount
    // class. The first three are properties. Properties are data elements and
    // can have code that enforces validation or other rules. The last two are
    // methods. Methods are blocks of code that perform a single function.
    public class BankAccount
    {

        // Creating a new object of the BankAccount type means defining a
        // constructor that assigns those values. A constructor is a member that
        // has the same name as the class. It is used to initialize objects of
        // that class type.
        public BankAccount(string name, decimal initialBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        public string Number { get; }
        public string Owner { get; set; }

        // Balance is found by summing the values of all transactions.
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }
        private List<Transaction> allTransactions = new List<Transaction>();

        // This is a data member. It's private, which means it can only be
        // accessed by code inside the BankAccount class. It's a way of
        // separating the public responsibilities (like having an account
        // number) from the private implementation (how account numbers are
        // generated.)
        private static int accountNumberSeed = 1234567890;

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                // The standard way of indicating that a method cannot complete
                // its work successfully is to throw an exception.
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                // The standard way of indicating that a method cannot complete
                // its work successfully is to throw an exception.
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        // Creates a string for the transaction history.
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            report.AppendLine("Date\t\tAmount\tNote");
            foreach (var item in allTransactions)
            {
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");
            }

            return report.ToString();
        }
    }
}
