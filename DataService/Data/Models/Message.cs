using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataService.Data.Models {
    public class Message {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Dialog Dialog { get; set; }

        public Consumer Sender { get; set; }

        public DateTime Time { get; set; }

        [MaxLength(1024)]
        public string Content { get; set; }

        public bool HasMultimedia { get; set; } 

        public bool Viewed { get; set; }
    }
}