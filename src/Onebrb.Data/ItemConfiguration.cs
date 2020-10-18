using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onebrb.Core.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Data
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        private const string tableName = "Items";

        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("integer")
                .IsRequired();

            builder.HasIndex(x => x.Id)
                .HasName("Id")
                .IsUnique();

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                            .HasColumnName("Description")
                            .HasColumnType("varchar(2000)")
                            .HasMaxLength(2000)
                            .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnName("Price")
                .HasColumnType("DECIMAL(12,2)");
        }
    }
}
