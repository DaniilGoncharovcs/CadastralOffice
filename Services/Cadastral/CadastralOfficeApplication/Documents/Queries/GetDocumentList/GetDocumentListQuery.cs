﻿namespace CadastralOfficeApplication.Documents.Queries.GetDocumentList;
public class GetDocumentsListQuery : IRequest<DocumentListVm>
{
    public string Name { get; set; }
}