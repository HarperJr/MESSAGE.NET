using Messanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.ViewModels {
    public class ViewModelMessage {

        public Message MessageModel { get; set; } 

        public Consumer SenderModel { get; set; }

        public ICollection<string> AttachedMultimedias { get; set; }
    }
}