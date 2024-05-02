using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Domain.Entities
{
    public class File
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public DateTime Created { get; set; }
    }
}
