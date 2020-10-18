using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Data
{
    public class OnebrbContext : DbContext
    {
        public OnebrbContext(DbContextOptions<OnebrbContext> options) 
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }
}
