namespace BG.CampusLife.Domain.Entities;

public class Location : BaseEntity
{
    public string Title { get; set; }

    public int Level { get; set; }

    public Guid? ParentId { get; set; }
    public Location Parent { get; set; }
    
    public double Longitude { get; set; }
    
    public double Latitude { get; set; }

    public bool Status { get; set; }

    public ICollection<Location> Children { get; set; }
}