using CopiedditV2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopiedditV2.ViewModels;

namespace CopiedditV2.Data
{
    public class CopiedditV2Context : DbContext
    {
        public CopiedditV2Context(DbContextOptions<CopiedditV2Context> options) : base (options)
        {
        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Comment>().ToTable("Comment");
        }
    }
}
