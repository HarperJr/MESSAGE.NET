using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Dialog {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Consumer Owner { get; set; }

        public Multimedia Shortcut { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        public DateTime InitDate { get; set; }
    }
}