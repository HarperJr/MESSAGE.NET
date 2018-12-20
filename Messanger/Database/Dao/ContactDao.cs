using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao {
    public class ContactDao : IDao<Contact, int> {

        private readonly LocalDbContext _localDbContext;

        public ContactDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public void Delete(Contact entity) {
            _localDbContext.Contacts
                .Remove(entity);
            _localDbContext.SaveChanges();
        }

        public Contact GetById(int id) {
            return _localDbContext.Contacts
                .Find(id);
        }

        public void Insert(Contact entity) {
            _localDbContext.Contacts
                .Add(entity);
            _localDbContext.SaveChanges();
        }

        public Contact GetByRelatedConsumersId(string initialConsumerId, string relatedConsumerId) {
            return _localDbContext.Contacts
                .Where(contact => contact.InitialConsumer.Id.Equals(initialConsumerId) &&
                contact.RelatedConsumer.Id.Equals(relatedConsumerId))
                .FirstOrDefault();
        }

        public void UpdateStatus(Contact entity, string newStatus) {
            Contact foundContact = _localDbContext.Contacts
                .Where(contact => contact.Id.Equals(entity.Id) &&
                contact.InitialConsumer.Id.Equals(entity.InitialConsumerId) &&
                contact.RelatedConsumer.Id.Equals(entity.RelatedConsumerId))
                .First();
            foundContact.Status = newStatus;
            _localDbContext.SaveChanges();
        }
    }
}