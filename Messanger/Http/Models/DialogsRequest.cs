using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.Models {
    public class DialogsRequest {

        public string Uid { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}