using Messanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories.Base {
    public interface IContactRepository {

        IEnumerable<Contact> GetConsumerContacts(Consumer consumer);
    }
}