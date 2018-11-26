

using System.Collections.Generic;

namespace DataService {
    public class DataService : IDataService {

        public IEnumerable<Dialog> getDialogListByConsumerId(string consuemerId) {
            var testDialog = new Dialog() {
                DialogTitle = "Test Dialog"
            };
            return new List<Dialog>() { testDialog };
        }
    }
}
