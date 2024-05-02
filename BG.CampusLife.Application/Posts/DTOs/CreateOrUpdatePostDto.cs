using BG.CampusLife.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BG.CampusLife.Application.Posts.DTOs
{
    public class CreateOrUpdatePostDto
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }
        
        public List<Document> Attachments { get; set; }
      
        public string RelatedUserFullName { get; set; }
    }
}
