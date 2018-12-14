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

        public ICollection<Message> GetMessagesByDialogIdWithOffsetAndLimit(string dialogId, int offset, int limit) {
            return _mapper.Map<ICollection<Message>>(
                _messageDao.GetMessagesByDialogIdWithOffsetAndLimit(dialogId, offset, limit));
        }

        public void Delete(Message model) {
            _messageDao.Delete(_mapper.Map<Database.Models.Message>(model));
        }

        public Message GetById(string id) {
            return _mapper.Map<Message>(_messageDao.GetById(id));
        }

        public void Insert(Message model) {
            _messageDao.Insert(_mapper.Map<Database.Models.Message>(model));
        }
    }
}