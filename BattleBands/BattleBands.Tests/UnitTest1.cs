using BattleBands.Data;
using BattleBands.Models;
using System;
using Xunit;
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
using BattleBands.Models.AdminViewModels;

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
        }

        [Fact]
        public void AdminControllerDefaultRoleForUser()
        {

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
            //                .Options;
            //var Context = new ApplicationDbContext(options);
            string email = "qwerty@gmail.com";
            string password = "Qwerty123456!";

            var controller = new AdminController(userManager, roleManager, Context);

            var createUser = new CreateUserViewModel
            {
                Email = email,
                Password = password
            };


            controller.CreateUser(createUser).Wait();

            var resultUser = userManager.FindByEmailAsync(email);
            var result = userManager.IsInRoleAsync(resultUser.Result, "user");
            Assert.True(result.Result);
            
        }

        //[Fact]
        //public void Test()
        //{

        //    string email = "qwerty@gmail.com";
        //    string password = "Qwerty123456!";

        //    var controller = new AdminController(userManager, roleManager, Context);

        //    var createUser = new CreateUserViewModel
        //    {
        //        Email = email,
        //        Password = password
        //    };
        //    controller.CreateUser(createUser).Wait();

        //    var findUsr = userManager.FindByEmailAsync(email);


        //    string newEmail = "ytrewq@gmail.com";
        //    var editUser = new EditUserViewModel
        //    {
        //        Email = newEmail,
        //        Id = findUsr.Result.Id
        //    };
        //    controller.EditUser(editUser).Wait();

        //    var resultUser = userManager.FindByIdAsync(findUsr.Result.Id);
        //    Assert.Equal(newEmail, resultUser.Result.Email);

        //    //var res = userManager.FindByEmailAsync(email);
        //    //Assert.Null(res.Result);
        //}
    }
}
