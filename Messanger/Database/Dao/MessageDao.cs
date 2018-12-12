using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messanger.Database.Models;

namespace Messanger.Database.Dao {
    public class MessageDao : IDao<Message, string> {

        private readonly LocalDbContext _localDbContext;

        public MessageDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public void Delete(Message entity) {
            _localDbContext
                .Messages.Remove(entity);
        }

        public Message GetById(string id) {
            return _localDbContext
                .Messages.Find(id);
        }

        public void Insert(Message entity) {
            _localDbContext
                .Messages.Add(entity);
        }
    }
}