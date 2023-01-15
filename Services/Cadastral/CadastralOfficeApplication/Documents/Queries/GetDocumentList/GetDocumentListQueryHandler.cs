namespace CadastralOfficeApplication.Documents.Queries.GetDocumentList;

public class GetDocumentListQueryHandler
    :IRequestHandler<GetDocumentsListQuery, DocumentListVm>
{
    private readonly IAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDocumentListQueryHandler(IAppDbContext context, IMapper mapper)
        => (_dbContext, _mapper) = (context, mapper);

    public async Task<DocumentListVm> Handle(GetDocumentsListQuery request,
        CancellationToken cancellationToken)
    {
        var documentsQuery = _dbContext.Documents.AsNoTracking();

        if (request.Name is not null)
            documentsQuery = documentsQuery.Where(d => EF.Functions.Like(
                d.Name, $"%{request.Name}%")
            );

        var result = _mapper.Map<List<DocumentDto>>(await documentsQuery.ToListAsync(cancellationToken));

        return new DocumentListVm { Documents = result};
    }
}