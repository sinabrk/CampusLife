using System;
using System.ComponentModel.DataAnnotations.Schema;
using BG.CampusLife.Domain.Enums;
using Newtonsoft.Json;

namespace BG.CampusLife.Domain.Entities
{
    // TODO : Check the realation and correct it
    public class Call
    {
        public Guid Id { get; set; } = new();

        public DateTime Created { get; set; } = DateTime.Now;

        public Guid SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        public Guid ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }

        public float Duration { get; set; }

        public CallStatus Status { get; set; }

        public int Type { get; set; }

    }
}
