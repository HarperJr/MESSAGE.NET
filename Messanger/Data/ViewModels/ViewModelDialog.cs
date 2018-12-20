using Messanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.ViewModels {
    public class ViewModelDialog {

        public Dialog DialogModel  { get; set; }

        public ICollection<Participant> ParticipantModels { get; set; }
    }
}