using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class Multimedia {

        [Key]
        [MaxLength(64)]
        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [MaxLength(64)]
        public string RemotePath { get; set; }
    }
}