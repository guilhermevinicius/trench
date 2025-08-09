using Trench.User.Domain.Resources;
using Trench.User.FunctionalTests.Config;
using Trench.User.FunctionalTests.Config.Helpers;

namespace Trench.User.FunctionalTests.Controllers;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class UserControllerTest(IntegrationTestWebAppFactory fixture)
{
    [Fact]
    public async Task UserEndpoint_RegisterUser_ShouldBeSuccess()
    {
        // Arrange
        const string uri = "/api/v1/users/register";
        const string json = $$"""
                              {
                                  "firstName": "First Name",
                                  "lastName": "Last name",
                                  "email": "test@gmail.com",
                                  "username": "trench.username",
                                  "birthDate": "1999-08-04T00:00:00.511Z",
                                  "password": "Test123456789"
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, json);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.True(response?.Success);
        Assert.Equal(201, response?.StatusCode);
    }

    [Fact]
    public async Task UserEndpoint_RegisterUser_ShouldBeReturnErrorWhenUsernameAlreadyExists()
    {
        // Arrange
        const string uri = "/api/v1/users/register";
        const string json = $$"""
                              {
                                  "firstName": "First Name",
                                  "lastName": "Last name",
                                  "email": "outher@trench.com",
                                  "username": "trench",
                                  "birthDate": "1999-08-04T00:00:00.511Z",
                                  "password": "Test123456789"
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, json);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.False(response?.Success);
        Assert.Equal(400, response?.StatusCode);
        Assert.Equal(DomainValidationResource.AlreadyUsernameExists, response?.Messages?[0]);
    }

    [Fact]
    public async Task UserEndpoint_RegisterUser_ShouldBeReturnErrorWhenEmailAlreadyExists()
    {
        // Arrange
        const string uri = "/api/v1/users/register";
        const string json = $$"""
                              {
                                  "firstName": "First Name",
                                  "lastName": "Last name",
                                  "email": "trench@trench.com",
                                  "username": "username",
                                  "birthDate": "1999-08-04T00:00:00.511Z",
                                  "password": "Test123456789"
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, json);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.False(response?.Success);
        Assert.Equal(400, response?.StatusCode);
        Assert.Equal(DomainValidationResource.AlreadyEmailExists, response?.Messages?[0]);
    }
    
    [Fact]
    public async Task UserEndpoint_GetUserLogging_ShouldBeSuccess()
    {
        // Arrange
        const string uri = "/api/v1/users/me";

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Get, uri, null);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.True(response?.Success);
        Assert.Equal(200, response?.StatusCode);
    }

    [Fact]
    public async Task UserEndpoint_Follower_ShouldBeReturnSuccess()
    {
        // Arrange
        var uri = "/api/v1/users/follower";
        const string body = $$"""
                              {
                                "followerId": 2
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, body);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.True(response?.Success);
        Assert.Equal(201, response?.StatusCode);
    }
}