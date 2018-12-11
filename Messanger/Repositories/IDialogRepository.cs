using Messanger.Data.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {
    public interface IDialogRepository : IRepository<Dialog, int> {

        ICollection<Dialog> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit);
    }
}