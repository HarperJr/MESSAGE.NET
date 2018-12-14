using AutoMapper;
using Database;
using Messanger.Data;
using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Database.Dao;
using System;
using System.Collections.Generic;

namespace Messanger.Repositories {
    public class ConsumerRepository : IConsumerRepository {

        private readonly IMapper _mapper;
        private readonly ConsumerDao _consumerDao;

        public ConsumerRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _consumerDao = new ConsumerDao(localDbContext);
        }

        public void Delete(Consumer model) {
            throw new NotImplementedException();
        }

        public Consumer GetById(string id) {
            return _mapper.Map<Consumer>(_consumerDao.GetById(id));
        }

        public ICollection<Consumer> GetMatchNameWithOffsetAndLimit(string name, int offset, int limit) {
            return _mapper.Map<ICollection<Consumer>>(
                _consumerDao.GetConsumersMatchNameWithOffsetAndLimit(name, offset, limit)
                );
        }

        public Consumer FindByName(string name) {
            return _mapper.Map<Consumer>(_consumerDao.FindByName(name));
        }

        public void Insert(Consumer model) {
            _consumerDao.Insert(_mapper.Map<Database.Models.Consumer>(model));
        }
    }
}