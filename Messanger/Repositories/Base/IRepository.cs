using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories.Base {
    public interface IRepository<Model> {

        void Insert(Model model);

        void InsertAll(ICollection<Model> models);

        void Delete(Model model);
    }
}