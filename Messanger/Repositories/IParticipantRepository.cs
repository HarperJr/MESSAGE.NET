using Messanger.Data.Models;
using Messanger.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Messanger.Repositories {
    public interface IParticipantRepository : IRepository<Participant, int> {

        Participant GetParticipantById(string participantId);

        ICollection<Participant> GetParticipantsByDialogId(string dialogId);
    }
}