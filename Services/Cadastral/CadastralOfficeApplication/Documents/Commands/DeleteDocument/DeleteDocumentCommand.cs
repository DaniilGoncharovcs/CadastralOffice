namespace CadastralOfficeApplication.Documents.Commands.DeleteDocument;
public class DeleteDocumentCommand : IRequest
{
    public Guid Id { get; set; }
}