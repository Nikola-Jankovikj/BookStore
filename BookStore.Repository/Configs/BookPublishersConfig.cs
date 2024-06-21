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
    public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
    {
        public void Configure(EntityTypeBuilder<BookPublisher> builder)
        {
            builder.HasOne(bp => bp.Book)
                   .WithMany()
                   .HasForeignKey(bp => bp.BookId);

            builder.HasOne(bp => bp.Publisher)
                   .WithMany()
                   .HasForeignKey(bp => bp.PublisherId);

            builder.HasIndex(bp => new { bp.BookId, bp.PublisherId })
                   .IsUnique();
        }
    }
}
