﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataService {

    [ServiceContract(CallbackContract = typeof(IDataDuplexCallback))]
    public interface IDataService {

        [OperationContract]
        void InsertConsumer(ConsumerContract consumerContract);

        [OperationContract]
        void InsertConsumerContact(string consumerId, ContactContract contactContract);

        [OperationContract]
        void InsertDialog(DialogContract dialogContract);

        [OperationContract]
        void InsertMessages(ICollection<MessageContract> messages);

        [OperationContract]
        void InsertMultimedias(ICollection<MultimediaContract> multimedias);

        [OperationContract]
        ICollection<ConsumerContract> GetConsumerContactsById(string id);

        [OperationContract]
        ConsumerContract GetConsumerDataById(string id);

        [OperationContract]
        ICollection<DialogContract> GetDialogsByConsumerIdWithOffsetAndLimit(string consumerId, int offset, int limit);

        [OperationContract]
        ICollection<MessageContract> GetMessagesByDialogIdWithOffsetAndLimit(int dialogId, int offset, int limit);

        [OperationContract]
        ICollection<MultimediaContract> GetAttachedMultimediasByMessageId(string messageId);

        [OperationContract]
        MultimediaContract GetMultimediaById(string id);
    }

    public interface IDataDuplexCallback {

        [OperationContract(IsOneWay = true)]
        void OnComplete();

        [OperationContract(IsOneWay = true)]
        void OnError();
    }

    [DataContract]
    public class ConsumerContract {

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime LastTimeOnline { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string AvatarId { get; set; }
    }

    [DataContract]
    public class ContactContract {

        [DataMember]
        public string ContactId { get; set; }

        [DataMember]
        public DateTime InitTime { get; set; }

        [DataMember]
        public string Status { get; set; }
    }

    [DataContract]
    public class DialogContract {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string OwnerId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime InitDate { get; set; }

        [DataMember]
        public string MultimediaId { get; set; }
    }

    [DataContract]
    public class ParticipantContract {

        [DataMember]
        public int DialogId { get; set; }

        [DataMember]
        public string ParticipantId { get; set; }

        [DataMember]
        public string InvitorId { get; set; }
    }

    [DataContract]
    public class MessageContract {

        [DataMember]
        public int DialogId { get; set; }

        [DataMember]
        public string SenderId { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public bool HasMultimedia { get; set; }

        [DataMember]
        public bool Viewed { get; set; }
    }

    [DataContract]
    public class MultimediaContract {

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public string RemotePath { get; set; }
    }
}
