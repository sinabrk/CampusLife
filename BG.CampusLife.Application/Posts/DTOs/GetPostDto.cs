using System;

namespace BG.CampusLife.Application.Posts.DTOs
{
    public class GetPostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public string FullName { get; set; }
    }
}
