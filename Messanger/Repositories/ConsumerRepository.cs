using AutoMapper;
using DataService;
using Messanger.Data;
using Messanger.Data.Models;
using Messanger.DataServiceWcf;
using System;
using System.Collections.Generic;

namespace Messanger.Repositories {
    public class ConsumerRepository : IConsumerRepository {

        private readonly IMapper _mapper;
        private readonly DataServiceClient _dataServiceClient;

        public ConsumerRepository() {
            _mapper = DataMapper.Initialize();
            _dataServiceClient = new DataServiceClient();
        }

        public void Delete(Consumer model) {
            throw new NotImplementedException();
        }

        public Consumer GetById(string id) {
            return _mapper.Map<Consumer>(_dataServiceClient.GetConsumerById(id));
        }

        public ICollection<Consumer> GetMatchNameWithOffsetAndLimit(string name, int offset, int limit) {
            return _mapper.Map<ICollection<Consumer>>(
                _dataServiceClient.GetConsumersMatchNameWithOffsetAndLimit(name, offset, limit)
                );
        }

        public void Insert(Consumer model) {
            _dataServiceClient.InsertConsumer(_mapper.Map<ConsumerContract>(model));
        }
    }
}