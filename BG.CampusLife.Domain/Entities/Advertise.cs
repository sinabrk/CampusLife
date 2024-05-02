using System;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Advertise : Post
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
