using CopiedditV2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopiedditV2.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CopiedditV2.Data
{
    public class CopiedditV2Context : DbContext
    {
        public CopiedditV2Context(DbContextOptions<CopiedditV2Context> options) : base (options)
        {
        }
        
        //public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<IdentityRole> IdentityRoles { get; set; }
        public DbSet<IdentityRoleClaim<string>> IdentityRoleClaims { get; set; }
        public DbSet<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
        public DbSet<IdentityUserLogin<string>> IdentityUserLogins { get; set; }
        public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        public DbSet<IdentityUserToken<string>> IdentityUserTokens { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>().HasMany(p => p.Posts);
            //modelBuilder.Entity<Author>().HasMany(c => c.Comments);
            //modelBuilder.Entity<Author>().ToTable("Author");

            //modelBuilder.Entity<Post>().HasOne(a => a.Author);
            //modelBuilder.Entity<Post>().HasMany(c => c.Comments);
            modelBuilder.Entity<Post>().HasKey("Id");
            modelBuilder.Entity<Post>().HasIndex("ApplicationUserId");
            modelBuilder.Entity<Post>().ToTable("Post");

            //modelBuilder.Entity<Comment>().HasOne(a => a.Author);
            //modelBuilder.Entity<Comment>().HasOne(p => p.Post);

            modelBuilder.Entity<Comment>().HasKey("Id");
            modelBuilder.Entity<Comment>().HasIndex("ApplicationUserId");
            modelBuilder.Entity<Comment>().HasIndex("PostId");
            modelBuilder.Entity<Comment>().ToTable("Comment");

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>().HasKey("Id");
            modelBuilder.Entity<Image>().HasIndex("ApplicationUserId");
            modelBuilder.Entity<Image>().ToTable("Image");

            //modelBuilder.Ignore<IdentityUserLogin<string>>();
            //modelBuilder.Ignore<IdentityUserRole<string>>();
            //modelBuilder.Ignore<IdentityUserClaim<string>>();
            //modelBuilder.Ignore<IdentityUserToken<string>>();
            //modelBuilder.Ignore<IdentityUser<string>>();
            //modelBuilder.Ignore<ApplicationUser>();

            //IdentityRole
            modelBuilder.Entity<IdentityRole>().HasKey("Id");
            modelBuilder.Entity<IdentityRole>().HasIndex("NormalizedName").HasName("RoleNameIndex");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");


            //IdentityRoleClaim<string>
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey("Id");
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasIndex("RoleId");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                    .WithMany("Claims")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);


            //IdentityUserClaim<string>
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey("Id");
            modelBuilder.Entity<IdentityUserClaim<string>>().HasIndex("UserId");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasOne("CopiedditV2.Models.ApplicationUser")
                    .WithMany("Claims")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);


            //IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey("LoginProvider", "ProviderKey");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasIndex("UserId");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasOne("CopiedditV2.Models.ApplicationUser")
                    .WithMany("Logins")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);


            //IdentityUserRole<string>
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey("UserId", "RoleId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasIndex("RoleId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasIndex("UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                    .WithMany("Users")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasOne("CopiedditV2.Models.ApplicationUser")
                    .WithMany("Roles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);


            //IdentityUserToken<string>
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey("UserId", "LoginProvider", "Name");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");


            //ApplicationUser
            modelBuilder.Entity<ApplicationUser>().HasKey("Id");
            modelBuilder.Entity<ApplicationUser>().HasIndex("NormalizedEmail").HasName("EmailIndex");
            modelBuilder.Entity<ApplicationUser>().HasIndex("NormalizedUserName").IsUnique().HasName("UserNameIndex");
            modelBuilder.Entity<ApplicationUser>().HasIndex("ImageId");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Image)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey<Image>(i => i.ApplicationUserId);

        }
    }
}
