using System;
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
        void InsertConsumerData(ConsumerContract consumerContract);

        [OperationContract]
        void InsertConsumerContact(ConsumerContract consumer, string contactId);

        [OperationContract]
        ICollection<ConsumerContract> GetConsumerContactsById(string id);

        [OperationContract]
        ConsumerContract GetConsumerDataById(string id);

    }

    public interface IDataDuplexCallback {

        [OperationContract(IsOneWay = true)]
        void OnComplete();

        [OperationContract(IsOneWay = true)]
        void OnSucces();

        [OperationContract]
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

        [DataMember]
        public ICollection<ConsumerContract> Contacts { get; set; }
    }
}
