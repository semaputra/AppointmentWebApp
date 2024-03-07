using AppointmentWebApp.API.Controllers;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Test.API.Controller
{
    public class UserControllerTest
    {

        [Fact]
        public async void UserController_GetUsers_ReturnOK()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var users = new List<Muser> { new Muser { UserId = Guid.NewGuid(), Username = "User1" }, new Muser { UserId = Guid.NewGuid(), Username = "User2" } };
            userServiceMock.Setup(service => service.GetUsers()).ReturnsAsync(users);
            var controller = new UserController(userServiceMock.Object);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Muser>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Value);
            var model = Assert.IsAssignableFrom<IEnumerable<Muser>>(okResult.Value);
            //Assert.Equal(users.Count, model.Count);
        }
    }
}
