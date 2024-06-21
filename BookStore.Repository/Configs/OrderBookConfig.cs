using BookStore.Domain.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Configs
{
    public class OrderBooksConfiguration : IEntityTypeConfiguration<OrderBooks>
    {
        public void Configure(EntityTypeBuilder<OrderBooks> builder)
        {
            builder.HasOne(ob => ob.Book)
                   .WithMany()
                   .HasForeignKey(ob => ob.BookId);

            builder.HasOne(ob => ob.Order)
                   .WithMany(o => o.BooksInOrder)
                   .HasForeignKey(ob => ob.OrderId);

            builder.HasIndex(ob => new { ob.BookId, ob.OrderId })
                   .IsUnique();
        }
    }
}
