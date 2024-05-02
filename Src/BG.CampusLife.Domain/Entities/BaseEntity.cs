namespace BG.CampusLife.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    
    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public bool IsDeleted { get; set; }
}