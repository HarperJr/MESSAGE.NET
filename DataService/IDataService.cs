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
        IEnumerable<Dialog> getDialogListByConsumerId(string consuemerId);

        [OperationContract]
        IEnumerable<Contact> getContactsByConsumerId(string consumerId);

    }

    [DataContract]
    public class Dialog {

        [DataMember]
        public string DialogTitle { get; set; }
    }

    [DataContract]
    public class Contact {

        [DataMember]
        public string ContactName { get; set; }
    }
}
