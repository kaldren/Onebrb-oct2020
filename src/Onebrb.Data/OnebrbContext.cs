using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Models;
using Onebrb.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public class OnebrbContext : DbContext, IOnebrbContext
    {
        public OnebrbContext(DbContextOptions<OnebrbContext> options) 
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        public async Task<Item> GetItemAsync(long itemId)
        {
            return await this.Items.SingleOrDefaultAsync(x => x.Id == itemId);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }
}
