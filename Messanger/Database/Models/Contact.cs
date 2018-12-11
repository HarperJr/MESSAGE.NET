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

        public DateTime InitTime { get; set; }

        [MaxLength(32)]
        public string Status { get; set; }

        public Consumer InitialConsumer { get; set; }

        public Consumer RelatedConsumer { get; set; }
    }
}