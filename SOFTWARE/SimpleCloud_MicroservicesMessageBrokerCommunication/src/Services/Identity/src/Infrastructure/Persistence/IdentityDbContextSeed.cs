﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SimpleCloudMonolithic.Infrastructure.Persistence
{
    public static class IdentityDbContextSeed
    {
        //public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        //{
        //    //var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //    //if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
        //    //{
        //    //    await userManager.CreateAsync(defaultUser, "Administrator1!");
        //    //}
        //}

        public static async Task SeedSampleDataAsync(IdentityDbContext context)
        {
            // Seed, if necessary
            //if (!context.TodoLists.Any())
            //{
            //    context.TodoLists.Add(new TodoList
            //    {
            //        Title = "Shopping",
            //        Items =
            //        {
            //            new TodoItem { Title = "Apples", Done = true },
            //            new TodoItem { Title = "Milk", Done = true },
            //            new TodoItem { Title = "Bread", Done = true },
            //            new TodoItem { Title = "Toilet paper" },
            //            new TodoItem { Title = "Pasta" },
            //            new TodoItem { Title = "Tissues" },
            //            new TodoItem { Title = "Tuna" },
            //            new TodoItem { Title = "Water" }
            //        }
            //    });

                // await context.SaveChangesAsync();
            //}
        }
    }
}
