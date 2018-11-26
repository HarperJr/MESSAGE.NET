
using Messanger.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {

    public class ContactRepository : IContactRepository {

        public static ContactRepository Instance { get; private set; }


        public ContactRepository() {

        }

        public IEnumerable<Contact> GetConsumerContacts(Consumer consumer) {
            return null;
        }
    }
}