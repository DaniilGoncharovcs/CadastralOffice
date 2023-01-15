namespace CadastralOfficeApplication.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler
    : IRequestHandler<UpdateDocumentCommand>
{
    private readonly IAppDbContext _dbContext;

    public UpdateDocumentCommandHandler(IAppDbContext context)
        => _dbContext = context;

    public async Task<Unit> Handle(UpdateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var document = await _dbContext.Documents
            .FirstOrDefaultAsync(document => document.Id == request.Id, cancellationToken);

        if (document == null)
            throw new NotFoundException(nameof(Document), request.Id);

        document.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}