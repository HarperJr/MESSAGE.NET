

using DataService.Data;
using DataService.Mappers;
using System.Collections.Generic;

namespace DataService {
    public class DataService : IDataService {

        private readonly LocalDbContext _localDbContext = new LocalDbContext();
        private readonly ConsumerMapper _consumerMapper;
        
        public DataService() {
            _consumerMapper = new ConsumerMapper();
        }

        public ICollection<ConsumerContract> GetConsumerContactsById(string id) {
            return GetConsumerDataById(id).Contacts;
        }

        public ConsumerContract GetConsumerDataById(string id) {
           return _consumerMapper.ModelToContract(_localDbContext.Consumers.Find(id));
        }

        public void InsertConsumerContact(ConsumerContract consumerContract, string contactId) {
        }

        public void InsertConsumerData(ConsumerContract consumerContract) {
        }
    }
}
