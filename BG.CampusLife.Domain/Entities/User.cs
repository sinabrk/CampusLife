using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BG.CampusLife.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = new();

        [Required]
        public string UserId { get; set; }

        #region SignUp Props
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public GenderType Gender { get; set; } = GenderType.Unknown;

        public DateTime? Birthday { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string NormalizedEmail { get; set; }
        #endregion

        #region Profile Properties
        public bool Private { get; set; }
        
        [MaxLength(500)]
        public string Bio { get; set; }
        
        public byte MarriageStatus { get; set; }

        public Guid? ImageId { get; set; }
        [JsonIgnore]
        [ForeignKey("ImageId")]
        public Document Image { get; set; }

        public Guid? UniversityId { get; set; }
        [JsonIgnore]
        [ForeignKey("UniversityId")]
        public University University { get; set; } = null;

        public Guid? LocationId { get; set; }
        [JsonIgnore]
        [ForeignKey("LocationId")]
        public Location UserLocation { get; set; }

        public Guid? FriendId { get; set; }
        [ForeignKey("FriendId")]
        public User Friend{ get; set; }

        public List<User> FriendsList { get; set; }
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

        #region Just Relations
        [JsonIgnore]
        public List<Post> Posts { get; set; }
        [JsonIgnore]
        public List<Blog> Blogs { get; set; }
        [JsonIgnore]
        public Property Department { get; set; }
        [JsonIgnore]
        public List<Community> Community { get; set; }
        #endregion

        //#region Removed Prop

        //public Guid? NationalityId { get; set; }
        //public Guid? HomeLocationId { get; set; }
        //[JsonIgnore]
        //public Location HomeLocation { get; set; }
        //[JsonIgnore]
        //public Location Nationality { get; set; }
        //[JsonIgnore]
        //public List<Property> WorkExperience { get; set; }
        //[JsonIgnore]
        //public List<Property> EducationalBackground { get; set; }
        //[JsonIgnore]
        //public List<Property> Hobbies { get; set; }
        //[JsonIgnore]
        //public List<Property> Activities { get; set; }
        //[JsonIgnore]
        //public List<Property> Sports { get; set; }
        //[JsonIgnore]
        //public List<Property> Certificates { get; set; }
        //[JsonIgnore]
        //public List<Property> Awards { get; set; }

        ////> Student
        //[JsonIgnore]
        //public Property MainMajor { get; set; }
        //[JsonIgnore]
        //public List<Property> Majors { get; set; }
        //[JsonIgnore]
        //public List<Property> InterestedAt { get; set; }
        //[JsonIgnore]
        //public List<Property> GoodAt { get; set; }
        //#endregion
    }
}