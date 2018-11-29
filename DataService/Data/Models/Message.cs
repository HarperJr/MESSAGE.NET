using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataService.Data.Models {
    public class Message {

        [Key]
        public int Id { get; set; }

        public int DialogId { get; set; }

        [MaxLength(64)]
        public string SenderId { get; set; }

        public DateTime Time { get; set; }

        public string Content { get; set; }

        public ICollection<Multimedia> Multimedia { get; set; }

        public bool HasMultimedia { get; set; } 

        public bool Viewed { get; set; }
    }
}