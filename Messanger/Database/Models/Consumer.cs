using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Consumer {

        [Key]
        public string Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [ForeignKey("Avatar")]
        public string AvatarId { get; set; }

        public Multimedia Avatar { get; set; }

        [MaxLength(16)]
        public string PhoneNumber { get; set; }
        
        public long LastTimeOnline { get; set; }
    }
}