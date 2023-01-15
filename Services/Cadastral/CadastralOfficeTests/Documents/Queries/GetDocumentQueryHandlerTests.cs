namespace CadastralOfficeTests.Documents.Queries;

[Collection("QueryCollection")]
public class GetDocumentQueryHandlerTests
{
    private readonly AppDbContext Context;
    private readonly IMapper Mapper;

    public GetDocumentQueryHandlerTests(QueryTestFixture fixture)
        => (Context, Mapper) = (fixture.Context, fixture.Mapper);

    [Fact]
    public async Task GetDocumentQueryHandler_Success()
    {
        // Arrange

        var handler = new GetDocumentQueryHandler(Context, Mapper);

        // Act

        var result = await handler.Handle(new GetDocumentQuery
        {
            Id = Guid.Parse("640e147a-08aa-4b50-a40c-77b90a210b80")
        }
        , CancellationToken.None);

        // Assert

        result.ShouldBeOfType<GetDocumentVm>();
        result.Name.ShouldBe("document1");
    }
}
