namespace CadastralOfficeTests.Documents.Commands;
public class CreateDocumentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateDocumentCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateDocumentCommandHandler(Context);
        var documentName = "name";

        // Act
        var documentId = await handler.Handle(
            new CreateDocumentCommand
            {
                Name = documentName
            },
            CancellationToken.None);

        // Assert

        Assert.NotNull(
            await Context.Documents.SingleOrDefaultAsync(document => document.Id == documentId)
        );
    }
}
