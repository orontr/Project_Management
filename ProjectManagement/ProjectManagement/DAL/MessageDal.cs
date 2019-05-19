using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectManagement.Models;

namespace ProjectManagement.DAL
{
    public class MessageDal : DbContext
    {
        public DbSet<Message> messages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>().ToTable("Message");
        }
    }
}