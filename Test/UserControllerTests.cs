using Xunit;
using LMS_Learning_Menagment_System_API.Controllers;
using LMS_Learning_Menagment_System_API.Models;
using LMS_Learning_Menagment_System_API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class UserControllerTests
{
    private readonly UsersController _controller;
    private readonly LmsContext _context;

    public UserControllerTests()
    {
        // Set up the in-memory database with a separate service provider
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<LmsContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        _context = new LmsContext(options);

        // Add sample data to the in-memory database
        _context.Users.Add(new User
        {
            Iduser = 1,
            UserName = "testuser",
            Password = "testpassword",
            Email = "testuser@example.com",
            FirstName = "Test",
            LastName = "User"
        });

        _context.Users.Add(new User
        {
            Iduser = 2,
            UserName = "anotheruser",
            Password = "anotherpassword",
            Email = "anotheruser@example.com",
            FirstName = "Another",
            LastName = "User"
        });

        _context.SaveChanges();
        // Set up in-memory configuration
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "dummy_secret_for_development_mode"},  // Example setting
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Initialize AuthHelpers with configuration
        var authHelpers = new AuthHelpers(configuration);

        // Initialize the controller with the context and helpers
        _controller = new UsersController(_context, authHelpers);
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Act
        var result = await _controller.GetUsers();

        // Assert
        Assert.NotNull(result);
        var users = Assert.IsType<List<User>>(result.Value);
        Assert.Equal(2, users.Count); // Assuming you added 2 users in the setup
    }
    [Fact]
    public async Task GetUsers_ReturnsEmptyList_WhenNoUsersExist()
    {
        // Arrange: Start with an empty in-memory database
        _context.Users.RemoveRange(_context.Users);  // Ensure no users exist
        await _context.SaveChangesAsync();  // Apply changes

        // Act: Call the GetUsers method
        var result = await _controller.GetUsers();

        // Assert: Verify the result is an empty list
        Assert.NotNull(result);  // The result should not be null
        var users = Assert.IsType<List<User>>(result.Value);  // Check that the result is a list of users
        Assert.Empty(users);  // The list should be empty since there are no users in the database
    }
    [Fact]
    public async Task GetUser_ReturnsUser()
    {
        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.NotNull(result);
        var user = Assert.IsType<User>(result.Value);
        Assert.Equal("testuser", user.UserName);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound()
    {
        // Act
        var result = await _controller.GetUser(999); 

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
    [Fact]
    public async Task GetUser_GetsNegativeID()
    {
        var result = await _controller.GetUser(-1);
        Assert.IsType<NotFoundResult>(result.Result);
    }
    [Fact]
    public async Task PutUser_UpdatePassword_ShouldUpdateUser()
    {
        // Arrange
        // Initial data setup
        var userId = 1;
        var originalUser = await _controller.GetUser(userId);

        // Check if the original user exists
        if (originalUser.Result is NotFoundResult)
        {
            throw new InvalidOperationException("User with the given ID does not exist.");
        }

        var user = originalUser.Value;

        // Make sure the user exists
        Assert.NotNull(user);

        // Update the user's password
        user.Password = "newpassword";

        // Act
        // Call the PUT method
        var updateResult = await _controller.PutUser(userId, user);

        // Assert
        // Verify the result
        Assert.IsType<NoContentResult>(updateResult);

        // Fetch the updated user from the database
        var updatedUserResult = await _controller.GetUser(userId);
        var updatedUser = updatedUserResult.Value as User;

        // Verify the password was updated
        Assert.NotNull(updatedUser);
        Assert.Equal("newpassword", updatedUser.Password);
    }
}
