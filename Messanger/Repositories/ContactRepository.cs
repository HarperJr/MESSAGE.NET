using Messanger.DataService;
using Messanger.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {

    public class ContactRepository : IContactRepository {

        public static ContactRepository Instance { get; private set; }

        private readonly IDataService _dataService;

        public ContactRepository(IDataService dataService) {
            _dataService = dataService;
        }

        public IEnumerable<Contact> GetConsumerContacts(Consumer consumer) {
            return null;
        }
    }
}