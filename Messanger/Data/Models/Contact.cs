using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Contact {
        public int Id { get; set; }

        public DateTime InitTime { get; set; }

        public string Status { get; set; }

        public string ContactId { get; set; }
    }
}