﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Models;
using Onebrb.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public class OnebrbContext : IdentityDbContext<User>, IOnebrbContext
    {
        public OnebrbContext(DbContextOptions<OnebrbContext> options) 
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        public async Task<Item> GetItemAsync(int itemId)
        {
            return await this.Items.SingleOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<ICollection<Item>> GetItemsAsync(string username)
        {
            User user =  await this.Users.SingleOrDefaultAsync(x => x.UserName == username);
            
            if (user == null)
            {
                return null;
            }

            return await this.Items.Where(x => x.UserId == user.Id).ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }

        public async Task<bool> DeleteAsync(int itemId)
        {
            Item item = await this.Items.FirstOrDefaultAsync(x => x.Id == itemId);

            if (item == null)
            {
                return false;
            }

            this.Items.Remove(item);
            int result = await this.SaveChangesAsync();

            return result > 0;
        }
    }
}
