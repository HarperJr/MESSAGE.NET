using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Models {
    public class Consumer {

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime LastTimeOnline { get; set; }

        public string PhoneNumber { get; set; }

        public string AvatarId { get; set; }
    }
}