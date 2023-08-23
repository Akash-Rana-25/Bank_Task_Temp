using BankManagment_Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagment_Infrastructure.Seed
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasData(
                new PaymentMethod { Id = Guid.NewGuid(), Name = "Cash" },
                new PaymentMethod { Id = Guid.NewGuid(), Name = "Cheque" },
                new PaymentMethod { Id = Guid.NewGuid(), Name = "NEFT" },
                new PaymentMethod { Id = Guid.NewGuid(), Name = "RTGS" },
                new PaymentMethod { Id = Guid.NewGuid(), Name = "Other" }
            );
        }
    }
}
