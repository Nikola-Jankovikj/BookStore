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
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasOne(sc => sc.Owner)
                   .WithMany()
                   .HasForeignKey(sc => sc.OwnerId);

            builder.HasMany(sc => sc.BooksInShoppingCart)
                   .WithOne(scb => scb.ShoppingCart)
                   .HasForeignKey(scb => scb.ShoppingCartId);
        }
    }
}
