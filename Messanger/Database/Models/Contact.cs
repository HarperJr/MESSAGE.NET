using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Contact {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long InitTime { get; set; }

        [MaxLength(32)]
        public string Status { get; set; }

        [ForeignKey("InitialConsumer")]
        public string InitialConsumerId { get; set; }

        public Consumer InitialConsumer { get; set; }

        [ForeignKey("RelatedConsumer")]
        public string RelatedConsumerId { get; set; }

        public Consumer RelatedConsumer { get; set; }
    }
}