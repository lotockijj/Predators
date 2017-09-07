using BattleBands.Controllers;
using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BattleBands.Tests.ControllerTests
{
    public class HomeControllerTest
    {
        [Fact]
        public void TestMethod1()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase()
                .Options;

            var context = new ApplicationDbContext(options);
            var appUser = new ApplicationPerformer();
            appUser.PerformerName = "Performer";
            context.Users.AddRange(Enumerable.Range(1, 10).Select(t => new ApplicationUser()));
            context.SaveChanges();

            var controller = new HomeController(context);
            var result = controller.Index();
            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationPerformer>>(result);
            Assert.Equal(1, model.Count());

        }
    }
}
