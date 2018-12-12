using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Database.Dao.Base {
    public interface IDao<Entity, Id> {
        Entity GetById(Id id);

        void Insert(Entity entity);

        void Delete(Entity entity);
    }
}