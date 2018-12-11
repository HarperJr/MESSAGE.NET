
using System.Linq;
using DataService.Data;
using AutoMapper;
using System.Collections.Generic;
using DataService.Data.Models;
using System.ServiceModel;

namespace DataService {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DataService : IDataService {

        private readonly IMapper _mapper;
        private readonly LocalDbContext _localDbContext;
        
        public DataService() {
            _mapper = DataMapper.Initialize();
            _localDbContext = new LocalDbContext();
        }

        public ICollection<MultimediaContract> GetAttachedMultimediasByMessageId(string messageId) {
            return _mapper.Map<ICollection<MultimediaContract>>(
                _localDbContext.AttachedMultimedia.Where(multimedia => multimedia.Message.Id.Equals(messageId)));
        }

        public ICollection<ContactContract> GetConsumerContactsById(string id) {
            return _mapper.Map<ICollection<ContactContract>>(
                _localDbContext.Contacts.Where(contact => contact.InitialConsumer.Id.Equals(id)));
        }

        public ConsumerContract GetConsumerById(string id) {
           return _mapper.Map<ConsumerContract>(
               _localDbContext.Consumers.Find(id));
        }

        public ICollection<DialogContract> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit) {
            return _mapper.Map<ICollection<DialogContract>>(
                _localDbContext.DialogParticipants.Where(part => part.Participant.Id.Equals(consumerId))
                .Select(part => part.Dialog)
                .Skip(offset)
                .Take(limit)
                );
                
        }

        public ICollection<MessageContract> GetMessagesByDialogIdWithOffsetAndLimit(int dialogId, int offset, int limit) {
            return _mapper.Map<ICollection<MessageContract>>(
                _localDbContext.Messages.Where(message => message.Dialog.Id.Equals(dialogId))
                .Reverse()
                .Skip(offset)
                .Take(limit)
                );
        }

        public ICollection<ConsumerContract> GetConsumersMatchNameWithOffsetAndLimit(string name, int offset, int limit) {
            return _mapper.Map<ICollection<ConsumerContract>>(
                _localDbContext.Consumers.Where(consumer => consumer.Name.Contains(name))
                .Skip(offset)
                .Take(limit)
                );
        }

        public MultimediaContract GetMultimediaById(string id) {
            return _mapper.Map<MultimediaContract>(
                _localDbContext.Multimedia.Find(id)
                );
        }

        public void InsertMultimedias(ICollection<MultimediaContract> multimedias) {
            _localDbContext.Multimedia.AddRange(_mapper.Map<ICollection<Multimedia>>(multimedias));
            _localDbContext.SaveChanges();
        }

        public void InsertConsumer(ConsumerContract consumerContract) {
            _localDbContext.Consumers.Add(_mapper.Map<Consumer>(consumerContract));
            _localDbContext.SaveChanges();
        }

        public void InsertConsumerContact(string consumerId, ContactContract contactContract) {
            _localDbContext.Contacts.Add(_mapper.Map<Contact>(contactContract));
            _localDbContext.SaveChanges();
        }

        public void InsertDialog(DialogContract dialogContract) {
            _localDbContext.Dialogs.Add(_mapper.Map<Dialog>(dialogContract));
            _localDbContext.SaveChanges();
        }

        public void InsertMessages(ICollection<MessageContract> messages) {
            _localDbContext.Messages.AddRange(_mapper.Map<ICollection<Message>>(messages));
            _localDbContext.SaveChanges();
        }
    }
}
