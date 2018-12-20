﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.DataTypes {
    public enum WebSocketRequestType {
        SEND_MESSAGE,
        ADD_CONTACT,
        DELETE_CONTACT,
        NOTIFY,
        INVITE,
        CREATE_DIALOG
    }
}