using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManagement.DAL
{
    public class GroupsDal : DbContext
    {
            public DbSet<Groups> groups { get; set; }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Groups>().ToTable("Groups");
            }
    }
}