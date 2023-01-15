namespace CadastralOfficeApplication.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler
    : IRequestHandler<DeleteDocumentCommand>
{
    private readonly IAppDbContext _dbContext;

    public DeleteDocumentCommandHandler(IAppDbContext context)
        => _dbContext = context;

    public async Task<Unit> Handle(DeleteDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = await _dbContext.Documents
                             .FirstOrDefaultAsync(document => document.Id == request.Id, cancellationToken);

        if (document == null)
            throw new NotFoundException(nameof(Document), request.Id);

        _dbContext.Documents.Remove(document);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}