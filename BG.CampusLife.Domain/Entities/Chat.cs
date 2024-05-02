using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.CampusLife.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; } = false;
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
