using Messanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Repositories.Base {
    public interface IConsumerRepository : IRepository<Consumer> {

        Consumer GetConsumerById(string id);

        ICollection<Consumer> GetConsumerContactsById(string id);

        ICollection<Consumer> GetConsumersByMatchingNameWithOffsetAndLimit(string name, int offset, int limit);
    }
}