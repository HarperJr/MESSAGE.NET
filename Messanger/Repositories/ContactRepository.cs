using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messanger.Data.Models;
using Messanger.Database;

namespace Messanger.Repositories {
    public class ContactRepository : IContactRepository {


        public ContactRepository(LocalDbContext localDbContext) {

        }

        public void Delete(Contact model) {
            throw new NotImplementedException();
        }

        public Contact GetById(int id) {
            throw new NotImplementedException();
        }

        public void Insert(Contact model) {
            throw new NotImplementedException();
        }
    }
}