namespace NKZSoft.Template.Presentation.REST.Tests.Controllers;

using System.Net;
using MediatR;
using Models;
using SeedData;
using NKZSoft.Template.Presentation.REST.Tests.Common;
using Xunit.Abstractions;

public class ToDoItemControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private const string ApiUrlBase = "api/v1/to-do-items";

    private readonly CustomWebApplicationFactory<Startup> _factory;
    private readonly ITestOutputHelper testOutputHelper;

    public ToDoItemControllerTest(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        this.testOutputHelper = testOutputHelper;
    }

    private static class Get
    {
        public static string GetToDoItem(int id) => $"{ApiUrlBase}/{id}";
    }

    [Theory]
    [InlineData(1)]
    public async Task GetByIdTestAsync(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(Get.GetToDoItem(id));

        response.EnsureSuccessStatusCode();

        var content = await response.GetContentAsync<ResultDto<ToDoItemDto>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<ToDoItemDto>>();
        content!.IsSuccess.Should().BeTrue();

        content.Data.Should().NotBeNull();
        content.Data.Title.Should().Be(SeedDataContext.ToDoItems.First().Title);
        content.Data.Note.Should().Be(SeedDataContext.ToDoItems.First().Note);
    }

    [Theory]
    [InlineData(999)]
    public async Task GetByIdNotFoundTestAsync(int id)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(Get.GetToDoItem(id));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.GetContentAsync<ResultDto<Unit>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<Unit>>();
        content!.IsSuccess.Should().BeFalse();
    }
}
