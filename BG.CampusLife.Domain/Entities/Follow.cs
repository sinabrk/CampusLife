using BG.CampusLife.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Follow
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User UserNavigation { get; set; }


        public Guid FollowId { get; set; }
        [ForeignKey("FollowId")]
        public User FollowNavigation { get; set; }
    }
}
