
using System.Linq;
using DataService.Data;
using AutoMapper;
using System.Collections.Generic;
using DataService.Data.Models;
using System.ServiceModel;

namespace DataService {
    public class DataService : IDataService {

        const int DATA_CHANGE_REFUSED = 0;
        const int DATA_CHANGED = 1;

        private IDataDuplexCallback Callback => OperationContext.Current.GetCallbackChannel<IDataDuplexCallback>();

        private readonly LocalDbContext _localDbContext = new LocalDbContext();
        
        public DataService() {
            DataMapper.Configure();
        }

        private void SaveChanges() {
            switch(_localDbContext.SaveChanges()) {
                case DATA_CHANGED: {
                        Callback.OnComplete();
                        break;
                    }
                case DATA_CHANGE_REFUSED: {
                        Callback.OnError();
                        break;
                    }
            }
        }

        public ICollection<MultimediaContract> GetAttachedMultimediasByMessageId(string messageId) {
            return Mapper.Map<ICollection<MultimediaContract>>(
                _localDbContext.AttachedMultimedia.Where(multimedia => multimedia.Message.Id.Equals(messageId)));
        }

        public ICollection<ConsumerContract> GetConsumerContactsById(string id) {
            return Mapper.Map<ICollection<ConsumerContract>>(
                _localDbContext.Contacts.Where(contact => contact.InitialConsumer.Id.Equals(id)));
        }

        public ConsumerContract GetConsumerDataById(string id) {
           return Mapper.Map<ConsumerContract>(
               _localDbContext.Consumers.Find(id));
        }

        public ICollection<DialogContract> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit) {
            return Mapper.Map<ICollection<DialogContract>>(
                _localDbContext.DialogParticipants.Where(part => part.Participant.Id.Equals(consumerId))
                .Select(part => part.Dialog)
                .Skip(offset)
                .Take(limit)
                );
                
        }

        public ICollection<MessageContract> GetMessagesByDialogIdWithOffsetAndLimit(int dialogId, int offset, int limit) {
            return Mapper.Map<ICollection<MessageContract>>(
                _localDbContext.Messages.Where(message => message.Dialog.Id.Equals(dialogId))
                .Reverse()
                .Skip(offset)
                .Take(limit)
                );
        }

        public MultimediaContract GetMultimediaById(string id) {
            return Mapper.Map<MultimediaContract>(
                _localDbContext.Multimedia.Find(id)
                );
        }

        public void InsertMultimedias(ICollection<MultimediaContract> multimedias) {
            _localDbContext.Multimedia.AddRange(Mapper.Map<ICollection<Multimedia>>(multimedias));
            SaveChanges();
        }

        public void InsertConsumer(ConsumerContract consumerContract) {
            _localDbContext.Consumers.Add(Mapper.Map<Consumer>(consumerContract));
            SaveChanges();
        }

        public void InsertConsumerContact(string consumerId, ContactContract contactContract) {
            _localDbContext.Contacts.Add(Mapper.Map<Contact>(contactContract));
            SaveChanges();
        }

        public void InsertDialog(DialogContract dialogContract) {
            _localDbContext.Dialogs.Add(Mapper.Map<Dialog>(dialogContract));
            SaveChanges();
        }

        public void InsertMessages(ICollection<MessageContract> messages) {
            _localDbContext.Messages.AddRange(Mapper.Map<ICollection<Message>>(messages));
            SaveChanges();
        }
    }
}
