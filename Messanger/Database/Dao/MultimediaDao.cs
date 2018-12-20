using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao {
    public class MultimediaDao : IDao<Multimedia, string> {

        private readonly LocalDbContext _localDbContext;

        public MultimediaDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public void Delete(Multimedia entity) {
            _localDbContext.Multimedia
                .Remove(entity);
            _localDbContext.SaveChanges();
        }

        public Multimedia GetById(string id) {
            return _localDbContext.Multimedia
                .Find(id);
        }

        public void Insert(Multimedia entity) {
            _localDbContext.Multimedia
                .Add(entity);
            _localDbContext.SaveChanges();
        }
    }
}