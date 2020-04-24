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


namespace RevDogs.Testing
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        public  UsersControllerTests()
        {
            var mockRepository = new Mock<IProject2Repository>();
            var user = new RevDogs.Core.Model.Users
            {
                Id = 1,
                UserName = "TestUserName",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Score = 1
            };

        }


    }
}