using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithListOfUsers()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, model.Count);
            Assert.IsTrue(model.Any(u => u.Id == 1 && u.Name == "John" && u.Email == "john@example.com"));
            Assert.IsTrue(model.Any(u => u.Id == 2 && u.Name == "Jane" && u.Email == "jane@example.com"));
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John", model.Name);
            Assert.AreEqual("john@example.com", model.Email);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Details(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_AddsUserToListAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, UserController.userlist.Count);
            Assert.AreEqual(user, UserController.userlist.First());
        }

        [TestMethod]
        public void Edit_ExistingUserId_UpdatesUserAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };
            var updatedUser = new User { Id = 1, Name = "Updated John", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated John", user1.Name);
            Assert.AreEqual("updatedjohn@example.com", user1.Email);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };
            var updatedUser = new User { Id = 3, Name = "Updated John", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(3, updatedUser) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_ExistingUserId_RemovesUserFromListAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Delete(1, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, UserController.userlist.Count);
            Assert.AreEqual(user2, UserController.userlist.First());
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Delete(3, null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
