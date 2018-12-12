using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Participant {
        public int Id { get; set; }

        public string DialogId { get; set; }

        public string ParticipantId { get; set; }

        public string InvitorId { get; set; }

    }
}