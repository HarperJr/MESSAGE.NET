using AutoMapper;
using Database;
using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Database.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories {
    public class MessageRepository : IMessageRepository {

        private IMapper _mapper;
        private readonly MessageDao _messageDao;

        public MessageRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _messageDao = new MessageDao(localDbContext);
        }

        public void Delete(Message model) {
            throw new NotImplementedException();
        }

        public Message GetById(string id) {
            throw new NotImplementedException();
        }

        public void Insert(Message model) {
            throw new NotImplementedException();
        }
    }
}