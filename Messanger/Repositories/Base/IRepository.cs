using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories.Base {
    public interface IRepository<Model, Id> {

        Model GetById(Id id);

        void Insert(Model model);

        void Delete(Model model);
    }
}