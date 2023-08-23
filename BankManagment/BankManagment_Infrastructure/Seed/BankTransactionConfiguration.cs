﻿using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BankManagment_Infrastructure.Seed
{
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        private readonly IConfiguration _configuration;

        public BankTransactionConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            Guid firstAccount = Guid.NewGuid();
            var numberOfBankTransactionRecords = _configuration["AppSettings:NumberOfBankTransactionRecords"];

            if (int.TryParse(numberOfBankTransactionRecords, out int numberOfBankTransactionRecordsInt) && numberOfBankTransactionRecordsInt > 0)
            {

                var random = new Random();

                var bankTransactions = Enumerable.Range(0, numberOfBankTransactionRecordsInt)
                    .Select(i => new BankTransaction
                    {
                        Id = Guid.NewGuid(),
                        TransactionPersonFirstName = "Akash",
                        TransactionPersonLastName = "Rana",
                        TransactionType = (i % 2 == 0) ? "Credit" : "Debit",
                        Category = GetRandomCategory(random),
                        Amount = (decimal)random.NextDouble() * 1000,
                        TransactionDate = DateTime.Now.AddDays(-i),
                        PaymentMethodID = Guid.Empty, // Placeholder for now
                        BankAccountID = Guid.Empty // Placeholder for now
                    })
                    .ToArray();

                builder.HasData(bankTransactions);
            }
        }

        //private Guid GetRandomPaymentMethodId()
        //{
        //    var paymentMethods = _dbContext.PaymentMethods.ToList();
        //    var random = new Random();
        //    var randomPaymentMethod = paymentMethods[random.Next(paymentMethods.Count)];
        //    return randomPaymentMethod.Id;
        //}

        //private Guid GetRandomBankAccountId()
        //{
        //    var bankAccounts = _dbContext.BankAccounts.ToList();
        //    var random = new Random();
        //    var randomBankAccount = bankAccounts[random.Next(bankAccounts.Count)];
        //    return randomBankAccount.Id;
        //}

        private string GetRandomCategory(Random random)
        {
            var categories = new[] { "Opening Balance", "Bank Interest", "Bank Charges", "Normal Transactions" };
            return categories[random.Next(categories.Length)];
        }
    }
}
