using Messanger.Data.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {
    public interface IMessageRepository : IRepository<Message, string> {
    }
}