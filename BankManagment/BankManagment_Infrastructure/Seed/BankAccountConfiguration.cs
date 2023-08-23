﻿using BankManagment_Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment_Infrastructure.Seed
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        private readonly IConfiguration _configuration;

        public BankAccountConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            Guid firstAccount = Guid.NewGuid();
            var numberOfBankAccountRecords = _configuration["AppSettings:NumberOfBankAccountRecords"];

            if (int.TryParse(numberOfBankAccountRecords, out int numberOfBankAccountRecordsInt))
            {
                var bankAccounts = Enumerable.Range(0, numberOfBankAccountRecordsInt)
                    .Select(i => new BankAccount
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Akash",
                        LastName = "Rana",
                        AccountNumber = GenerateRandomAccountNumber(),
                        OpeningDate = DateTime.Now.AddDays(-i),
                        AccountTypeId = firstAccount,
                        TotalBalance = 1000
                    })
                    .ToArray();

                builder.HasData(bankAccounts);
            }
        }

        private string GenerateRandomAccountNumber()
        {
            var random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }
    }

}
