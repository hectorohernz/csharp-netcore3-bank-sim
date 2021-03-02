using System;
namespace BankApp.Models
{
    public class Account
    {
        public Account(string name, double amount, string ownerUsername)
        {
            this.name = name;
            this.amount = amount;
            this.ownerUsername = ownerUsername;
        }

       

        public string name { get; set; }
        public double amount { get; set;}
        public string ownerUsername { get; set; }

        public void withdraw(double amount)
        {
            this.amount = this.amount - amount;
        }

        public void deposit(double amount)
        {
            this.amount = this.amount + amount;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
