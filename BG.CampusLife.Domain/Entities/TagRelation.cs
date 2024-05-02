using System;
using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class TagRelation
    {
        public Guid TagId { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }

        public Guid EntityId { get; set; }
        public EntityTypes EntityType { get; set; }
    }
}