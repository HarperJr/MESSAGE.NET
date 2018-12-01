using DataService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataService.Mappers {
    public class ConsumerMapper {

        public ConsumerContract ModelToContract(Consumer consumer) {
            if (consumer == null) return null;
            ConsumerContract consumerContract = new ConsumerContract() {
                Name = consumer.Name,
                AvatarId = consumer.Avatar.Id,
                PhoneNumber = consumer.PhoneNumber,
                LastTimeOnline = consumer.LastTimeOnline,
                Contacts = ModelListToContractList(consumer.Contacts)
            };
            return consumerContract;
        }

        public ICollection<ConsumerContract> ModelListToContractList(ICollection<Consumer> consumers) {
            return consumers.Select(x => ModelToContract(x)).ToList();
        }

        public Consumer ContractToModel(ConsumerContract consumerContract) {
            if (consumerContract == null) return null;
            Consumer consumer = new Consumer() {
                Id = consumerContract.Id,
                Name = consumerContract.Name,
                PhoneNumber = consumerContract.PhoneNumber
            };
            return consumer;
        }

        public ICollection<Consumer> ContractListToModelList(ICollection<ConsumerContract> consumerContracts) {
            return consumerContracts.Select(x => ContractToModel(x)).ToList();
        }
    }
}