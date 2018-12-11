using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Multimedia {
        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string RemotePath { get; set; }
    }
}