using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Database;
using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Database.Dao;

namespace Messanger.Repositories {
    public class ContactRepository : IContactRepository {

        private readonly IMapper _mapper;
        private readonly ContactDao _contactDao;

        public ContactRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _contactDao = new ContactDao(localDbContext);
        }

        public void Delete(Contact model) {
            _contactDao.Delete(_mapper.Map<Database.Models.Contact>(model));
        }

        public Contact GetById(int id) {
            return _mapper.Map<Contact>(_contactDao.GetById(id));
        }

        public void Insert(Contact model) {
            _contactDao.Insert(_mapper.Map<Database.Models.Contact>(model));
        }

        public void UpdateStatus(Contact model, string newStatus) {
            _contactDao.UpdateStatus(_mapper.Map<Database.Models.Contact>(model), newStatus);
        }

        public Contact GetByRelatedConsumersId(string initialConsumerId, string relatedConsumerId) {
            return _mapper.Map<Contact>(_contactDao.GetByRelatedConsumersId(initialConsumerId, relatedConsumerId));
        }
    }
}