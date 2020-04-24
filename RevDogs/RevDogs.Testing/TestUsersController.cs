using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevDogsApi.Model;


namespace RevDogs.Testing
{
    [TestClass]
    public class TestUsersController
    {
        [TestMethod]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            var testUsers = GetTestUsers();
            var controller = new UsersController(testUsers);
            var result = await controller.GetUsersAsync(4) as OkNegotiatedContentResult<Users>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testUsers[3].Username, result.Content.Username);
        }
    }
}