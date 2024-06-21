using BookStore.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Configs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Owner)
                   .WithMany()
                   .HasForeignKey(o => o.userId);

            builder.HasMany(o => o.BooksInOrder)
                   .WithOne(ob => ob.Order)
                   .HasForeignKey(ob => ob.OrderId);
        }
    }
}
