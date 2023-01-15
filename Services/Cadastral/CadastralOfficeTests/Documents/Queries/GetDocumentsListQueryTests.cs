namespace CadastralOfficeTests.Documents.Queries;

[Collection("QueryCollection")]
public class GetDocumentListQueryHandlerTests
{
    private readonly AppDbContext Context;
    private readonly IMapper Mapper;

    public GetDocumentListQueryHandlerTests(QueryTestFixture fixture)
        => (Context, Mapper) = (fixture.Context, fixture.Mapper);

    [Fact]
    public async Task GetDocumentListQueryHandler_Success()
    {
        // Arrange

        var handler = new GetDocumentListQueryHandler(Context, Mapper);

        // Act

        var result = await handler.Handle(new GetDocumentsListQuery(), CancellationToken.None);

        // Assert

        result.ShouldBeOfType<DocumentListVm>();
        result.Documents.Count.ShouldBe(3);
    }
}
