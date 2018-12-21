using Messanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.ViewModels {
    public class ViewModelDialog {

        public string DialogId { get; set; }

        public DateTime InitTime { get; set; }

        public string DialogTitle { get; set; }
    }
}