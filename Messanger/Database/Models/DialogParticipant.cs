using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Messanger.Database.Models {
    public class DialogParticipant {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Dialog")]
        public string DialogId { get; set; }

        public Dialog Dialog { get; set; }

        [ForeignKey("Participant")]
        public string ParticipantId { get; set; }

        public Consumer Participant { get; set; }

        [ForeignKey("Invitor")]
        public string InvitorId { get; set; }

        public Consumer Invitor { get; set; }
    }
}