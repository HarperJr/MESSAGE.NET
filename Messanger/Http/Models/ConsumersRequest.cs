using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.Models {
    public class ConsumersRequest {

        public string Name { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}