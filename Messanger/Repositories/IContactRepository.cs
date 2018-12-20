using Messanger.Data.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {
    public interface IContactRepository : IRepository<Contact, int> {

        Contact GetByRelatedConsumersId(string initialConsumerId, string relatedConsumerId);

        void UpdateStatus(Contact model, string newStatus);
    }
}