﻿namespace CadastralOfficeWebApi.DTO;

public class UpdateDocumentDto : IMapWith<UpdateDocumentCommand>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateDocumentDto, UpdateDocumentCommand>()
            .ForMember(documentCommand => documentCommand.Id,
                opt => opt.MapFrom(documentDto => documentDto.Id))
            .ForMember(documentCommand => documentCommand.Name,
                opt => opt.MapFrom(documentDto => documentDto.Name));
    }
}