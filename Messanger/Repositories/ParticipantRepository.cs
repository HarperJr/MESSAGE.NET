using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Database;
using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Database.Dao;
using Messanger.Database.Models;

namespace Messanger.Repositories {
    public class ParticipantRepository : IParticipantRepository {

        private readonly IMapper _mapper;
        private readonly ParticipantDao _participantDao;

        public ParticipantRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _participantDao = new ParticipantDao(localDbContext);
        }

        public ICollection<Participant> GetParticipantsByDialogId(int dialogId) {
            return _mapper.Map<ICollection<Participant>>(
                _participantDao.GetParticipantsByDialogId(dialogId));
        }

        public void Delete(Participant model) {
            _participantDao.Delete(_mapper.Map<DialogParticipant>(model));
        }

        public Participant GetById(int id) {
            return _mapper.Map<Participant>(
                _participantDao.GetById(id));
        }

        public void Insert(Participant model) {
            _participantDao.Insert(_mapper.Map<DialogParticipant>(model));
        }
    }
}