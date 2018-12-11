using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Consumer {

        [Key]
        public string Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public Multimedia Avatar { get; set; }

        [MaxLength(16)]
        public string PhoneNumber { get; set; }
        
        public DateTime LastTimeOnline { get; set; }
    }
}