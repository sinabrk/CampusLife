namespace BG.CampusLife.Domain.Entities;

public class Document : BaseEntity
{
    public bool IsPrimary { get; set; }
    
    [MaxLength(20)]
    public string Extension { get; set; }
    
    [MaxLength(20)]
    public string ContentType { get; set; }
    
    [MaxLength(500)]
    public string FileName { get; set; }

    [MaxLength(500)]
    public string FilePath { get; set; }

}
