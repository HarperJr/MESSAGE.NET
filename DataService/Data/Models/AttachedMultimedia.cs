using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataService.Data.Models {
    public class AttachedMultimedia {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Message Message { get; set; }

        public Multimedia Multimedia { get; set; }

        public bool IsStatic { get; set; }
    }
}