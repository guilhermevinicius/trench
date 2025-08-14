using Trench.User.Domain.Aggregates.Follower.Dtos;
using Trench.User.FunctionalTests.Config;
using Trench.User.FunctionalTests.Config.Helpers;

namespace Trench.User.FunctionalTests.Controllers;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class FollowerControllerTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task FollowerController_GetFollowers_ShouldBeSuccess()
    {
        // Arrange
        const string uri = "/api/v1/followers";

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Get, uri, null);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.True(response?.Success);
        Assert.Equal(200, response?.StatusCode);
    }

    [Fact]
    public async Task FollowerController_Follow_ShouldBeSuccess()
    {
        // Arrange
        const string uri = "/api/v1/followers";
        const string json = $$"""
                             {
                                "followerId": 1,
                                "followingId": 1
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
    public async Task FollowerController_Follow_ShouldBeReturnErrorWhenAlreadyFollowerExists()
    {
        // Arrange
        const string uri = "/api/v1/followers";
        const string json = $$"""
                              {
                                 "followerId": 1,
                                 "followingId": 2
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, json);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.False(response?.Success);
        Assert.Equal(400, response?.StatusCode);
        Assert.Equal("Already follower exists", response?.Messages?[0]);
    }

    [Fact]
    public async Task FollowerController_Follow_ShouldBeReturnErrorWhenFollowingNotFound()
    {
        // Arrange
        const string uri = "/api/v1/followers";
        const string json = $$"""
                              {
                                 "followerId": 1,
                                 "followingId": 200
                              }
                              """;

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Post, uri, json);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.False(response?.Success);
        Assert.Equal(400, response?.StatusCode);
        Assert.Equal("User not found", response?.Messages?[0]);
    }
}