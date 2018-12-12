using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Dialog {

        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Shortcut { get; set; }

        public string Title { get; set; }

        public DateTime InitTime { get; set; }
    }
}