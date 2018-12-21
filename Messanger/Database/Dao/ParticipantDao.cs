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

        public ICollection<DialogParticipant> GetParticipantsByDialogId(string dialogId) {
            return _localDbContext
                .DialogParticipants.Where(participant => participant.Dialog.Id.Equals(dialogId))
                .ToList();
        }

        public ICollection<string> GetParticipantNamesByDialogId(string dialogId) {
            return _localDbContext.DialogParticipants
                .Where(participant => participant.Dialog.Id.Equals(dialogId))
                .Join(_localDbContext.Consumers, p => p.Participant.Id, c => c.Id, (p, c) => new { c.Name })
                .Select(consumer => consumer.Name)
                .ToList();
        }

        public void Delete(DialogParticipant entity) {
            _localDbContext.DialogParticipants
                .Remove(entity);
            _localDbContext.SaveChanges();
        }

        public DialogParticipant GetById(int id) {
            return _localDbContext.DialogParticipants
                .Find(id);
        }

        public void Insert(DialogParticipant entity) {
            _localDbContext.DialogParticipants
                .Add(entity);
            _localDbContext.SaveChanges();
        }

    }
}