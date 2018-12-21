using Database;
using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao {
    public class DialogDao : IDao<Dialog, int> {

        private readonly LocalDbContext _localDbContext;

        public DialogDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public ICollection<Dialog> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit) {
            return _localDbContext
                .DialogParticipants
                .Where(participant => participant.Participant.Id.Equals(consumerId))
                .Select(participant => participant.Dialog)
                .Union(_localDbContext.Dialogs
                .Where(dialog => dialog.Owner.Id.Equals(consumerId)))
                .OrderBy(dialog => dialog.InitTime)
                .Take(limit)
                .ToList();
        }

        public void Delete(Dialog entity) {
            _localDbContext
                .Dialogs.Remove(entity);
            _localDbContext.SaveChanges();
        }

        public Dialog GetById(int id) {
            return _localDbContext
                .Dialogs.Find(id);
        }

        public void Insert(Dialog entity) {
            _localDbContext
                .Dialogs.Add(entity);
            _localDbContext.SaveChanges();
        }
    }
}