using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BG.CampusLife.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsSuspend { get; set; }
        public bool IsActive { get; set; }
        public string ConnectionId { get; set; }
        public DateTime LastDeactivate { get; set; }
    }
}