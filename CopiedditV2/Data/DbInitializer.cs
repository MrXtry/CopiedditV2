﻿using CopiedditV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CopiedditV2Context context)
        {
            context.Database.EnsureCreated();

            if (context.Posts.Any())
                return;
            return;
            //var authors = new Author[]
            //{
            //    new Author { FirstName = "Carson", LastName = "Alexander", Email = "AlexanderCarson@hotmail.com", Password = "Test123!", DateCreated = DateTime.Now },
            //    new Author { FirstName = "Meredith", LastName = "Alonso", Email = "AlonsoMeredith@hotmail.com", Password = "Test123!", DateCreated = DateTime.Now },
            //    new Author { FirstName = "Arturo", LastName = "Anand", Email = "AnandArturo@hotmail.com", Password = "Test123!", DateCreated = DateTime.Now },
            //    new Author { FirstName = "Gytis", LastName = "Barzdukas", Email = "BarzdukasGytis@hotmail.com", Password = "Test123!", DateCreated = DateTime.Now },
            //    new Author { FirstName = "Yan", LastName = "Li", Email = "LiYan@hotmail.com", Password = "Test123!", DateCreated = DateTime.Now }
            //};
            //foreach (Author a in authors)
            //{
            //    context.Authors.Add(a);
            //}
            //context.SaveChanges();
            var userEmail = "JohnDoe@hotmail.com";
            //var users = new ApplicationUser[]
            //{
            //    new ApplicationUser
            //    {
            //        Email = userEmail, NormalizedEmail = userEmail.ToUpper(), NormalizedUserName = userEmail.ToUpper() 
            //    }
            //}

            //var posts = new Post[]
            //{
            //    new Post { Title = "Test 1", Url = "", DateCreated = DateTime.Now },
            //    new Post { Title = "Test 2", Url = "", DateCreated = DateTime.Now },
            //    new Post { Title = "Test 3", Url = "", DateCreated = DateTime.Now },
            //    new Post { Title = "Test 4", Url = "", DateCreated = DateTime.Now },
            //    new Post { Title = "Test 5", Url = "", DateCreated = DateTime.Now },
            //    new Post { Title = "Test 6", Url = "", DateCreated = DateTime.Now }
            //};
            //foreach (Post p in posts)
            //{
            //    context.Posts.Add(p);
            //}
            //context.SaveChanges();

            //var comments = new Comment[]
            //{
            //    new Comment { PostId = posts.Single(p => p.Title == "Test 1").Id, ParentId = null, Content = "Test", DateCreated = DateTime.Now }
            //};
            //foreach (Comment c in comments)
            //{
            //    context.Comments.Add(c);
            //}
            //context.SaveChanges();
        }
    }
}