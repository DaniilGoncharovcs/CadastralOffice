namespace CadastralOfficeApplication.Documents.Commands.CreateDocument;
public class CreateDocumentCommandHandler
    : IRequestHandler<CreateDocumentCommand, Guid>
{
    private readonly IAppDbContext _dbContext;

    public CreateDocumentCommandHandler(IAppDbContext context)
        => _dbContext = context;

    public async Task<Guid> Handle(CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _dbContext.Documents.AddAsync(document, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}
