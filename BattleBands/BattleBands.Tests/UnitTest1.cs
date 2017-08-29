using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BattleBands.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using BattleBands.Controllers;
using BattleBands.Models.ViewModels.AdminViewModels;

namespace BattleBands.Tests
{
    public class UnitTest1
    {
        private ApplicationDbContext Context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UnitTest1()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=PAYNE-PC\\SQLEXPRESS;Database=TestBattleBandsDb;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            var context = new DefaultHttpContext();

            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName: "teeestdb")
            //    .Options;
            Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            init().Wait();
        }
        public async Task init()
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("moder") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moder"));
            }

            var testUserOne = new ApplicationUser
            {
                UserName = "maksim.tantsyura@gmail.com",
                Email = "maksim.tantsyura@gmail.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(testUserOne, "Qwerty123456!");
            var testUserTwo = new ApplicationUser
            {
                UserName = "qwerty@gmail.com",
                Email = "qwerty@gmail.com",
                EmailConfirmed= true
            };
            await userManager.CreateAsync(testUserTwo, "Qwerty123456!");
        }

        [Fact]
        public void AdminControllerDefaultRoleForUser()
        {
            string email = "createUserTest@gmail.com";
            string password = "Qwerty123456!";

            var deleteMe = userManager.FindByEmailAsync(email);
            if (deleteMe.Result != null) userManager.DeleteAsync(deleteMe.Result);
            
            var controller = new AdminController(userManager, roleManager, Context);

            var createUser = new CreateUserViewModel
            {
                Email = email,
                Password = password
            };


            controller.CreateUser(createUser).Wait();

            var resultUser = userManager.FindByEmailAsync(email);
            var result = userManager.IsInRoleAsync(resultUser.Result, "user");
            deleteMe = userManager.FindByEmailAsync(email);
            userManager.DeleteAsync(deleteMe.Result);
            Assert.True(result.Result);
        }

    }
}
