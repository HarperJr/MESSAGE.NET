using Messanger.Database.Models;
using Messanger.DataBase.Dao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao {
    public class ParticipantDao : IDao<DialogParticipant, int> {

        private readonly LocalDbContext _localDbContext;

        public ParticipantDao(LocalDbContext localDbContext) {
            _localDbContext = localDbContext;
        }

        public ICollection<DialogParticipant> GetParticipantsByDialogId(int dialogId) {
            return _localDbContext
                .DialogParticipants.Where(participant => participant.Dialog.Id.Equals(dialogId))
                .ToList();
        }

        public void Delete(DialogParticipant entity) {
            throw new NotImplementedException();
        }

        public DialogParticipant GetById(int id) {
            throw new NotImplementedException();
        }

        public void Insert(DialogParticipant entity) {
            throw new NotImplementedException();
        }

    }
}