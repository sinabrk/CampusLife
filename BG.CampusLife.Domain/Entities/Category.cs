using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = new();
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        public CategoryTypes CategoryType { get; set; }
        
        public int Level { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Slug { get; set; }

        public Guid CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public bool Status { get; set; }
    }
}