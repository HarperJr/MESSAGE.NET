using DataService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Mappers {
    public class ConsumerMapper {

        private readonly ContactMapper _contactMapper = new ContactMapper();

        public ConsumerContract ModelToContract(Consumer consumer) {
            if (consumer == null) return null;
            ConsumerContract consumerContract = new ConsumerContract() {
                Name = consumer.Name,
                AvatarId = consumer.Avatar.Id,
                PhoneNumber = consumer.PhoneNumber,
                LastTimeOnline = consumer.LastTimeOnline,
                Contacts = _contactMapper.ModelListToContractList(consumer.Contacts)
            };
            return consumerContract;
        }

        public ICollection<ConsumerContract> ModelListToContractList(ICollection<Consumer> consumers) {
            return consumers.Select(x => ModelToContract(x)).ToList();
        }
    }

    public class ContactMapper {

        public ContactContract ModelToContract(Consumer consumer) {
            if (consumer == null) return null;
            ContactContract contactContract = new ContactContract() {
                Id = consumer.Id,
                Name = consumer.Name,
                AvatarId = consumer.Avatar.Id
            };
            return contactContract;
        }

        public ICollection<ContactContract> ModelListToContractList(ICollection<Consumer> consumers) {
            return consumers.Select(x => ModelToContract(x)).ToList();
        }
    }
}