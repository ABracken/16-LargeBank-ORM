using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeBankORM
{
    class Program
    {
        static string line = "--------------------------";

        static void Main(string[] args)
        {
            while (true)
            {
                DisplayCustomerInfo();
                InsertToDB();
            }
        }

        public static void DisplayCustomerInfo()
        {
            using (var db = new LargeBankEntities())
            {
                foreach (var customer in db.Customers)
                {
                    Console.WriteLine("Customer Name: " + customer.FirstName + " " + customer.LastName);

                    Console.WriteLine("Accounts:");

                    foreach (var account in customer.Accounts)
                    {
                        Console.WriteLine("Account # " + account.AccountNumber + " has $" + account.Balance.ToString());

                        Console.WriteLine(line);
                    }

                    Console.WriteLine("With a total balance of $" + customer.Accounts.Sum(a => a.Balance));

                    Console.WriteLine();
                }
            }
        }
        public static string AddToDB(string message)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);

                    string input = Console.ReadLine().ToLower();                    

                    if (input == "customer" || input == "account" || input == "transaction")
                    {
                        return input;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();

                    Console.WriteLine("Please make sure answer is written correctly");
                }
            }
        }
        static void InsertToDB()
        {
            using (var db = new LargeBankEntities())
            {
                string str = AddToDB("Would you like to add a new customer, account, or transaction.? \n Press enter to return to the beginning.");
                                
                switch (str)
                {
                    case "customer":
                        Customer newCustomer = new Customer();

                        newCustomer.CreatedDate = DateTime.Now;

                        Console.WriteLine("First name:");

                        newCustomer.FirstName = Console.ReadLine();

                        Console.WriteLine("Last name:");

                        newCustomer.LastName = Console.ReadLine();

                        Console.WriteLine("Address1:");

                        newCustomer.Address1 = Console.ReadLine();

                        Console.WriteLine("Optional, press enter to bypass\nAddress2:");

                        newCustomer.Address2 = Console.ReadLine();

                        Console.WriteLine("City:");

                        newCustomer.City = Console.ReadLine();

                        Console.WriteLine("State:");

                        newCustomer.States = Console.ReadLine();

                        Console.WriteLine("Zipcode:");

                        newCustomer.Zip = Console.ReadLine();

                        db.Customers.Add(newCustomer);

                        db.SaveChanges();
                        break;
                    case "account":
                        Account newAccount = new Account();

                        newAccount.CreatedDate = DateTime.Now;

                        Console.WriteLine("Customer ID number:");

                        string inputAID = Console.ReadLine();

                        int responseAID = int.Parse(inputAID);

                        newAccount.CustomerID = responseAID;

                        Console.WriteLine("Account Number:");

                        newAccount.AccountNumber = Console.ReadLine();

                        Console.WriteLine("Balance:");

                        string inputABalance = Console.ReadLine();

                        decimal responseABalance = decimal.Parse(inputABalance);

                        newAccount.Balance = responseABalance;

                        db.Accounts.Add(newAccount);

                        db.SaveChanges();
                        break;
                    case "transaction":
                        Transaction newTransaction = new Transaction();

                        Console.WriteLine("Transaction date: ie 02/14/1988");

                        Console.WriteLine("Account ID number:");

                        string inputTID = Console.ReadLine();

                        int responseTID = int.Parse(inputTID);

                        newTransaction.AccountID = responseTID;

                        string inputTDate = Console.ReadLine();

                        DateTime responseTDate = DateTime.Parse(inputTDate);

                        newTransaction.TransactionDate = responseTDate;

                        Console.WriteLine("Amount:");

                        string inputTAmount = Console.ReadLine();

                        decimal responeTAmount = decimal.Parse(inputTAmount);

                        db.Transactions.Add(newTransaction);

                        db.SaveChanges();
                        return;

                }
            }
        }
    }
}


