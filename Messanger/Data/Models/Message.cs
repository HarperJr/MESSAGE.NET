﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.Models {
    public class Message {

        public int Id { get; set; }

        public int DialogId { get; set; }

        public string SenderId { get; set; }

        public DateTime Time { get; set; }

        public string Content { get; set; }

        public bool HasMultimedia { get; set; }

        public bool Viewed { get; set; }
    }
}