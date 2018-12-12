using Messanger.Database.Dao.Base;
using Messanger.Database.Models;
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

        public DialogParticipant GetParticipantById(string participantId) {
            return _localDbContext
                .DialogParticipants.Where(participant => participant.Participant.Id.Equals(participantId))
                .First();
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