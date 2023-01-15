namespace CadastralOfficeTests.Documents.Commands;
public class DeleteDocumentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteDocumentCommandHandler_Success()
    {
        // Arrange

        var handler = new DeleteDocumentCommandHandler(Context);

        // Act

        await handler.Handle(new DeleteDocumentCommand
        {
            Id = TestDbContextFactory.DocumentForDelete
        }
        , CancellationToken.None);

        // Assert

        Assert.Null(
            await Context.Documents.SingleOrDefaultAsync(
                document => document.Id == TestDbContextFactory.DocumentForDelete
                ));
    }
}