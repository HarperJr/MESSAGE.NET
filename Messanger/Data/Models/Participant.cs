using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Participant {
        public string Id { get; set; }

        public int DialogId { get; set; }

        public string ParticipantId { get; set; }

        public string InvitorId { get; set; }

    }
}