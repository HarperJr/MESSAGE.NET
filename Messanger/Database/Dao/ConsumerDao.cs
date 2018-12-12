using Database;
using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao {
    public class ConsumerDao : IDao<Consumer, string> {

        private readonly LocalDbContext _localDbContext;

        public ConsumerDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public ICollection<Consumer> GetConsumersMatchNameWithOffsetAndLimit(string name, int offset, int limit) {
            return _localDbContext
                .Consumers.Where(consumer => consumer.Name.Contains(name))
                .Take(limit)
                .ToList();
        }

        public void Insert(Consumer entity) {
            _localDbContext
                .Consumers.Add(entity);
            _localDbContext.SaveChanges();
        }

        public void Delete(Consumer entity) {
            _localDbContext
                .Consumers.Remove(entity);
            _localDbContext.SaveChanges();
        }

        public Consumer GetById(string id) {
            return _localDbContext
                .Consumers.Find(id);
        }
    }
}