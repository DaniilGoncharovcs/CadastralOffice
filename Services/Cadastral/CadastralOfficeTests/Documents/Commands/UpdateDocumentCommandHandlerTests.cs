namespace CadastralOfficeTests.Documents.Commands;

public class UpdateDocumentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateDocumentCommandHandler_Success()
    {
        // Arrange

        var handler = new UpdateDocumentCommandHandler(Context);
        var updateName = "new name";

        // Act

        await handler.Handle(new UpdateDocumentCommand
        {
            Id = TestDbContextFactory.DocumentForUpdate,
            Name = updateName
        }
        , CancellationToken.None);

        // Assert

        Assert.NotNull(
            await Context.Documents.SingleOrDefaultAsync(
                document => document.Id == TestDbContextFactory.DocumentForUpdate &&
                document.Name == updateName
                ));
    }

}