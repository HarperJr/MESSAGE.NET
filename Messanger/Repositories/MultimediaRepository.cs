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
    public class MultimediaRepository : IMultimediaRepository {

        private readonly IMapper _mapper;
        private readonly MultimediaDao _multimediaDao;

        public MultimediaRepository(LocalDbContext localDbContext) {
            _mapper = DataMapper.Initialize();
            _multimediaDao = new MultimediaDao(localDbContext);
        }

        public void Delete(Multimedia model) {
            _multimediaDao.Delete(_mapper.Map<Database.Models.Multimedia>(model));
        }

        public Multimedia GetById(string id) {
            return _mapper.Map<Multimedia>(_multimediaDao.GetById(id));
        }

        public void Insert(Multimedia model) {
            _multimediaDao.Insert(_mapper.Map<Database.Models.Multimedia>(model));
        }
    }
}