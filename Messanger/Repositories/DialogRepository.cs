using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Database;
using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Database.Dao;

namespace Messanger.Repositories {
    public class DialogRepository : IDialogRepository {

        private readonly IMapper _mapper;
        private readonly DialogDao _dialogDao;

        public DialogRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _dialogDao = new DialogDao(localDbContext);
        }

        public void Delete(Dialog model) {
            _dialogDao.Delete(_mapper.Map<Database.Models.Dialog>(model));
        }

        public Dialog GetById(int id) {
            return _mapper.Map<Dialog>(
                _dialogDao.GetById(id));
        }

        public ICollection<Dialog> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit) {
            return _mapper.Map<ICollection<Dialog>>(
                _dialogDao.GetDialogsByConsumerIdWithOffsetAndLimit(consumerId, offset, limit));
        }

        public void Insert(Dialog model) {
            _dialogDao.Insert(_mapper.Map<Database.Models.Dialog>(model));
        }
    }
}