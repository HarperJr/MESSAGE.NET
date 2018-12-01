
using System.Linq;
using DataService.Data;
using AutoMapper;
using System.Collections.Generic;
using DataService.Data.Models;

namespace DataService {
    public class DataService : IDataService {

        private readonly LocalDbContext _localDbContext = new LocalDbContext();
        
        public DataService() {
            DataMapper.Configure();
        }

        public ICollection<ConsumerContract> GetConsumerContactsById(string id) {
            return Mapper.Map<ICollection<ConsumerContract>>(_localDbContext.Contacts.Where(contact => contact.InitialConsumer.Id.Equals(id)));
        }

        public ConsumerContract GetConsumerDataById(string id) {
           return Mapper.Map<ConsumerContract>(_localDbContext.Consumers.Find(id));
        }

        public void InsertConsumerContact(string consumerId, ContactContract contactContract) {
            _localDbContext.Contacts.Add(Mapper.Map<Contact>(contactContract));
        }

        public void InsertConsumerData(ConsumerContract consumerContract) {
            _localDbContext.Consumers.Add(Mapper.Map<Consumer>(consumerContract));
        }
    }
}
