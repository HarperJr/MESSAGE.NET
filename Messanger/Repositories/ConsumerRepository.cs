using AutoMapper;
using DataService;
using Messanger.DataServiceRef;
using Messanger.Logger;
using Messanger.Mappers;
using Messanger.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Messanger.Repositories {
    public class ConsumerRepository : IConsumerRepository {

        private readonly ILogger _logger = LogFactory.Factory.GetLogger<ConsumerRepository>();

        private readonly IMapper _mapper;
        private readonly DataServiceClient _dataService;

        public ConsumerRepository() {
            _mapper = MapperConfig.Initialize();
            _dataService = new DataServiceClient();
        }

        public Consumer GetConsumerById(string id) {
            ConsumerContract contract = _dataService.GetConsumerById(id);
            _logger.Trace($"GetConsumerById {id}, received {contract?.Name}");
            return _mapper.Map<Consumer>(
                contract);
        }

        public ICollection<Consumer> GetConsumerContactsById(string id) {
            return _mapper.Map<ICollection<Consumer>>(
                _dataService.GetConsumerContactsById(id)); 
        }

        public ICollection<Consumer> GetConsumersByMatchingNameWithOffsetAndLimit(string name, int offset, int limit) {
            return _mapper.Map<ICollection<Consumer>>(
                _dataService.GetDialogsByConsumerIdWithOffsetAndLimit(name, offset, limit));
        }

        public void Insert(Consumer model) {
            _dataService.InsertConsumer(_mapper.Map<ConsumerContract>(model));
        }

        public void InsertAll(ICollection<Consumer> models) {
            throw new NotImplementedException();
        }

        public void Delete(Consumer model) {
            throw new NotImplementedException();
        }
    }
}