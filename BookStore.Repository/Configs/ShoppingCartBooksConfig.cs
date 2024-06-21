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
    public class ShoppingCartBooksConfiguration : IEntityTypeConfiguration<ShoppingCartBooks>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartBooks> builder)
        {
            builder.HasOne(scb => scb.Book)
                   .WithMany()
                   .HasForeignKey(scb => scb.BookId);

            builder.HasOne(scb => scb.ShoppingCart)
                   .WithMany(sc => sc.BooksInShoppingCart)
                   .HasForeignKey(scb => scb.ShoppingCartId);

            builder.HasIndex(scb => new { scb.BookId, scb.ShoppingCartId })
                   .IsUnique();
        }
    }
}
