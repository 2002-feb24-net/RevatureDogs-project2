using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RevDogs.DataAccess.Repository;
using RevDogs.Core.Interfaces;
using RevDogsApi.Controllers;
using RevDogsApi.Models;
using RevDogs.Core.Model;
using FluentAssertions;
using RevDogs.DataAccess.Model;
using RevDogs.DataAccess;

namespace RevDogs.Testing
{
    public class UsersControllerTests
    {


            public void GetUsers()
            {
            //  var mockRepository = new Mock<IProject2Repository>();
            //  var user = new RevDogs.Core.Model.Users
             var listOfUsers = new Core.Model.Users();
             listOfUsers = new Core.Model.Users
             {
                Id = 1,
                UserName = "TestUserName",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Score = 1,
             };


            var a = Mapper.MapUsers(listOfUsers);
            Mock<IProject2Repository> mockIProject2Repository = new Mock<IProject2Repository>();
            mockIProject2Repository.Setup(x => x.GetUsersAsync()).Verifiable();
            var usersController = new UsersController(mockIProject2Repository.Object);
            usersController.Should().NotBeNull();
            }

    }
}