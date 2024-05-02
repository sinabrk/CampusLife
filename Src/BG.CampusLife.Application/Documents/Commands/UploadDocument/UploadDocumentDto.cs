namespace BG.CampusLife.Application.Documents.Commands.UploadDocument;

public class UploadDocumentDto : IMapFrom<TempDocument>
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TempDocument, UploadDocumentDto>()
            .ForMember(cld => cld.FilePath, 
                opt => 
                    opt.MapFrom(c => c.FilePath.Insert(0, Configs.ApiUrl)));
    }
}