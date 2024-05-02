namespace BG.CampusLife.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    public string UserId { get; set; }

    #region SignUp Props
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public GenderType Gender { get; set; } = GenderType.Unknown;
    public DateTime? Birthday { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    #endregion

    #region Profile Properties
    public bool Private { get; set; }
    
    [MaxLength(500)]
    public string Bio { get; set; }
    public byte MarriageStatus { get; set; }
    public string Department { get; set; }

    public Guid? ImageId { get; set; }
    public Document Image { get; set; }

    public Guid? UniversityId { get; set; }
    public University University { get; set; } = null;

    public Guid? LocationId { get; set; }
    public Location Location { get; set; }

    #endregion

    #region Student Props
    public DateTime? Started { get; set; }
    public DateTime? Graduation { get; set; }
    public bool? Graduated { get; set; }
    #endregion

    #region Faculty Props
    [MaxLength(150)]
    public string Title { get; set; }

    [MaxLength(150)]
    public string PersonalEmail { get; set; }

    [JsonIgnore]
    [MaxLength(150)]
    public string NormalizedPersonalEmail { get; set; }
    #endregion

    #region Explorer Props
    [MaxLength(150)]
    public string AdditionalEmail { get; set; }

    [JsonIgnore]
    [MaxLength(150)]
    public string NormalizedAdditionalEmail { get; set; }
    #endregion

    public ICollection<SharedCalendar> SharedCalendars { get; set; }
    public ICollection<Post> Posts { get; set; }

}