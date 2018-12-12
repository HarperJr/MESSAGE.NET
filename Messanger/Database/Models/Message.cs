using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Message {

        [Key]
        public string Id { get; set; }

        public Dialog Dialog { get; set; }

        public Consumer Sender { get; set; }

        public long Time { get; set; }

        [MaxLength(4096)]
        public string Content { get; set; }

        public bool HasMultimedia { get; set; } 

        public bool Viewed { get; set; }
    }
}