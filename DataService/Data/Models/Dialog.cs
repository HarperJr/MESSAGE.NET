using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataService.Data.Models {
    public class Dialog {

        [Key]
        public int Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        public DateTime InitDate { get; set; }

        public ICollection<Consumer> Participants { get; set; }
    }
}