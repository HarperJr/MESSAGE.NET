using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataService {

    [ServiceContract]
    public interface IDataService {

        [OperationContract]
        ConsumerContract GetConsumerDataById(string id);

    }

    [DataContract]
    public class ConsumerContract {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime LastTimeOnline { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string AvatarId { get; set; }

        [DataMember]
        public ICollection<ContactContract> Contacts { get; set; }
    }

    [DataContract]
    public class ContactContract {

        [DataMember]
        public string Id { get; set; }

        [DataMember] 
        public string Name { get; set; }

        [DataMember]
        public string AvatarId { get; set; }
    }
}
